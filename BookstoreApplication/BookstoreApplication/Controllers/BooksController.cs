using BookstoreApplication.Models;
using BookstoreApplication.Repository;
using BookstoreApplication.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBooksService _booksService;

        public BooksController(IBooksService booksService)
        {
            _booksService = booksService;  
        }

        // GET: api/books
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _booksService.GetAllAsync());
        }

        // GET api/books/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneAsync(int id)
        {
            return Ok(await _booksService.GetByIdAsync(id));
        }

        // POST api/books
        [HttpPost]
        public async Task<IActionResult> PostAsync(Book book)
        {
            return Ok(await _booksService.AddAsync(book));
        }

        // PUT api/books/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, Book book)
        {
            return Ok(await _booksService.UpdateAsync(id, book));
        }

        // DELETE api/books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _booksService.DeleteAsync(id);
            return NotFound();
   
        }
    }
}
