using JOWheels.Models;
using JOWheels.Models.Repository;
using Microsoft.AspNetCore.Mvc;

namespace JOWheels.Controllers
{
    public class FuleController : Controller
    {

        private readonly ICRUDAction<Fule> _action;

        public FuleController(ICRUDAction<Fule> action)
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
        public IActionResult Create(Fule fule)
        {
            _action.Add(fule);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View(_action.GetBy(id));
        }

        [HttpPost]
        public IActionResult Edit(Fule fule)
        {
            _action.Update(fule);
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