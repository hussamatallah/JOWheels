using JOWheels.Models;
using JOWheels.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
//using System.Drawing;

namespace JOWheels.Controllers
{
    public class RegionController : Controller
    {

        private readonly ICRUDAction<Region> _action;
        private readonly ICRUDAction<Governorate> _actionGovernorate;

        public RegionController(ICRUDAction<Region> action, ICRUDAction<Governorate> actionGovernorate)
        {
            _action = action;
            _actionGovernorate = actionGovernorate;
        }

        public IActionResult Index()
        {
            return View(_action.List());
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.GovernorateId = new SelectList(_actionGovernorate.List(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Region region)
        {
            _action.Add(region);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var region = _action.GetBy(id);
            ViewBag.GovernorateId = new SelectList(_actionGovernorate.List(), "Id", "Name", region.GovernorateId);
            return View(_action.GetBy(id));
        }

        [HttpPost]
        public IActionResult Edit(Region region)
        {
            _action.Update(region);
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
