using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Atlassian.Jira;
using AutoMapper;
using Jira.Api.Infrastructure.Extensions;
using Jira.Api.Infrastructure.Options;
using Jira.Core.Data;
using Jira.Core.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Options;

namespace Jira.Api.Infrastructure
{
    public interface IJUsersRepository
    {
        Task<List<JUser>> GetDbUsersAsync(Expression<Func<JUser, bool>> query = null,
            Func<IQueryable<JUser>, IOrderedQueryable<JUser>> orderBy = null,
            Func<IQueryable<JUser>, IIncludableQueryable<JUser, object>> include = null,
            bool disableTracking = true, CancellationToken token = default(CancellationToken));

        Task SyncUsers(CancellationToken token = default(CancellationToken));
    }

    public class JUsersRepository : JiraApi, IJUsersRepository
    {
        private readonly DataContext _context;
        private readonly IJUsersService _userService;

        public JUsersRepository(IOptions<JCredentials> credentials, DataContext context, IMapper mapper,
            IJUsersService userService) : base(
            credentials)
        {
            _context = context;
            _userService = userService;
        }


        public async Task<List<JUser>> GetDbUsersAsync(Expression<Func<JUser, bool>> query = null,
            Func<IQueryable<JUser>, IOrderedQueryable<JUser>> orderBy = null,
            Func<IQueryable<JUser>, IIncludableQueryable<JUser, object>> include = null, bool disableTracking = true,
            CancellationToken token = default(CancellationToken))
        {
            var users = _context.JUsers.AsQueryable();
            if (disableTracking)
                users = users.AsNoTracking();
            if (!include.IsNullOrEmpty())
                users = include(users);
            if (!query.IsNullOrEmpty())
                users = users.Where(query);
            if (!orderBy.IsNullOrEmpty())
            {
                return await orderBy(users).ToListAsync(token);
            }
            else
            {
                return await users.ToListAsync(token);
            }
        }

        public async Task SyncUsers(CancellationToken token = default(CancellationToken))
        {
            var jiraUsers = await _userService.GetUsersFullAsync(new JiraUserFilter {MaxResult = 1000}, token);
            if (jiraUsers.IsNullOrEmpty())
                throw new Exception("jira users not founded");

            var dbUsers = await _context.JUsers.ToListAsync(token);

            foreach (var jUser in jiraUsers)
            {
                var existDbUser = dbUsers.FirstOrDefault(x => x.Key == jUser.Key);
                if (existDbUser.IsNullOrEmpty())
                {
                    _context.JUsers.Add(jUser);
                }
                else
                {
                    UpdateUser(jUser, existDbUser);
                }
            }

            await _context.SaveChangesAsync(token);
        }

        private void UpdateUser(JUser jUser, JUser existDbUser)
        {
            foreach (var property in existDbUser.GetType().GetProperties())
            {
                var oldValue = property.GetValue(existDbUser);
                var newValue = property.GetValue(jUser);

                if (Equals(oldValue, newValue)) continue;

                property.SetValue(existDbUser, newValue);
            }
        }
    }

    public class JiraUserFilter
    {
        public JiraUserFilter()
        {
            Query = "";
            Status = JiraUserStatus.Active;
            StartAt = 0;
            MaxResult = 50;
        }

        public string Query { get; set; }
        public JiraUserStatus Status { get; set; }
        public int StartAt { get; set; }
        public int MaxResult { get; set; }
    }
}