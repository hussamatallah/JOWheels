namespace JOWheels.Models.Repository
{
    public class TransmissionAction : ICRUDAction<Transmission>
    {
        private readonly JOWheelsDbContext _db;     // to connect whit database

        public TransmissionAction(JOWheelsDbContext db)
        {
            _db = db;
        }

        public void Add(Transmission entity)
        {
            _db.Transmissions.Add(entity);     // the invoke by name of the table
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            _db.Transmissions.Remove(GetBy(id));
            _db.SaveChanges();
        }

        public Transmission GetBy(int id)
        {
            return _db.Transmissions.Find(id);
        }

        public IEnumerable<Transmission> List()
        {
            return _db.Transmissions.ToList();
        }

        public void Update(Transmission entity)
        {
            _db.Entry(entity).State =
                Microsoft.EntityFrameworkCore.EntityState.Modified;
            _db.SaveChanges();
        }
    }
}
