using JOWheels.Models;
using JOWheels.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;

namespace JOWheels.Controllers
{
    public class GovernorateController : Controller
    {

        private readonly ICRUDAction<Governorate> _action;

        public GovernorateController(ICRUDAction<Governorate> action)
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
        public IActionResult Create(Governorate governorate)
        {
            _action.Add(governorate);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View(_action.GetBy(id));
        }

        [HttpPost]
        public IActionResult Edit(Governorate governorate)
        {
            _action.Update(governorate);
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
