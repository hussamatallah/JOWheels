namespace JOWheels.Models.Repository
{
    public class BrandAction : ICRUDAction<Brand>
    {
        private readonly JOWheelsDbContext _db;     // to connect whit database

        public BrandAction(JOWheelsDbContext db)
        {
            _db = db;
        }

        public void Add(Brand entity)
        {
            _db.Brands.Add(entity);     // the invoke by name of the table
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            _db.Brands.Remove(GetBy(id));
            _db.SaveChanges();
        }

        public Brand GetBy(int id)
        {
            return _db.Brands.Find(id);
        }

        public IEnumerable<Brand> List()
        {
            return _db.Brands.ToList();
        }

        public void Update(Brand entity)
        {
            _db.Entry(entity).State =
                Microsoft.EntityFrameworkCore.EntityState.Modified;
            _db.SaveChanges();
        }
    }
}
