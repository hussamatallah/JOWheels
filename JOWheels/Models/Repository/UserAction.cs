using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace JOWheels.Models.Repository
{
    public class UserAction : ICRUDAction<User>
    {
        private readonly JOWheelsDbContext _db;     // to connect whit database

        public UserAction(JOWheelsDbContext db)
        {
            _db = db;
        }

        public void Add(User entity)
        {
            //entity.Password = HashPassword(entity.Password);

            _db.Users.Add(entity);     // the invoke by name of the table
            _db.SaveChanges();
        }

        // For security part 1
        //private string HashPassword(string password)
        //{
        //    using (SHA256 sha256 = SHA256.Create())
        //    {
        //        byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        //        return Convert.ToBase64String(hashBytes); // convert string to hash
        //    }
        //}

        public void Delete(int id)
        {
            _db.Users.Remove(GetBy(id));
            _db.SaveChanges();
        }

        public User GetBy(int id)
        {
            return _db.Users.Include(u => u.Governorate).Include(u => u.Region).FirstOrDefault(u => u.Id == id);
        }

        public IEnumerable<User> List()
        {
            return _db.Users.Include(u => u.Governorate).Include(u => u.Region).ToList();
        }

        public void Update(User entity)
        {
            _db.Entry(entity).State =
            Microsoft.EntityFrameworkCore.EntityState.Modified;
            _db.SaveChanges();
        }
    }
}
