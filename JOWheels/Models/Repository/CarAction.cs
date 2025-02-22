using Microsoft.EntityFrameworkCore;

namespace JOWheels.Models.Repository
{
    public class CarAction : ICRUDAction<Car>
    {
        private readonly JOWheelsDbContext _db;     // to connect whit database

        public CarAction(JOWheelsDbContext db)
        {
            _db = db;
        }

        public void Add(Car entity)
        {
            _db.Cars.Add(entity);     // the invoke by name of the table
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            _db.Cars.Remove(GetBy(id));
            _db.SaveChanges();
        }

        public Car GetBy(int id)
        {
            return _db.Cars.Include(c => c.Entity)
                .Include(c => c.Brand).Include(c => c.Body)
                .Include(c => c.Transmission).Include(c => c.Fule)
                .Include(c => c.Color).Include(c => c.Seat)
                .Include(c => c.Condition).Include(c => c.Mileage)
                .Include(c => c.Custom).Include(c => c.License)
                .Include(c => c.Insurance).Include(c => c.Payment).Include(c=>c.Year)
                .FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<Car> List()
        {
            return _db.Cars.Include(c => c.Entity)
                .Include(c => c.Brand).Include(c => c.Body)
                .Include(c => c.Transmission).Include(c => c.Fule)
                .Include(c => c.Color).Include(c => c.Seat)
                .Include(c => c.Condition).Include(c => c.Mileage)
                .Include(c => c.Custom).Include(c => c.License)
                .Include(c => c.Insurance).Include(c => c.Payment).Include(c => c.Year)
                .ToList();
        }

        public void Update(Car entity)
        {
            _db.Entry(entity).State =
                Microsoft.EntityFrameworkCore.EntityState.Modified;
            _db.SaveChanges();
        }
    }
}
