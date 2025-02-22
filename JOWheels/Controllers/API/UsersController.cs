using JOWheels.Models;
using JOWheels.Models.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JOWheels.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly ICRUDAction<User> _action;
        private readonly JOWheelsDbContext _region;

        public UsersController(ICRUDAction<User> action, JOWheelsDbContext region)
        {
            _action = action;
            _region = region;
        }

        [HttpGet]
        public IActionResult Index()    //api/user
        {
            var users = _action.List().Select(user => new
            {
                id = user.Id,
                name = user.Name,
                email = user.Email,
                userType = user.UserType,
                password = user.Password,
                phoneNumber = user.PhoneNumber,
                governorate = user.Governorate.Name,
                region = user.Region.Name
            });
            return Ok(users);
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            _action.Add(user);
            return Ok();
        }

        [HttpPut]
        public IActionResult Edit(User user)
        {
            _action.Update(user);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _action.Delete(id);
            return NoContent();
        }

        [HttpGet("{id}")]
        public IActionResult GetRegionDataByGovernorateId(int id)
        {
            var list = _region.Regions.Where(c => c.GovernorateId == id).Select(user => new
            {
                id = user.Id,
                name = user.Name
            });

            return Ok(list);
        }
    }
}