using LibraryApp.Model;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp
{
    public class LibraryRepository : IRepository<Library>
    {
        LibraryAppContext db = new LibraryAppContext();
        public List<Library> GetAll()
        {
            return db.Libraries.Include(p => p.Collections).ToList();
        }
        public Library Get(int id)
        {
            return db.Libraries.Include(p => p.Collections).FirstOrDefault(b => b.UserId == id);
        }
        public void Add(Library library)
        {
            db.Libraries.Add(library);
            db.SaveChanges();
        }
        public void Update(Library library)
        {
            db.Entry(library).State = EntityState.Modified;
            db.SaveChanges();
        }
        public void Delete(int id)
        {
            db.Libraries.Remove(db.Libraries.Where(d => d.LibraryId == id).Single());
            db.SaveChanges();
        }
    }
}
