using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Jira.Api.Controllers.Common;
using Jira.Api.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Jira.Api.Controllers
{
    public class ProjectsController : BaseController
    {
        private readonly ILogger<ProjectsController> _logger;
        private readonly IJProjectsRepository _projectsRepository;

        public ProjectsController(ILogger<ProjectsController> logger, IJProjectsRepository projectsRepository)
        {
            _logger = logger;
            _projectsRepository = projectsRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken ct)
        {
            try
            {
                var projects =
                    await _projectsRepository
                        .GetDbProjectsAsync(
                            orderBy: o => o.OrderBy(x => x.Name),
                            token: ct
                        );
                return Ok(projects);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return BadRequest(e.GetBaseException().Message);
            }
        }
    }
}