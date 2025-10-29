using BookstoreApplication.Data;
using BookstoreApplication.Models;
using BookstoreApplication.Repository;
using BookstoreApplication.Repository;
using BookstoreApplication.Services.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookstoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorsService _authorsService;

        public AuthorsController(IAuthorsService authorsService)
        {
            _authorsService = authorsService;
        }
        // GET: api/authors
        [Authorize(Policy = "PublicGet")]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _authorsService.GetAllAsync());
        }

        // GET api/authors/5
        [Authorize(Policy = "PublicGet")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            return Ok(await _authorsService.GetByIdAsync(id));
        }

        // POST api/authors
        [Authorize(Policy = "RegisteredPost")]
        [HttpPost]
        public async Task<IActionResult> PostAsync(Author author)
        {
            return Ok(await _authorsService.AddAsync(author));
        }

        // PUT api/authors/5
        [Authorize(Policy = "EditContent")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, Author author)
        {
            return Ok(await _authorsService.UpdateAsync(id, author));
        }

        // DELETE api/authors/5
        [Authorize(Policy = "EditContent")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _authorsService.DeleteAsync(id);
            return NoContent();
        }

        [Authorize(Policy = "PublicGet")]
        [HttpGet("paging")]
        public async Task<IActionResult> GetAuthorsPage([FromQuery] int page = 1, int pageSize = 5)
        {
            return Ok(await _authorsService.GetAllPagedAsync(page, pageSize));
        }

    }
}
