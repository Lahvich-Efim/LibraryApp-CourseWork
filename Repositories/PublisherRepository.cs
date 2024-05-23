using LibraryApp.Model;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp
{
    public class PublisherRepository : IRepository<Publisher>
    {
        LibraryAppContext db = new LibraryAppContext();
        public List<Publisher> GetAll()
        {
            return db.Publishers.ToList();
        }
        public Publisher Get(int id)
        {
            return db.Publishers.FirstOrDefault(b => b.IdPublisher == id);
        }
        public void Add(Publisher publisher)
        {
            db.Publishers.Add(publisher);
            db.SaveChanges();
        }
        public void Update(Publisher publisher)
        {
            db.Entry(publisher).State = EntityState.Modified;
            db.SaveChanges();
        }
        public void Delete(int id)
        {
            db.Publishers.Remove(db.Publishers.Where(d => d.IdPublisher == id).Single());
            db.SaveChanges();
        }
    }
}
