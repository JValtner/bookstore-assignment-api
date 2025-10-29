using BookstoreApplication.Data;
using BookstoreApplication.Models;
using BookstoreApplication.Repository;
using BookstoreApplication.Services.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookstoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private readonly IPublishersService _publishersService;

        public PublishersController(IPublishersService publishersService)
        {
            _publishersService = publishersService;

        }
        // GET: api/publishers
        [Authorize(Policy = "PublicGet")]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _publishersService.GetAllAsync());
        }


        // GET api/publishers/5
        [Authorize(Policy = "PublicGet")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneAsync(int id)
        {
            return Ok(await _publishersService.GetByIdAsync(id));
        }

        // POST api/publishers
        [Authorize(Policy = "RegisteredPost")]
        [HttpPost]
        public async Task<IActionResult> PostAsync(Publisher publisher)
        {
            return Ok(await _publishersService.AddAsync(publisher));
        }

        // PUT api/publishers/5
        [Authorize(Policy = "EditContent")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, Publisher publisher)
        {
            return Ok(await _publishersService.UpdateAsync(id, publisher));
        }

        // DELETE api/publishers/5
        [Authorize(Policy = "EditContent")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _publishersService.DeleteAsync(id);
            return NoContent();
        }
        // GET /api/publishers/sortTypes
        [Authorize(Policy = "PublicGet")]
        [HttpGet("sortTypes")]
        public IActionResult GetSortTypes()
        {
            return Ok(_publishersService.GetSortTypes());
        }
        // GET /api/publishers/sort?sortType=2
        [Authorize(Policy = "PublicGet")]
        [HttpGet("sort")]
        public async Task<IActionResult> GetSortedPublishers([FromQuery] int sortType = (int)PublisherSortType.NAME_ASCENDING)
        {
            return Ok(await _publishersService.GetAllSortedAsync(sortType));
        }
    }
}
