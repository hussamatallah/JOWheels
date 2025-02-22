using JOWheels.Models.Repository;
using JOWheels.Models;
using Microsoft.AspNetCore.Mvc;

namespace JOWheels.Controllers
{
    public class LicenseController : Controller
    {

        private readonly ICRUDAction<License> _action;

        public LicenseController(ICRUDAction<License> action)
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
        public IActionResult Create(License license)
        {
            _action.Add(license);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View(_action.GetBy(id));
        }

        [HttpPost]
        public IActionResult Edit(License license)
        {
            _action.Update(license);
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
