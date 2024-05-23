using LibraryApp.Model;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Repositories
{
    public class NotifyRepository : IRepository<Notify>
    {
        LibraryAppContext db = new LibraryAppContext();
        public List<Notify> GetAll()
        {
            return db.Notifies.ToList();
        }
        public Notify Get(int id)
        {
            return db.Notifies.FirstOrDefault(b => b.IdNotify == id);
        }
        public void Add(Notify notify)
        {
            db.Notifies.Add(notify);
            db.SaveChanges();
        }
        public void Update(Notify notify)
        {
            db.Entry(notify).State = EntityState.Modified;
            db.SaveChanges();
        }
        public void Delete(int id)
        {
            db.Notifies.Remove(db.Notifies.Where(d => d.IdNotify == id).Single());
            db.SaveChanges();
        }
    }
}
