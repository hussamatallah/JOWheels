using Microsoft.EntityFrameworkCore;

namespace JOWheels.Models
{
    public class JOWheelsDbContext : DbContext
    {

        // Constuctor -> to retrieve  from DbContext
        public JOWheelsDbContext(DbContextOptions options) 
            : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Governorate> Governorates { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Body> Bodies { get; set; }
        public DbSet<Transmission> Transmissions { get; set; }
        public DbSet<Fule> Fules { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Condition> Conditions { get; set; }
        public DbSet<Mileage> Mileages { get; set; }
        public DbSet<Custom> Customs { get; set; }
        public DbSet<License> Licenses { get; set; }
        public DbSet<Insurance> Insurances { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Year> Years { get; set; }
        public DbSet<ImageGallary> ImageGallaries { get; set; }

    }
}
