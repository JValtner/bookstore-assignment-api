using BookstoreApplication.Data;
using BookstoreApplication.Models;
using BookstoreApplication.Repository;
using BookstoreApplication.Services;
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
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _publishersService.GetAllAsync());
        }

        // GET api/publishers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneAsync(int id)
        {
            return Ok(await _publishersService.GetByIdAsync(id));
        }

        // POST api/publishers
        [HttpPost]
        public async Task<IActionResult> PostAsync(Publisher publisher)
        {
            return Ok(await _publishersService.AddAsync(publisher));
        }

        // PUT api/publishers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, Publisher publisher)
        {
            return Ok(await _publishersService.UpdateAsync(id, publisher));
        }

        // DELETE api/publishers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _publishersService.DeleteAsync(id);
            return NoContent();
        }
    }
}
