namespace JOWheels.Models.Repository
{
    public class SeatAction : ICRUDAction<Seat>
    {
        private readonly JOWheelsDbContext _db;     // to connect whit database

        public SeatAction(JOWheelsDbContext db)
        {
            _db = db;
        }

        public void Add(Seat entity)
        {
            _db.Seats.Add(entity);     // the invoke by name of the table
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            _db.Seats.Remove(GetBy(id));
            _db.SaveChanges();
        }

        public Seat GetBy(int id)
        {
            return _db.Seats.Find(id);
        }

        public IEnumerable<Seat> List()
        {
            return _db.Seats.ToList();
        }

        public void Update(Seat entity)
        {
            _db.Entry(entity).State =
                Microsoft.EntityFrameworkCore.EntityState.Modified;
            _db.SaveChanges();
        }
    }
}
