using LibraryApp.Model;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp
{
    public class CollectionRepository : IRepository<Collection>
    {
        LibraryAppContext db = new LibraryAppContext();
        public List<Collection> GetAll()
        {
            return db.Collections.Include(p => p.ProductCodes).ToList();
        }
        public Collection Get(int id)
        {
            return db.Collections.Include(p => p.ProductCodes).FirstOrDefault(b => b.CollectionId == id);
        }
        public List<Collection> GetAllNoWithProduct()
        {
            return db.Collections.ToList();
        }
        public Collection GetNoWithProduct(int id)
        {
            return db.Collections.FirstOrDefault(b => b.CollectionId == id);
        }
        public void Add(Collection collection)
        {
            db.Collections.Add(collection);
            db.SaveChanges();
        }
        public void Update(Collection collection)
        {
            db.Entry(collection).State = EntityState.Modified;
            db.SaveChanges();
        }
        public void Delete(int id)
        {
            db.Collections.Remove(db.Collections.Where(d => d.CollectionId == id).Single());
            db.SaveChanges();
        }
    }
}
