using LibraryApp.Model;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Repositories
{
    public class TypeBookRepository : IRepository<TypeBook>
    {
        LibraryAppContext db = new LibraryAppContext();
        public List<TypeBook> GetAll()
        {
            return db.TypeBooks.ToList();
        }
        public TypeBook Get(int id)
        {
            return db.TypeBooks.FirstOrDefault(b => b.IdType == id);
        }
        public void Add(TypeBook typeBook)
        {
            db.TypeBooks.Add(typeBook);
            db.SaveChanges();
        }
        public void Update(TypeBook typeBook)
        {
            db.Entry(typeBook).State = EntityState.Modified;
            db.SaveChanges();
        }
        public void Delete(int id)
        {
            db.TypeBooks.Remove(db.TypeBooks.Where(d => d.IdType == id).Single());
            db.SaveChanges();
        }
    }
}
