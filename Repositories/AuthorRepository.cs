using LibraryApp.Model;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Repositories
{
    public class AuthorRepository : IRepository<Author>
    {
        LibraryAppContext db = new LibraryAppContext();
        public List<Author> GetAll()
        {
            return db.Authors.ToList();
        }
        public Author Get(int id)
        {
            return db.Authors.FirstOrDefault(b => b.AuthorId == id);
        }
        public void Add(Author author)
        {
            db.Authors.Add(author);
            db.SaveChanges();
        }
        public void Update(Author author)
        {
            db.Entry(author).State = EntityState.Modified;
            db.SaveChanges();
        }
        public void Delete(int id)
        {
            db.Authors.Remove(db.Authors.Where(d => d.AuthorId == id).Single());
            db.SaveChanges();
        }
    }
}
