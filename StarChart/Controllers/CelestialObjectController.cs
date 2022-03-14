using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StarChart.Data;

namespace StarChart.Controllers
{
    [Route("")]
    [ApiController]
    public class CelestialObjectController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CelestialObjectController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById (int Id)
        {
            var Satellites = _context.CelestialObjects.FirstOrDefault(i => i.OrbitedObjectId == Id);

            if (Satellites != null )
            {
                return Ok(Satellites);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("{name}")]
        public IActionResult GetByName(string name)
        {
            var Satellites = _context.CelestialObjects.FirstOrDefault(n => n.Name == name);

            if(Satellites != null)
            {
                return Ok(name);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var Satellites = _context.CelestialObjects.ToList();

            return Ok(Satellites);
        }

    }
}
