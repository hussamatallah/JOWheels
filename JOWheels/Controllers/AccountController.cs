using JOWheels.Models;
using JOWheels.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Cryptography;
using System.Text;

namespace JOWheels.Controllers
{
    public class AccountController : Controller
    {

        private readonly JOWheelsDbContext _db;
        private readonly ICRUDAction<Governorate> _actionGovernorate;
        private readonly ICRUDAction<Region> _actionRegion;
        private readonly ICRUDAction<User> _user;

        public AccountController(JOWheelsDbContext db,
            ICRUDAction<Governorate> actionGovernorate,
            ICRUDAction<Region> actionRegion,
            ICRUDAction<User> user)
        {
            _db = db;
            _actionGovernorate = actionGovernorate;
            _actionRegion = actionRegion;
            _user = user;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            var usr = _db.Users.FirstOrDefault(u => u.Email == user.Email && u.Password == user.Password);
            if (usr != null)
            {
                HttpContext.Session.SetInt32("UserId", usr.Id);

                HttpContext.Session.SetString("UserName", usr.Name); // اعلن عن الخدمة program.cs

                HttpContext.Session.SetString("UserType", usr.UserType);

                if (usr.UserType == "Admin")
                {
                    return RedirectToAction("Index", "Car");
                }
                else if (usr.UserType == "Dealership")
                {
                    return RedirectToAction("Index", "Home");
                }
                else if (usr.UserType == "User")
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }

        public IActionResult Register()
        {
            ViewBag.GovernorateId = new SelectList(_actionGovernorate.List(), "Id", "Name");
            ViewBag.RegionId = new SelectList(_actionRegion.List(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            _user.Add(user);
            return RedirectToAction("Login");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        // For security part 2
        //private bool ValidatePassword(string enteredPassword, string storedPassword)
        //{
        //    string enteredPasswordHash = HashPassword(enteredPassword);
        //    return enteredPasswordHash == storedPassword;
        //}
        
        //private string HashPassword(string password)
        //{
        //    using (SHA256 sha256 = SHA256.Create())
        //    {
        //        byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        //        return Convert.ToBase64String(hashBytes); // convert string to hash
        //    }
        //}
    }
}
