using LibraryApp.Model;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp
{
    public class BasketRepository : IRepository<Basket>
    {
        LibraryAppContext db = new LibraryAppContext();
        public List<Basket> GetAll()
        {
            return db.Baskets.Include(p => p.ProductCodes).ToList();
        }
        public Basket Get(int id)
        {
            return db.Baskets.Include(p => p.ProductCodes).FirstOrDefault(b => b.UserId == id);
        }
        public void Add(Basket basket)
        {
            db.Baskets.Add(basket);
            db.SaveChanges();
        }
        public void Update(Basket basket)
        {
            db.Entry(basket).State = EntityState.Modified;
            db.SaveChanges();
        }
        public void Delete(int id)
        {
            db.Baskets.Remove(db.Baskets.Where(d => d.UserId == id).Single());
            db.SaveChanges();
        }
    }
}
