namespace JOWheels.Models.Repository
{
    public class LicenseAction : ICRUDAction<License>
    {
        private readonly JOWheelsDbContext _db;     // to connect whit database

        public LicenseAction(JOWheelsDbContext db)
        {
            _db = db;
        }

        public void Add(License entity)
        {
            _db.Licenses.Add(entity);     // the invoke by name of the table
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            _db.Licenses.Remove(GetBy(id));
            _db.SaveChanges();
        }

        public License GetBy(int id)
        {
            return _db.Licenses.Find(id);
        }

        public IEnumerable<License> List()
        {
            return _db.Licenses.ToList();
        }

        public void Update(License entity)
        {
            _db.Entry(entity).State =
                Microsoft.EntityFrameworkCore.EntityState.Modified;
            _db.SaveChanges();
        }
    }
}
