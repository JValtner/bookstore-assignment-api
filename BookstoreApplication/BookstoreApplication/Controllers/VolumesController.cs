using BookstoreApplication.Services.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VolumesController : ControllerBase
    {
        private readonly IVolumesService _volumesService;

        public VolumesController(IVolumesService volumesService)
        {
            _volumesService = volumesService;
        }

        // GET /api/volumes/search?filter=name:Batman
        [Authorize(Policy = "PublicGet")]
        [HttpGet("search")]
        public async Task<IActionResult> GetVolumesByName(string? filter =null, [FromQuery] string? sortdirection="asc", [FromQuery] int page = 1,[FromQuery] int pageSize = 10)
        {
            return Ok(await _volumesService.GetVolumesByName(filter, sortdirection, page, pageSize));
        }
    }
}