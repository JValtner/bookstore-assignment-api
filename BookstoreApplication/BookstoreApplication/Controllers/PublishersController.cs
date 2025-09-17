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
    public class PublishersController : ControllerBase
    {
        private readonly BooksRepository _booksRepository;
        private readonly PublishersRepository _publishersRepository;

        public PublishersController(BooksRepository booksRepository, PublishersRepository publishersRepository)
        {
            _publishersRepository = publishersRepository;
            _booksRepository = booksRepository;

        }
        // GET: api/publishers
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _publishersRepository.GetAllAsync());
        }

        // GET api/publishers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneAsync(int id)
        {
            Publisher publisher = await _publishersRepository.GetByIdAsync(id);
            if (publisher == null)
            {
                return NotFound();
            }
            return Ok(publisher);
        }

        // POST api/publishers
        [HttpPost]
        public async Task<IActionResult> PostAsync(Publisher publisher)
        {
            return Ok(await _publishersRepository.AddAsync(publisher));
        }

        // PUT api/publishers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, Publisher publisher)
        {
            if (id != publisher.Id)
            {
                return BadRequest();
            }

            Publisher existingPublisher = await _publishersRepository.GetByIdAsync(id);
            if (existingPublisher == null)
            {
                return NotFound();
            }
            return Ok(await _publishersRepository.UpdateAsync(existingPublisher));
        }

        // DELETE api/publishers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            Publisher existingPublisher = await _publishersRepository.GetByIdAsync(id);
            if (existingPublisher == null)
            {
                return NotFound();
            }
            await _publishersRepository.DeleteAsync(id);
            // kaskadno brisanje svih knjiga obrisanog izdavača
            await _booksRepository.DeleteAllForPublisherAsync(id);
            return NoContent();
        }
    }
}
