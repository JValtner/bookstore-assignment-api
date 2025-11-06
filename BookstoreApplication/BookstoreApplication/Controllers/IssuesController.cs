using BookstoreApplication.DTO.ExternalComics;
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

        //POST /api/issues/
        //[Authorize(Policy = "EditContent")]
        [HttpPost("local")]
        public async Task<IActionResult> AddLocalIssue([FromBody] LocalIssueDTO dto)
        {
            if (dto == null)
                return BadRequest("Invalid issue data.");

            var created = await _issuesService.AddLocalIssueAsync(dto);
            return Ok(created);
        }

    }
}