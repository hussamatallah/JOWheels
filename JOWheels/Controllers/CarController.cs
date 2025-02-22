using JOWheels.Models.Repository;
using JOWheels.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JOWheels.Controllers
{
    public class CarController : Controller
    {

        private readonly JOWheelsDbContext _db;
        private readonly ICRUDAction<Car> _action;
        private readonly ICRUDAction<Brand> _actionBrand;
        private readonly ICRUDAction<Body> _actionBody;
        private readonly ICRUDAction<Transmission> _actionTransmission;
        private readonly ICRUDAction<Fule> _actionFule;
        private readonly ICRUDAction<Color> _actionColor;
        private readonly ICRUDAction<Seat> _actionSeat;
        private readonly ICRUDAction<Condition> _actionCondition;
        private readonly ICRUDAction<Mileage> _actionMileage;
        private readonly ICRUDAction<Custom> _actionCustom;
        private readonly ICRUDAction<License> _actionLicense;
        private readonly ICRUDAction<Insurance> _actionInsurance;
        private readonly ICRUDAction<Payment> _actionPayment;
        private readonly ICRUDAction<Year> _actionYear;

        public CarController(ICRUDAction<Car> action, ICRUDAction<Brand> actionBrand, ICRUDAction<Body> actionBody,
                             ICRUDAction<Transmission> actionTransmission, ICRUDAction<Fule> actionFule,
                             ICRUDAction<Color> actionColor, ICRUDAction<Seat> actionSeat, ICRUDAction<Condition> actionCondition,
                             ICRUDAction<Mileage> actionMileage, ICRUDAction<Custom> actionCustom,
                             ICRUDAction<License> actionLicense, ICRUDAction<Insurance> actionInsurance,
                             ICRUDAction<Payment> actionPayment, ICRUDAction<Year> actionYear,
                             JOWheelsDbContext db)
        {
            _action = action;
            _actionBrand = actionBrand;
            _actionBody = actionBody;
            _actionTransmission = actionTransmission;
            _actionFule = actionFule;
            _actionColor = actionColor;
            _actionSeat = actionSeat;
            _actionCondition = actionCondition;
            _actionMileage = actionMileage;
            _actionCustom = actionCustom;
            _actionLicense = actionLicense;
            _actionInsurance = actionInsurance;
            _actionPayment = actionPayment;
            _actionYear = actionYear;
            _db = db;
        }

        public IActionResult Index()
        {
            return View(_action.List());
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.BrandId = new SelectList(_actionBrand.List(), "Id", "Name");
            ViewBag.BodyId = new SelectList(_actionBody.List(), "Id", "Name");
            ViewBag.TransmissionId = new SelectList(_actionTransmission.List(), "Id", "Type");
            ViewBag.FuleId = new SelectList(_actionFule.List(), "Id", "Type");
            ViewBag.ColorId = new SelectList(_actionColor.List(), "Id", "Name");
            ViewBag.SeatId = new SelectList(_actionSeat.List(), "Id", "Number");
            ViewBag.ConditionId = new SelectList(_actionCondition.List(), "Id", "State");
            ViewBag.MileageId = new SelectList(_actionMileage.List(), "Id", "Mile");
            ViewBag.CustomId = new SelectList(_actionCustom.List(), "Id", "State");
            ViewBag.LicenseId = new SelectList(_actionLicense.List(), "Id", "State");
            ViewBag.InsuranceId = new SelectList(_actionInsurance.List(), "Id", "Type");
            ViewBag.PaymentId = new SelectList(_actionPayment.List(), "Id", "Type");
            ViewBag.YearId = new SelectList(_actionYear.List(), "Id", "Date");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Car car, IFormFile Upload, IFormFile[] UploadGallary)
        {

            if (Upload == null || Upload.Length == 0)
            {
                ModelState.AddModelError("ImageFile", "Please select a valid image.");
                return View();
            }

            // Save the Image to wwwroot/
            var filePath = Path.Combine("wwwroot/Photos/Cars", Upload.FileName); // ~/Photos/Cars/imagename.jpg
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                Upload.CopyToAsync(stream);
            }

            car.Image = Upload.FileName;
            car.EntityId = Convert.ToInt32(HttpContext.Session.GetInt32("UserId"));
            _action.Add(car);


            // For Gallary Photos
            var lastCar = _db.Cars.Max(x => x.Id);

            if (UploadGallary != null)
            {
                foreach (var item in UploadGallary)
                {
                    var filePath2 = Path.Combine("wwwroot/Photos/Gallary", item.FileName); // ~/images/imagename.jpg
                    using (var stream = new FileStream(filePath2, FileMode.Create))         // let you save the pics in the path
                    {
                        item.CopyToAsync(stream);
                    }
                    var gallary = new ImageGallary();
                    gallary.ImageURL = item.FileName;
                    gallary.CarId = lastCar;
                    _db.ImageGallaries.Add(gallary);
                    _db.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var car = _action.GetBy(id);
            ViewBag.BrandId = new SelectList(_actionBrand.List(), "Id", "Name", car.BrandId);
            ViewBag.BodyId = new SelectList(_actionBody.List(), "Id", "Name", car.BodyId);
            ViewBag.TransmissionId = new SelectList(_actionTransmission.List(), "Id", "Type", car.TransmissionId);
            ViewBag.FuleId = new SelectList(_actionFule.List(), "Id", "Type", car.FuleId);
            ViewBag.ColorId = new SelectList(_actionColor.List(), "Id", "Name", car.ColorId);
            ViewBag.SeatId = new SelectList(_actionSeat.List(), "Id", "Number", car.SeatId);
            ViewBag.ConditionId = new SelectList(_actionCondition.List(), "Id", "State", car.ConditionId);
            ViewBag.MileageId = new SelectList(_actionMileage.List(), "Id", "Mile", car.MileageId);
            ViewBag.CustomId = new SelectList(_actionCustom.List(), "Id", "State", car.CustomId);
            ViewBag.LicenseId = new SelectList(_actionLicense.List(), "Id", "State", car.LicenseId);
            ViewBag.InsuranceId = new SelectList(_actionInsurance.List(), "Id", "Type", car.InsuranceId);
            ViewBag.PaymentId = new SelectList(_actionPayment.List(), "Id", "Type", car.PaymentId);
            ViewBag.YearId = new SelectList(_actionYear.List(), "Id", "Date", car.YearId);
            return View(_action.GetBy(id));
        }

        [HttpPost]
        public IActionResult Edit(Car car, IFormFile Upload, IFormFile[] UploadGallary)
        {

            if (Upload == null || Upload.Length == 0)
            {
                ModelState.AddModelError("ImageFile", "Please select a valid image.");
                return View();
            }

            // Save the Image to wwwroot/
            var filePath = Path.Combine("wwwroot/Photos/Cars", Upload.FileName); // ~/Photos/Cars/imagename.jpg
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                Upload.CopyToAsync(stream);
            }

            car.Image = Upload.FileName;
            car.EntityId = Convert.ToInt32(HttpContext.Session.GetInt32("UserId"));
            _action.Update(car);


            // For Gallary Photos
            var lastCar = _db.Cars.Max(x => x.Id);

            if (UploadGallary != null)
            {
                foreach (var item in UploadGallary)
                {
                    var filePath2 = Path.Combine("wwwroot/Photos/Gallary", item.FileName); // ~/images/imagename.jpg
                    using (var stream = new FileStream(filePath2, FileMode.Create))         // let you save the pics in the path
                    {
                        item.CopyToAsync(stream);
                    }
                    var gallary = new ImageGallary();
                    gallary.ImageURL = item.FileName;
                    gallary.CarId = lastCar;
                    _db.ImageGallaries.Update(gallary);
                    _db.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            return View(_action.GetBy(id));
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirm(int id)
        {
            _action.Delete(id);
            return RedirectToAction("Index");
        }
    }
}