
namespace JOWheels.Models.Repository
{
    public class InsuranceAction : ICRUDAction<Insurance>
    {
        private readonly JOWheelsDbContext _db;     // to connect whit database

        public InsuranceAction(JOWheelsDbContext db)
        {
            _db = db;
        }

        public void Add(Insurance entity)
        {
            _db.Insurances.Add(entity);     // the invoke by name of the table
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            _db.Insurances.Remove(GetBy(id));
            _db.SaveChanges();
        }

        public Insurance GetBy(int id)
        {
            return _db.Insurances.Find(id);
        }

        public IEnumerable<Insurance> List()
        {
            return _db.Insurances.ToList();
        }

        public void Update(Insurance entity)
        {
            _db.Entry(entity).State =
                Microsoft.EntityFrameworkCore.EntityState.Modified;
            _db.SaveChanges();
        }
    }
}
