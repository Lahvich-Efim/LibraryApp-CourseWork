using LibraryApp.Model;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp
{
    public class ReviewRepository : IRepository<Review>
    {
        LibraryAppContext db = new LibraryAppContext();
        public List<Review> GetAll()
        {
            return db.Reviews.Include(p => p.User).ToList();
        }
        public Review Get(int id)
        {
            return db.Reviews.Include(p => p.User).FirstOrDefault(b => b.ReviewId == id);
        }
        public void Add(Review review)
        {
            db.Reviews.Add(review);
            db.SaveChanges();
        }
        public void Update(Review review)
        {
            db.Entry(review).State = EntityState.Modified;
            db.SaveChanges();
        }
        public void Delete(int id)
        {
            db.Reviews.Remove(db.Reviews.Where(d => d.ReviewId == id).Single());
            db.SaveChanges();
        }
    }
}
