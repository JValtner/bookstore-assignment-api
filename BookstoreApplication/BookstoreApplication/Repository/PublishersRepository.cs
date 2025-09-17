using BookstoreApplication.Models;

namespace BookstoreApplication.Repo
{
    public class PublishersRepository
    {
        private BookStoreDbContext _context;

        public PublishersRepository(BookStoreDbContext context)
        {
            _context = context;
        }
        public List<Publisher> GetAll()
        {
            return _context.Publishers.ToList();
        }
        public Publisher? GetById(int id)
        {
            return _context.Publishers.Find(id);
        }
        public Publisher Add(Publisher publisher)
        {
            _context.Publishers.Add(publisher);
            _context.SaveChanges();
            return publisher;
        }
        public Publisher Update(Publisher publisher)
        {
            _context.Publishers.Update(publisher);
            _context.SaveChanges();
            return publisher;
        }
        public bool Delete(int id)
        {
            Publisher publisher = _context.Publishers.Find(id);
            if (publisher == null)
            {
                return false;
            }
            _context.Publishers.Remove(publisher);
            _context.SaveChanges();
            return true;
        }
    }
}
