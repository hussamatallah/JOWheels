

using Microsoft.EntityFrameworkCore;

namespace JOWheels.Models.Repository
{
    public class RegionAction : ICRUDAction<Region>
    {
        private readonly JOWheelsDbContext _db;     // to connect whit database

        public RegionAction(JOWheelsDbContext db)
        {
            _db = db;
        }

        public void Add(Region entity)
        {
            _db.Regions.Add(entity);     // the invoke by name of the table
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            _db.Regions.Remove(GetBy(id));
            _db.SaveChanges();
        }

        public Region GetBy(int id)
        {
            return _db.Regions.Include(c => c.Governorate).FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<Region> List()
        {
            return _db.Regions.Include(c => c.Governorate).ToList();
        }

        public void Update(Region entity)
        {
            _db.Entry(entity).State =
                Microsoft.EntityFrameworkCore.EntityState.Modified;
            _db.SaveChanges();
        }
    }
}
