using LibraryApp.Model;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp
{
    public class ProductsRepository : IRepository<Product>
    {

        LibraryAppContext db = new LibraryAppContext();
        public List<Product> GetAll()
        {
            return db.Products.ToList();
        }
        public Product Get(int id)
        {
            return db.Products.FirstOrDefault(p => p.ProductCode == id);
        }
        public List<Product> GetAllWithCollections()
        {
            return db.Products.Include(p => p.Collections).ToList();
        }
        public Product GetWithCollections(int id)
        {
            return db.Products.Include(p => p.Collections).FirstOrDefault(p => p.ProductCode == id);
        }
        public void Add(Product Product)
        {
            db.Products.Add(Product);
            db.SaveChanges();
        }
        public void Update(Product Product)
        {
            db.Entry(Product).State = EntityState.Modified;
            db.SaveChanges();
        }
        public void Delete(int id)
        {
            db.Products.Remove(db.Products.Where(d => d.ProductCode == id).Single());
            db.SaveChanges();
        }
        public Product? Find(int id)
        {
            return db.Products.Find(id);
        }

        //public List<Product> FindByName(string name)
        //{
        //    return db.Products
        //}
    }
}
