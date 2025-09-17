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
    public class AuthorsController : ControllerBase
    {

        private readonly BooksRepository _booksRepository;
        private readonly PublishersRepository _publishersRepository;
        private readonly AuthorsRepository _authorsRepository;


        public AuthorsController(BooksRepository booksRepository, AuthorsRepository authorsRepository)
        {
            _authorsRepository = authorsRepository;
            _booksRepository = booksRepository;
        }
        // GET: api/authors
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_authorsRepository.GetAll());
        }

        // GET api/authors/5
        [HttpGet("{id}")]
        public IActionResult GetOne(int id)
        {
            Author author = _authorsRepository.GetById(id);
            if (author == null)
            {
                return NotFound();
            }
            return Ok(author);
        }

        // POST api/authors
        [HttpPost]
        public IActionResult Post(Author author)
        {
            Author Added_author = _authorsRepository.Add(author);
            return Ok(Added_author);
        }

        // PUT api/authors/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, Author author)
        {
            if (id != author.Id)
            {
                return BadRequest();
            }

            Author existingAuthor = _authorsRepository.GetById(id);
            if (existingAuthor == null)
            {
                return NotFound();
            }
            Author updatedAuthor = _authorsRepository.Update(existingAuthor);
            return Ok(updatedAuthor);
        }

        // DELETE api/authors/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Author existingAuthor = _authorsRepository.GetById(id);
            if (existingAuthor == null)
            {
                return NotFound();
            }
            _authorsRepository.Delete(id);
            // kaskadno brisanje svih knjiga obrisanog autora
            _booksRepository.DeleteAllForAuthor(id);
            return NoContent();
        }
        // TODO kaskadno brisanje svih vezanih knjiga AuthorsAwards
    }
}
