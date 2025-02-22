using JOWheels.Models;
using JOWheels.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;

namespace JOWheels.Controllers
{
    public class PaymentController : Controller
    {

        private readonly ICRUDAction<Payment> _action;

        public PaymentController(ICRUDAction<Payment> action)
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
        public IActionResult Create(Payment payment)
        {
            _action.Add(payment);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View(_action.GetBy(id));
        }

        [HttpPost]
        public IActionResult Edit(Payment payment)
        {
            _action.Update(payment);
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
