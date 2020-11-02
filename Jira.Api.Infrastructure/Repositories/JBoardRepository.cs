using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Jira.Api.Infrastructure.Extensions;
using Jira.Core.Data.Entities;
using Microsoft.EntityFrameworkCore.Query;

namespace Jira.Api.Infrastructure
{
    public interface IJBoardRepository
    {
        Task<List<JBoard>> GetDbSprintAsync(Expression<Func<JBoard, bool>> query = null,
            Func<IQueryable<JBoard>, IOrderedQueryable<JBoard>> orderBy = null,
            Func<IQueryable<JBoard>, IIncludableQueryable<JBoard, object>> include = null,
            bool disableTracking = true, CancellationToken token = default);

        Task Sync(CancellationToken token = default);
    }

    public class JBoardRepository : IJBoardRepository
    {
        private readonly IGenericRepository<JBoard> _repository;
        private readonly IJBoardService _boardService;
        private readonly IMapper _mapper;

        public JBoardRepository(IGenericRepository<JBoard> repository, IMapper mapper, IJBoardService boardService)
        {
            _repository = repository;
            _mapper = mapper;
            _boardService = boardService;
        }

        public async Task<List<JBoard>> GetDbSprintAsync(Expression<Func<JBoard, bool>> query = null,
            Func<IQueryable<JBoard>, IOrderedQueryable<JBoard>> orderBy = null,
            Func<IQueryable<JBoard>, IIncludableQueryable<JBoard, object>> include = null, bool disableTracking = true,
            CancellationToken token = default)
        {
            return await _repository.GetAsync(query, orderBy, include, disableTracking, token);
        }

        public async Task Sync(CancellationToken token = default)
        {
            var jiraBoards = await _boardService.GetBoardsAsync(token);

            if (jiraBoards.IsNullOrEmpty())
                throw new Exception("boards for sync not found");

            var dbBoards = (await _repository.GetAsync(token: token)) ?? new List<JBoard>();
            foreach (var board in jiraBoards)
            {
                var existBoard = dbBoards.FirstOrDefault(x => x.Id==board.Id);
                if (existBoard.IsNullOrEmpty())
                {
                    await _repository.InsertAsync(board);
                }
                else
                {
                    _repository.PatchEntity(board, existBoard);
                }
            }
            await _repository.SaveAsync(token);
        }
    }
}