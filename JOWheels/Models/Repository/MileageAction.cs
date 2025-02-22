namespace JOWheels.Models.Repository
{
    public class MileageAction : ICRUDAction<Mileage>
    {
        private readonly JOWheelsDbContext _db;     // to connect whit database

        public MileageAction(JOWheelsDbContext db)
        {
            _db = db;
        }

        public void Add(Mileage entity)
        {
            _db.Mileages.Add(entity);     // the invoke by name of the table
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            _db.Mileages.Remove(GetBy(id));
            _db.SaveChanges();
        }

        public Mileage GetBy(int id)
        {
            return _db.Mileages.Find(id);
        }

        public IEnumerable<Mileage> List()
        {
            return _db.Mileages.ToList();
        }

        public void Update(Mileage entity)
        {
            _db.Entry(entity).State =
                Microsoft.EntityFrameworkCore.EntityState.Modified;
            _db.SaveChanges();
        }
    }
}
