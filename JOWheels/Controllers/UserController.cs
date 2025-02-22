using JOWheels.Models;
using JOWheels.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JOWheels.Controllers
{
    public class UserController : Controller
    {

        private readonly ICRUDAction<User> _action;
        private readonly ICRUDAction<Governorate> _actionGovernorate;
        private readonly ICRUDAction<Region> _actionRegion;

        public UserController(ICRUDAction<User> action, ICRUDAction<Governorate> actionGovernorate, ICRUDAction<Region> actionRegion)
        {
            _action = action;
            _actionGovernorate = actionGovernorate;
            _actionRegion = actionRegion;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.GovernorateId = new SelectList(_actionGovernorate.List(), "Id", "Name");
            ViewBag.RegionId = new SelectList(_actionRegion.List(), "Id", "Name");
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var user = _action.GetBy(id);
            ViewBag.GovernorateId = new SelectList(_actionGovernorate.List(), "Id", "Name", user.GovernorateId);
            ViewBag.RegionId = new SelectList(_actionRegion.List(), "Id", "Name", user.RegionId);
            return View(_action.GetBy(id));
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            return View(_action.GetBy(id));
        }

    }
}
