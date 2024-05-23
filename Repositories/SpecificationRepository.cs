using LibraryApp.Model;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp
{
    public class SpecificationRepository : IRepository<Specification>
    {
        LibraryAppContext db = new LibraryAppContext();
        public List<Specification> GetAll()
        {
            return db.Specifications.Include(p => p.SpecValueForProductLists).ToList();
        }
        public List<Specification> GetAllSpec()
        {
            return db.Specifications.ToList();
        }
        public Specification? GetSpec(int id)
        {
            return db.Specifications.FirstOrDefault(b => b.SpecId == id);
        }
        public Specification? Get(int id)
        {
            return db.Specifications.Include(p => p.SpecValueForProductLists).FirstOrDefault(b => b.SpecId == id);
        }
        public void Add(Specification specification)
        {
            db.Specifications.Add(specification);
            db.SaveChanges();
        }
        public void Update(Specification specification)
        {
            db.Entry(specification).State = EntityState.Modified;
            db.SaveChanges();
        }
        public void Delete(int id)
        {
            db.Specifications.Remove(db.Specifications.Where(d => d.SpecId == id).Single());
            db.SaveChanges();
        }

        public List<Specification> GetSpecForProduct(Product product)
        {
            var specIdsForProduct = db.SpecValueForProductLists
            .Where(specvalue => specvalue.ProductId == product.ProductCode)
            .Select(specvalue => specvalue.SpecId)
            .Distinct();

            return db.Specifications
                .Where(spec => spec.ProductCodes.Any(pc => pc.ProductCode == product.ProductCode) &&
                               specIdsForProduct.Contains(spec.SpecId))
                .Include(p => p.SpecValueForProductLists)
                .ToList();
        }
    }
}
