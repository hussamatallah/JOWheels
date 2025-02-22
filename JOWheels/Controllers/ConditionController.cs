using JOWheels.Models.Repository;
using JOWheels.Models;
using Microsoft.AspNetCore.Mvc;

namespace JOWheels.Controllers
{
    public class ConditionController : Controller
    {

        private readonly ICRUDAction<Condition> _action;

        public ConditionController(ICRUDAction<Condition> action)
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
        public IActionResult Create(Condition condition)
        {
            _action.Add(condition);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View(_action.GetBy(id));
        }

        [HttpPost]
        public IActionResult Edit(Condition condition)
        {
            _action.Update(condition);
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