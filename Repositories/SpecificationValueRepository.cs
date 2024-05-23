using LibraryApp.Model;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp
{
    public class SpecificationValueRepository : IRepository<SpecificationValue>
    {
        LibraryAppContext db = new LibraryAppContext();
        public List<SpecificationValue> GetAll()
        {
            return db.SpecificationValues.ToList();
        }
        public SpecificationValue? Get(int id)
        {
            return db.SpecificationValues.FirstOrDefault(b => b.SpecValueId == id);
        }
        public void Add(SpecificationValue specificationValue)
        {
            db.SpecificationValues.Add(specificationValue);
            db.SaveChanges();
        }
        public void Update(SpecificationValue specificationValue)
        {
            db.Entry(specificationValue).State = EntityState.Modified;
            db.SaveChanges();
        }
        public void Delete(int id)
        {
            db.SpecificationValues.Remove(db.SpecificationValues.Where(d => d.SpecValueId == id).Single());
            db.SaveChanges();
        }
    }
}
