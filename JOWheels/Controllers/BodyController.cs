using JOWheels.Models.Repository;
using JOWheels.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JOWheels.Controllers
{
    public class BodyController : Controller
    {

        private readonly ICRUDAction<Body> _action;
        private readonly ICRUDAction<Brand> _actionBrand;

        public BodyController(ICRUDAction<Body> action, ICRUDAction<Brand> actionBrand)
        {
            _action = action;
            _actionBrand = actionBrand;
        }

        public IActionResult Index()
        {
            return View(_action.List());
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.BrandId = new SelectList(_actionBrand.List(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Body body, IFormFile Upload)
        {

            if (Upload == null || Upload.Length == 0)
            {
                ModelState.AddModelError("ImageFile", "Please select a valid image.");
                return View();
            }

            // Save the Image to wwwroot/
            var filePath = Path.Combine("wwwroot/Photos/Bodies", Upload.FileName); // ~/Photos/Bodies/imagename.jpg
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                Upload.CopyToAsync(stream);
            }

            body.Image = Upload.FileName;

            _action.Add(body);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var body = _action.GetBy(id);
            ViewBag.BrandId = new SelectList(_actionBrand.List(), "Id", "Name", body.BrandId);
            return View(_action.GetBy(id));
        }

        [HttpPost]
        public IActionResult Edit(Body body, IFormFile Upload)
        {

            if (Upload == null || Upload.Length == 0)
            {
                ModelState.AddModelError("ImageFile", "Please select a valid image.");
                return View();
            }

            // Save the Image to wwwroot/
            var filePath = Path.Combine("wwwroot/Photos/Bodies", Upload.FileName); // ~/Photos/Bodies/imagename.jpg
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                Upload.CopyToAsync(stream);
            }

            body.Image = Upload.FileName;

            _action.Update(body);
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