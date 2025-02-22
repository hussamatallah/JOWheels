
namespace JOWheels.Models.Repository
{
    public class GovernorateAction : ICRUDAction<Governorate>
    {
        private readonly JOWheelsDbContext _db;     // to connect whit database

        public GovernorateAction(JOWheelsDbContext db)
        {
            _db = db;
        }

        public void Add(Governorate entity)
        {
            _db.Governorates.Add(entity);     // the invoke by name of the table
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            _db.Governorates.Remove(GetBy(id));
            _db.SaveChanges();
        }

        public Governorate GetBy(int id)
        {
            return _db.Governorates.Find(id);
        }

        public IEnumerable<Governorate> List()
        {
            return _db.Governorates.ToList();
        }

        public void Update(Governorate entity)
        {
            _db.Entry(entity).State =
                Microsoft.EntityFrameworkCore.EntityState.Modified;
            _db.SaveChanges();
        }
    }
}
