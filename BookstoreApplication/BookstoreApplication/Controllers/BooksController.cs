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
            return NoContent();
   
        }
        // GET /api/publishers/sortTypes
        [HttpGet("sortTypes")]
        public IActionResult GetSortTypes()
        {
            return Ok(_booksService.GetSortTypesAsync());
        }
        // GET /api/publishers/sort?sortType=2
        [HttpGet("sort")]
        public async Task<IActionResult> GetSortedPublishersAsync([FromQuery] int sortType = (int)BookSortType.BOOK_NAME_ASCENDING)
        {
            return Ok(await _booksService.GetAllSortedAsync(sortType));
        }
    }
}
