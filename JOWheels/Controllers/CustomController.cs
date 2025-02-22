using JOWheels.Models.Repository;
using JOWheels.Models;
using Microsoft.AspNetCore.Mvc;

namespace JOWheels.Controllers
{
    public class CustomController : Controller
    {

        private readonly ICRUDAction<Custom> _action;

        public CustomController(ICRUDAction<Custom> action)
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
        public IActionResult Create(Custom custom)
        {
            _action.Add(custom);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View(_action.GetBy(id));
        }

        [HttpPost]
        public IActionResult Edit(Custom custom)
        {
            _action.Update(custom);
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
