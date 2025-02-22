using JOWheels.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JOWheels.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {

        private readonly JOWheelsDbContext _db;

        public CarsController(JOWheelsDbContext db)
        {
            _db = db;
        }

        [HttpGet("{id}")]
        public IActionResult GetBodyDataByBrandId(int id)
        {
            var list = _db.Bodies.Where(c => c.BrandId == id).Select(bran => new
            {
                id = bran.Id,
                name = bran.Name
            });

            return Ok(list);
        }

    }
}
