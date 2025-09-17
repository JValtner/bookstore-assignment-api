using System.Threading.Tasks;
using BookstoreApplication.Data;
using BookstoreApplication.Models;
using BookstoreApplication.Repo;
using BookstoreApplication.Repository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookstoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BooksRepository _booksRepository;
        private readonly PublishersRepository _publishersRepository;
        private readonly AuthorsRepository _authorsRepository;

        public BooksController(BooksRepository booksRepository, PublishersRepository publishersRepository, AuthorsRepository authorsRepository)
        {
            _booksRepository = booksRepository;
            _publishersRepository = publishersRepository;
            _authorsRepository = authorsRepository;
        }

        // GET: api/books
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _booksRepository.GetAllAsync());
        }

        // GET api/books/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneAsync(int id)
        {
            var book = await _booksRepository.GetByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        // POST api/books
        [HttpPost]
        public async Task<IActionResult> PostAsync(Book book)
        {
            // kreiranje knjige je moguće ako je izabran postojeći autor
            Author author = await _authorsRepository.GetByIdAsync(book.AuthorId);
            if (author == null)
            {
                return BadRequest();
            }

            // kreiranje knjige je moguće ako je izabran postojeći izdavač
            Publisher publisher =await _publishersRepository.GetByIdAsync(book.PublisherId);
            if (publisher == null)
            {
                return BadRequest();
            }

            book.AuthorId = author.Id;
            book.Author = author;
            book.PublisherId= publisher.Id;
            book.Publisher = publisher;
            Book added_book = await _booksRepository.AddAsync(book);
            return Ok(added_book);
        }

        // PUT api/books/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, Book book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }

            if (!await _booksRepository.ExistsAsync(id))
            {
                return NotFound();
            }

            // izmena knjige je moguca ako je izabran postojeći autor
            if (!await _authorsRepository.ExistsAsync(book.AuthorId))
            {
                return BadRequest();
            }

            // izmena knjige je moguca ako je izabran postojeći izdavač
            if (!await _publishersRepository.ExistsAsync(book.PublisherId))
            {
                return BadRequest();
            }
            
            book.Author = await _authorsRepository.GetByIdAsync(book.AuthorId);
            book.Publisher = await _publishersRepository.GetByIdAsync(book.PublisherId);
            Book updated_book = await _booksRepository.UpdateAsync(book);
            return Ok(updated_book);
        }

        // DELETE api/books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            Book existingBook = await _booksRepository.GetByIdAsync(id);
            if (existingBook == null)
            {
                return NotFound();
            }
            await _booksRepository.DeleteAsync(id);
          return NoContent();       
        }
    }
}
