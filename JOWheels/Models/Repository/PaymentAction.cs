
namespace JOWheels.Models.Repository
{
    public class PaymentAction : ICRUDAction<Payment>
    {
        private readonly JOWheelsDbContext _db;     // to connect whit database

        public PaymentAction(JOWheelsDbContext db)
        {
            _db = db;
        }

        public void Add(Payment entity)
        {
            _db.Payments.Add(entity);     // the invoke by name of the table
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            _db.Payments.Remove(GetBy(id));
            _db.SaveChanges();
        }

        public Payment GetBy(int id)
        {
            return _db.Payments.Find(id);
        }

        public IEnumerable<Payment> List()
        {
            return _db.Payments.ToList();
        }

        public void Update(Payment entity)
        {
            _db.Entry(entity).State =
                Microsoft.EntityFrameworkCore.EntityState.Modified;
            _db.SaveChanges();
        }
    }
}
