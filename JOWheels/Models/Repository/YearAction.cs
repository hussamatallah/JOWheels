
namespace JOWheels.Models.Repository
{
    public class YearAction : ICRUDAction<Year>
    {

        private readonly JOWheelsDbContext _db;     // to connect whit database

        public YearAction(JOWheelsDbContext db)
        {
            _db = db;
        }

        public void Add(Year entity)
        {
            _db.Years.Add(entity);     // the invoke by name of the table
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            _db.Remove(GetBy(id));
            _db.SaveChanges();
        }

        public Year GetBy(int id)
        {
            return _db.Years.Find(id);
        }

        public IEnumerable<Year> List()
        {
            return _db.Years.ToList();
        }

        public void Update(Year entity)
        {
            _db.Entry(entity).State =
                Microsoft.EntityFrameworkCore.EntityState.Modified;
            _db.SaveChanges();
        }
    }
}
