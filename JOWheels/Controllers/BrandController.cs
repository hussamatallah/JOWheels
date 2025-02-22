using JOWheels.Models.Repository;
using JOWheels.Models;
using Microsoft.AspNetCore.Mvc;

namespace JOWheels.Controllers
{
    public class BrandController : Controller
    {

        private readonly ICRUDAction<Brand> _action;

        public BrandController(ICRUDAction<Brand> action)
        {
            _action = action;
        }

        public IActionResult Index()
        {
            return View(_action.List());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Brand brand, IFormFile Upload)
        {

            if (Upload == null || Upload.Length == 0)
            {
                ModelState.AddModelError("ImageFile", "Please select a valid image.");
                return View();
            }

            // Save the Image to wwwroot/
            var filePath = Path.Combine("wwwroot/Photos/Brands", Upload.FileName); // ~/Photos/Brands/imagename.jpg
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                Upload.CopyToAsync(stream);
            }

            brand.Image = Upload.FileName;

            _action.Add(brand);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View(_action.GetBy(id));
        }

        [HttpPost]
        public IActionResult Edit(Brand brand, IFormFile Upload)
        {

            if (Upload == null || Upload.Length == 0)
            {
                ModelState.AddModelError("ImageFile", "Please select a valid image.");
                return View();
            }

            // Save the Image to wwwroot/
            var filePath = Path.Combine("wwwroot/Photos/Brands", Upload.FileName); // ~/Photos/Brands/imagename.jpg
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                Upload.CopyToAsync(stream);
            }

            brand.Image = Upload.FileName;

            _action.Update(brand);
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