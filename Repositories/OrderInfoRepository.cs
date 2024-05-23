using LibraryApp.Model;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp
{
    public class OrderInfoRepository : IRepository<OrderInfo>
    {
        LibraryAppContext db = new LibraryAppContext();
        public List<OrderInfo> GetAll()
        {
            return db.OrderInfos.Include(p => p.ProductCodes).ToList();
        }
        public OrderInfo Get(int id)
        {
            return db.OrderInfos.Include(p => p.ProductCodes).FirstOrDefault(b => b.OrderId == id);
        }
        public void Add(OrderInfo orderInfo)
        {
            db.OrderInfos.Add(orderInfo);
            db.SaveChanges();
        }
        public void Update(OrderInfo orderInfo)
        {
            db.Entry(orderInfo).State = EntityState.Modified;
            db.SaveChanges();
        }
        public void Delete(int id)
        {
            db.OrderInfos.Remove(db.OrderInfos.Where(d => d.OrderId == id).Single());
            db.SaveChanges();
        }
    }
}
