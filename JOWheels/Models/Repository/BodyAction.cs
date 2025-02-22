using Microsoft.EntityFrameworkCore;

namespace JOWheels.Models.Repository
{
    public class BodyAction : ICRUDAction<Body>
    {
        private readonly JOWheelsDbContext _db;     // to connect whit database

        public BodyAction(JOWheelsDbContext db)
        {
            _db = db;
        }

        public void Add(Body entity)
        {
            _db.Bodies.Add(entity);     // the invoke by name of the table
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            _db.Bodies.Remove(GetBy(id));
            _db.SaveChanges();
        }

        public Body GetBy(int id)
        {
            return _db.Bodies.Include(c => c.Brand).FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<Body> List()
        {
            return _db.Bodies.Include(c => c.Brand).ToList();
        }

        public void Update(Body entity)
        {
            _db.Entry(entity).State =
                Microsoft.EntityFrameworkCore.EntityState.Modified;
            _db.SaveChanges();
        }
    }
}
