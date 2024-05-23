using LibraryApp.Model;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp
{
    public class DictionaryRepository : IRepository<Dictionary>
    {
        LibraryAppContext db = new LibraryAppContext();
        public List<Dictionary> GetAll()
        {
            return db.Dictionaries.ToList();
        }
        public Dictionary Get(int id)
        {
            return db.Dictionaries.FirstOrDefault(b => b.WordId == id);
        }
        public void Add(Dictionary dictionary)
        {
            db.Dictionaries.Add(dictionary);
            db.SaveChanges();
        }
        public void Update(Dictionary dictionary)
        {
            db.Entry(dictionary).State = EntityState.Modified;
            db.SaveChanges();
        }
        public void Delete(int id)
        {
            db.Dictionaries.Remove(db.Dictionaries.Where(d => d.WordId == id).Single());
            db.SaveChanges();
        }
    }
}
