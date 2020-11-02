using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Jira.Api.Infrastructure.Models;
using Jira.Api.Infrastructure.Options;
using Jira.Core.Data.Entities;
using Microsoft.Extensions.Options;
using RestSharp;

namespace Jira.Api.Infrastructure
{
    public interface IJBoardService
    {
        Task<List<JBoard>> GetBoardsAsync(CancellationToken token = default);
        Task<JBoard> GetBoardByIdAsync(long id, CancellationToken token = default);
    }
    public class JBoardService: JiraApi, IJBoardService
    {
        private readonly IMapper _mapper;
        public JBoardService(IOptions<JCredentials> credentials, IMapper mapper) : base(credentials)
        {
            _mapper = mapper;
        }

        public async Task<List<JBoard>> GetBoardsAsync(CancellationToken token = default)
        {
            var jsonBoards = await _jira.RestClient.ExecuteRequestAsync<JBoardQuery>(Method.GET,
                $"{BaseAgileUrl}/board", null, token);
            var jBoardItems = jsonBoards.BoardItems;
            if(jBoardItems==null || !jBoardItems.Any())
                return new List<JBoard>();

            return _mapper.Map<List<JBoard>>(jBoardItems);
        }

        public async Task<JBoard> GetBoardByIdAsync(long id, CancellationToken token = default)
        {
            var jBoardJson = await _jira.RestClient.ExecuteRequestAsync<JBoardItem>(Method.GET,
                $"{BaseAgileUrl}/board/{id}", null, token);
            if(jBoardJson==null)
                throw new Exception("board not found");
            return _mapper.Map<JBoard>(jBoardJson);
        }
    }

}