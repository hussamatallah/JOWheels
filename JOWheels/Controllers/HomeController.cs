using JOWheels.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Policy;

namespace JOWheels.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly JOWheelsDbContext _db;

        public object Include { get; private set; }

        public HomeController(ILogger<HomeController> logger, JOWheelsDbContext db)
        {
            _logger = logger;
            _db = db;
        }


        // Home Page (Brands and Bodies) section
        public IActionResult Index(int? brandId, int page = 1)
        {

            int pageSize = 12;

            // Filtering Each Body to its Brand
            var groupedData = _db.Bodies
                .GroupBy(body => new { body.Brand.Id, body.Brand.Name, body.Brand.Image })  // Group by Body Id and Name
                .Select(brandGroup => new
                {
                    BrandId = brandGroup.Key.Id,
                    BrandName = brandGroup.Key.Name,
                    Image = brandGroup.Key.Image,
                    Bodies = brandGroup
                    .GroupBy(body => new { body.Id, body.Name, body.Image })  // Group by Body Id and Name
                    .Select(bodyGroup => new
                    {
                        BodyId = bodyGroup.Key.Id,
                        BodyName = bodyGroup.Key.Name,
                        Image = bodyGroup.Key.Image
                    })
                    .ToList()
                })
                .ToList();


            // For Pagination
            if (brandId == null)    // if select all Brands
            {
                var allBodies = groupedData.SelectMany(b => b.Bodies).ToList();
                int totalBodies = allBodies.Count;

                var pagedBodies = allBodies
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                ViewBag.Bodies = pagedBodies;
                ViewBag.brandId = null;
                ViewBag.PageNumber = page;
                ViewBag.PageSize = pageSize;
                ViewBag.TotalPages = (int)Math.Ceiling((double)totalBodies / pageSize);
            }
            else
            {
                // filters based on Brands
                var selectBrand = groupedData.FirstOrDefault(b => b.BrandId == brandId);

                if (selectBrand != null)
                {

                    int totalBodies = selectBrand.Bodies.Count;

                    var pagedBodies = selectBrand.Bodies
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();

                    ViewBag.Bodies = pagedBodies;
                    ViewBag.BrandId = brandId;
                    ViewBag.BrandName = selectBrand.BrandName;
                    ViewBag.PageNumber = page;
                    ViewBag.PageSize = pageSize;
                    ViewBag.TotalPages = (int)Math.Ceiling((double)totalBodies / pageSize);
                }
            }

            ViewBag.GroupedData = groupedData;
            return View();
        }


        // Announcements page 
        public IActionResult Cars(int id, string? search)
        {
            if (search == null)
            {
                var cars = _db.Cars.Where(c => c.BodyId == id);
                TempData["BodyId"] = id;
                return View(cars);
            }

            else
            {
                var cars = _db.Cars.Where(c => c.BodyId == id &&
                (c.Title.Contains(search) || c.Description.Contains(search) || c.Color.Name.Contains(search) ||
                c.Transmission.Type.Contains(search) || c.Year.Date.Contains(search) || c.Price.Contains(search)));

                TempData["BodyId"] = id;
                return View(cars);
            }

        }


        // Add or Remove from Favorites (Button)
        public IActionResult FavValue(int carId)
        {
            var cars = _db.Cars.Find(carId);
            cars.IsFavorite = !cars.IsFavorite;

            _db.Entry(cars).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _db.SaveChanges();

            return RedirectToAction("Cars", new { id = TempData["BodyId"] });
        }


        public IActionResult FavoriteView()
        {
            return View(_db.Cars.Where(c => c.IsFavorite == true));
        }


        // Dealerships Page
        public IActionResult Dealerships(int? entityId, int page = 1)
        {
            int pageSize = 12;

            var groupedData = _db.Cars
                .GroupBy(car => new { car.Entity.Id, car.Entity.Name, car.Entity.UserType })
                .Select(entityGroup => new
                {
                    EntityId = entityGroup.Key.Id,
                    EntityName = entityGroup.Key.Name,
                    UserType = entityGroup.Key.UserType,
                    Cars = entityGroup
                    .GroupBy(car => new { car.Id, car.Title, car.Image })
                    .Select(car => new
                    {
                        CarId = car.Key.Id,
                        CarName = car.Key.Title,
                        Image = car.Key.Image
                    })
                    .ToList()
                })
                .Where(x=>x.UserType == "Dealership")
                .ToList();


            // For Pagination
            if (entityId == null)    // if select all Entities
            {
                var allCars = groupedData.SelectMany(c => c.Cars).ToList();
                int totalCars = allCars.Count;

                var pagedCars = allCars
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                ViewBag.Cars = pagedCars;
                ViewBag.EntityId = null;
                ViewBag.PageNumber = page;
                ViewBag.PageSize = pageSize;
                ViewBag.TotalPages = (int)Math.Ceiling((double)totalCars / pageSize);
            }
            else
            {
                var selectEntity = groupedData.FirstOrDefault(b => b.EntityId == entityId);

                if (selectEntity != null)
                {

                    int totalCars = selectEntity.Cars.Count;

                    var pagedCars = selectEntity.Cars
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();

                    ViewBag.Cars = pagedCars;
                    ViewBag.EntityId = entityId;
                    ViewBag.PageNumber = page;
                    ViewBag.PageSize = pageSize;
                    ViewBag.TotalPages = (int)Math.Ceiling((double)totalCars / pageSize);
                }
            }

            ViewBag.GroupedData = groupedData;
            return View();
        }


        // Car Details Page
        public IActionResult CarDetails(int id)
        {
            ViewBag.Gallary = _db.ImageGallaries.Where(x => x.CarId == id).ToList();

            var car = _db.Cars
                .Include(e => e.Entity)
                .Include(b => b.Brand)
                .Include(b => b.Body)
                .Include(b => b.Transmission)
                .Include(b => b.Fule)
                .Include(b => b.Color)
                .Include(b => b.Seat)
                .Include(b => b.Condition)
                .Include(b => b.Mileage)
                .Include(b => b.Custom)
                .Include(b => b.License)
                .Include(b => b.Insurance)
                .Include(b => b.Payment)
                .Include(b => b.Year)
                .FirstOrDefault(x => x.Id == id);

            return View(car);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
