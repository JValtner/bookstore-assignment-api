using BookstoreApplication.Models;

namespace BookstoreApplication.Repository
{
    public class AuthorsRepository
    {
        private BookStoreDbContext _context;

        public AuthorsRepository(BookStoreDbContext context)
        {
            _context = context;
        }
        public List<Author> GetAll()
        {
            return _context.Authors.ToList();
        }
        public Author? GetById(int id)
        {
            return _context.Authors.Find(id);
        }
        public Author Add(Author author)
        {
            _context.Authors.Add(author);
            _context.SaveChanges();
            return author;
        }
        public Author Update(Author author)
        {
            _context.Authors.Update(author);
            _context.SaveChanges();
            return author;
        }
        public bool Delete(int id)
        {
            Author author = _context.Authors.Find(id);
            if (author == null)
            {
                return false;
            }

            _context.Authors.Remove(author);
            _context.SaveChanges();
            return true;
        }
    }
}
