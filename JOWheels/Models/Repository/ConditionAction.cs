namespace JOWheels.Models.Repository
{
    public class ConditionAction : ICRUDAction<Condition>
    {
        private readonly JOWheelsDbContext _db;     // to connect whit database

        public ConditionAction(JOWheelsDbContext db)
        {
            _db = db;
        }

        public void Add(Condition entity)
        {
            _db.Conditions.Add(entity);     // the invoke by name of the table
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            _db.Conditions.Remove(GetBy(id));
            _db.SaveChanges();
        }

        public Condition GetBy(int id)
        {
            return _db.Conditions.Find(id);
        }

        public IEnumerable<Condition> List()
        {
            return _db.Conditions.ToList();
        }

        public void Update(Condition entity)
        {
            _db.Entry(entity).State =
                Microsoft.EntityFrameworkCore.EntityState.Modified;
            _db.SaveChanges();
        }
    }
}
