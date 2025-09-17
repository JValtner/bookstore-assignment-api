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
        public IActionResult GetAll()
        {
            return Ok(_publishersRepository.GetAll());
        }

        // GET api/publishers/5
        [HttpGet("{id}")]
        public IActionResult GetOne(int id)
        {
            Publisher publisher = _publishersRepository.GetById(id);
            if (publisher == null)
            {
                return NotFound();
            }
            return Ok(publisher);
        }

        // POST api/publishers
        [HttpPost]
        public IActionResult Post(Publisher publisher)
        {
            Publisher Added_publisher = _publishersRepository.Add(publisher);
            return Ok(Added_publisher);
        }

        // PUT api/publishers/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, Publisher publisher)
        {
            if (id != publisher.Id)
            {
                return BadRequest();
            }

            Publisher existingPublisher = _publishersRepository.GetById(id);
            if (existingPublisher == null)
            {
                return NotFound();
            }
            Publisher updatedPublisher = _publishersRepository.Update(existingPublisher);
            return Ok(updatedPublisher);
        }

        // DELETE api/publishers/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Publisher existingPublisher = _publishersRepository.GetById(id);
            if (existingPublisher == null)
            {
                return NotFound();
            }
            _publishersRepository.Delete(id);
            // kaskadno brisanje svih knjiga obrisanog izdavača
            _booksRepository.DeleteAllForPublisher(id);
            return NoContent();
        }
    }
}
