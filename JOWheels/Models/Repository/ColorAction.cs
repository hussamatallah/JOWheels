namespace JOWheels.Models.Repository
{
    public class ColorAction : ICRUDAction<Color>
    {
        private readonly JOWheelsDbContext _db;     // to connect with database

        public ColorAction(JOWheelsDbContext db)
        {
            _db = db;
        }

        public void Add(Color entity)
        {
            _db.Colors.Add(entity);     // the invoke by name of the table
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            _db.Colors.Remove(GetBy(id));
            _db.SaveChanges();
        }

        public Color GetBy(int id)
        {
            return _db.Colors.Find(id);
        }

        public IEnumerable<Color> List()
        {
            return _db.Colors.ToList();
        }

        public void Update(Color entity)
        {
            _db.Entry(entity).State =
                Microsoft.EntityFrameworkCore.EntityState.Modified;
            _db.SaveChanges();
        }
    }
}
