using System;
using BookstoreApplication.Models;

namespace BookstoreApplication.Repository
{
    public class AwardRepository : IAwardRepository
    {
        private readonly BookStoreDbContext _context;

        public AwardRepository(BookStoreDbContext context)
        {
            _context = context;
        }

        public List<Award> GetAll()
        {
            return _context.Awards.ToList();
        }

        public Award? GetById(int id)
        {
            return _context.Awards.Find(id);
        }
        public Award Add(Award award)
        {
            _context.Awards.Add(award);
            _context.SaveChanges();
            return award;
        }

        public Award Update(Award award)
        {
            _context.Awards.Update(award);
            _context.SaveChanges();
            return award;
        }

        public bool Delete(int id)
        {
            var award = _context.Awards.Find(id);
            if (award == null)
                return false;

            _context.Awards.Remove(award);
            _context.SaveChanges();
            return true;
        }
    }
}//TODO ASYNC i controller too
