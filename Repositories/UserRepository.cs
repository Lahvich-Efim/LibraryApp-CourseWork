using LibraryApp.Model;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp
{
    public class UserRepository : IRepository<User>
    {
        LibraryAppContext db = new LibraryAppContext();
        public List<User> GetAll()
        {
            return db.Users.ToList();
        }
        public User? Get(int id)
        {
            return db.Users.FirstOrDefault(b => b.UserId == id);
        }
        public void Add(User user)
        {
            db.Users.Add(user);
            db.SaveChanges();
        }
        public void Update(User user)
        {
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
        }
        public void Delete(int id)
        {
            db.Users.Remove(db.Users.Where(d => d.UserId == id).Single());
            db.SaveChanges();
        }
    }
}
