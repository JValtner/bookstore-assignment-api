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
        public IActionResult GetAll()
        {
            return Ok(_booksRepository.GetAll());
        }

        // GET api/books/5
        [HttpGet("{id}")]
        public IActionResult GetOne(int id)
        {
            var book = _booksRepository.GetById(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        // POST api/books
        [HttpPost]
        public IActionResult Post(Book book)
        {
            // kreiranje knjige je moguće ako je izabran postojeći autor
            Author author =_authorsRepository.GetById(book.AuthorId);
            if (author == null)
            {
                return BadRequest();
            }

            // kreiranje knjige je moguće ako je izabran postojeći izdavač
            Publisher publisher =_publishersRepository.GetById(book.PublisherId);
            if (publisher == null)
            {
                return BadRequest();
            }

            book.AuthorId = author.Id;
            book.Author = author;
            book.PublisherId= publisher.Id;
            book.Publisher = publisher;
            Book added_book = _booksRepository.Add(book);
            return Ok(added_book);
        }

        // PUT api/books/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, Book book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }

            if (!_booksRepository.Exists(id))
            {
                return NotFound();
            }

            // izmena knjige je moguca ako je izabran postojeći autor
            //TODO napraviti exist funkcije u repozitorijumima za autore i izdavače
            Author author = _authorsRepository.GetById(book.AuthorId);
            if (author == null)
            {
                return BadRequest();
            }

            // izmena knjige je moguca ako je izabran postojeći izdavač
            //TODO napraviti exist funkcije u repozitorijumima za autore i izdavače
            Publisher publisher = _publishersRepository.GetById(book.PublisherId);
            if (publisher == null)
            {
                return BadRequest();
            }
            
            book.Author = author;
            book.Publisher = publisher;
            Book updated_book = _booksRepository.Update(book);
            return Ok(updated_book);
        }

        // DELETE api/books/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Book existingBook = _booksRepository.GetById(id);
            if (existingBook == null)
            {
                return NotFound();
            }
            _booksRepository.Delete(id);
          return NoContent();       
        }
    }
}
