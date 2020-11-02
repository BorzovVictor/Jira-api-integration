using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Jira.Api.Controllers.Common;
using Jira.Api.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Jira.Api.Controllers
{
    public class UsersController : BaseController
    {
        private readonly ILogger<ValuesController> _logger;
        private readonly IJUsersRepository _usersRepository;

        public UsersController(ILogger<ValuesController> logger, IJUsersRepository usersRepository)
        {
            _logger = logger;
            _usersRepository = usersRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers(CancellationToken ct)
        {
            try
            {
                var users = await _usersRepository
                    .GetDbUsersAsync(
                        query: q => q.AccountType == "atlassian",
                        orderBy: queryable => queryable.OrderBy(x => x.Name),
                        token: ct);
                return Ok(users);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return BadRequest(e.GetBaseException().Message);
            }
        }
    }
}