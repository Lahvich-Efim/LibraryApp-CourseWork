using LibraryApp.Model;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp
{
    public class CategoriesRepository : IRepository<Category>
    {
        LibraryAppContext db = new LibraryAppContext();
        public List<Category> GetAll()
        {
            return db.Categories.Include(p => p.Products).ToList();
        }
        public Category Get(int id)
        {
            return db.Categories.Include(p => p.Products).FirstOrDefault(b => b.CategoryId == id);
        }
        public void Add(Category category)
        {
            db.Categories.Add(category);
            db.SaveChanges();
        }
        public void Update(Category category)
        {
            db.Entry(category).State = EntityState.Modified;
            db.SaveChanges();
        }
        public void Delete(int id)
        {
            db.Categories.Remove(db.Categories.Where(d => d.CategoryId == id).Single());
            db.SaveChanges();
        }
    }
}
