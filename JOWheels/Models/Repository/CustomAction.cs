namespace JOWheels.Models.Repository
{
    public class CustomAction : ICRUDAction<Custom>
    {
        private readonly JOWheelsDbContext _db;     // to connect whit database

        public CustomAction(JOWheelsDbContext db)
        {
            _db = db;
        }

        public void Add(Custom entity)
        {
            _db.Customs.Add(entity);     // the invoke by name of the table
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            _db.Customs.Remove(GetBy(id));
            _db.SaveChanges();
        }

        public Custom GetBy(int id)
        {
            return _db.Customs.Find(id);
        }

        public IEnumerable<Custom> List()
        {
            return _db.Customs.ToList();
        }

        public void Update(Custom entity)
        {
            _db.Entry(entity).State =
                Microsoft.EntityFrameworkCore.EntityState.Modified;
            _db.SaveChanges();
        }
    }
}
