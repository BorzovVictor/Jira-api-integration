using System.Threading;
using System.Threading.Tasks;
using Hangfire;
using Jira.Api.Infrastructure;
using Jira.Api.Infrastructure.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Jira.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ILogger<ValuesController> _logger;
        private readonly IOptions<JCredentials> _options;
        private readonly IJUsersService _usersService;
        private readonly IJProjectService _projectService;
        private readonly IJSprintService _sprintService;
        private readonly IJBoardService _boardService;
        private readonly IJIssueTypeService _issueTypeService;
        private readonly IJIssueCommentService _commentService;
        private readonly IJIssueWorklogService _worklogService;


        private JCredentials Credentials { get; }

        public ValuesController(ILogger<ValuesController> logger, IOptions<JCredentials> options,
            IJUsersService usersService, IJProjectService projectService, IJSprintService sprintService,
            IJBoardService boardService, IJIssueTypeService issueTypeService, IJIssueCommentService commentService, IJIssueWorklogService worklogService)
        {
            _logger = logger;
            _options = options;
            _usersService = usersService;
            _projectService = projectService;
            _sprintService = sprintService;
            _boardService = boardService;
            _issueTypeService = issueTypeService;
            _commentService = commentService;
            _worklogService = worklogService;
            Credentials = options.Value;
        }

        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken ct)
        {
            _logger.LogDebug("test log");
//            var projects = await _projectService.GetProjectsAsync(ct);
            var items = await _issueTypeService.GetAsync(ct);
            return Ok(items);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, CancellationToken ct)
        {
            var item = await _worklogService.GetByIssueIdAsync(id, ct);
            return Ok(item);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetWorkLog([FromQuery] int workLogId, [FromQuery] int issueId,
            CancellationToken ct)
        {
            var item = await _worklogService.GetByIdAsync(workLogId, issueId, ct);
            return Ok(item);
        }
        
        [HttpGet("[action]")]
        public async Task<IActionResult> StartProjectJob([FromServices] IJProjectsRepository projectsRepository,
            CancellationToken ct)
        {
            await Task.Run(() => { BackgroundJob.Enqueue(() =>  projectsRepository.SyncProjects(ct) );}, ct);
            return Ok();
        }
        
        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}