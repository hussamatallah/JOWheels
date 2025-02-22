namespace JOWheels.Models.Repository
{
    public class FuleAction : ICRUDAction<Fule>
    {
        private readonly JOWheelsDbContext _db;     // to connect whit database

        public FuleAction(JOWheelsDbContext db)
        {
            _db = db;
        }

        public void Add(Fule entity)
        {
            _db.Fules.Add(entity);     // the invoke by name of the table
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            _db.Fules.Remove(GetBy(id));
            _db.SaveChanges();
        }

        public Fule GetBy(int id)
        {
            return _db.Fules.Find(id);
        }

        public IEnumerable<Fule> List()
        {
            return _db.Fules.ToList();
        }

        public void Update(Fule entity)
        {
            _db.Entry(entity).State =
                Microsoft.EntityFrameworkCore.EntityState.Modified;
            _db.SaveChanges();
        }
    }
}
