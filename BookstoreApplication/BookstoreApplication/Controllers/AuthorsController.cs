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
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _authorsRepository.GetAllAsync());
        }

        // GET api/authors/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            Author author = await _authorsRepository.GetByIdAsync(id);
            if (author == null)
            {
                return NotFound();
            }
            return Ok(author);
        }

        // POST api/authors
        [HttpPost]
        public async Task<IActionResult> PostAsync(Author author)
        {
            return Ok(await _authorsRepository.AddAsync(author));
        }

        // PUT api/authors/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, Author author)
        {
            if (id != author.Id)
            {
                return BadRequest();
            }

            if (!await _authorsRepository.ExistsAsync(id))
            {
                return NotFound();
            }
            return Ok(await _authorsRepository.UpdateAsync(author));
        }

        // DELETE api/authors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (! await _authorsRepository.ExistsAsync(id))
            {
                return NotFound();
            }
            await _authorsRepository.DeleteAsync(id);
            // kaskadno brisanje svih knjiga obrisanog autora
            await _booksRepository.DeleteAllForAuthorAsync(id);
            return NoContent();
        }
        // TODO kaskadno brisanje svih vezanih knjiga AuthorsAwards
    }
}
