using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Jira.Api.Infrastructure.Extensions;
using Jira.Core.Data.Entities;
using Microsoft.EntityFrameworkCore.Query;

namespace Jira.Api.Infrastructure
{
    public interface IJSprintRepository
    {
        Task<List<JSprint>> GetDbSprintAsync(Expression<Func<JSprint, bool>> query = null,
            Func<IQueryable<JSprint>, IOrderedQueryable<JSprint>> orderBy = null,
            Func<IQueryable<JSprint>, IIncludableQueryable<JSprint, object>> include = null,
            bool disableTracking = true, CancellationToken token = default);
        Task Sync(CancellationToken token = default);
    }
    
    public class JSprintRepository: IJSprintRepository
    {
        private readonly IGenericRepository<JSprint> _repository;
        private readonly IJSprintService _sprintService;
        private readonly IJBoardService _boardService;

        public JSprintRepository(IGenericRepository<JSprint> repository, IJSprintService sprintService,
            IJBoardService boardService)
        {
            _repository = repository;
            _sprintService = sprintService;
            _boardService = boardService;
        }

        public async Task<List<JSprint>> GetDbSprintAsync(Expression<Func<JSprint, bool>> query = null,
            Func<IQueryable<JSprint>, IOrderedQueryable<JSprint>> orderBy = null,
            Func<IQueryable<JSprint>, IIncludableQueryable<JSprint, object>> include = null,
            bool disableTracking = true,
            CancellationToken token = default)
        {
            return await _repository.GetAsync(query, orderBy, include, disableTracking, token);
        }

        public async Task Sync(CancellationToken token = default)
        {
            var boards = await _boardService.GetBoardsAsync(token);
            
            foreach (var board in boards)
            {
                await InsertOrUpdateSprint(token, board);
            }

            await _repository.SaveAsync(token);
        }

        private async Task InsertOrUpdateSprint(CancellationToken token, JBoard board)
        {
            var sprints = await _sprintService.GetSprintsAsync(board.Id, token);
            foreach (var sprint in sprints)
            {
                var existSprint = await _repository.GetByIdAsync(sprint.Id);
                if (existSprint.IsNullOrEmpty())
                {
                    await _repository.InsertAsync(sprint);
                }
                else
                {
                    _repository.PatchEntity(sprint, existSprint);
                }
            }
        }
    }

}