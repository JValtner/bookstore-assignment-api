using BookstoreApplication.Services;
using BookstoreApplication.Services.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssuesController : ControllerBase
    {
        private readonly IIssuesService _issuesService;

        public IssuesController(IIssuesService issuesService)
        {
            _issuesService = issuesService;
        }

        // GET /api/issues/search?filter=Batman
        [Authorize(Policy = "PublicGet")]
        [HttpGet("search")]
        public async Task<IActionResult> GetIssuesByVolume(string? filter = null, [FromQuery] string? sortdirection = "asc", [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            return Ok(await _issuesService.GetIssuesByVolume(filter, sortdirection, page, pageSize));
        }
    }
}