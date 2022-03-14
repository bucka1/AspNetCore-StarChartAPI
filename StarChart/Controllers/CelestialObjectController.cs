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

        [HttpGet("{id:int}", Name = "GetById")]
        public IActionResult GetById (int Id)
        {
            var CelestialObject = _context.CelestialObjects.Find(Id);

            if (CelestialObject == null )
            {
                return NotFound();
            }
            else
            {
                CelestialObject.Satellites = _context.CelestialObjects.Where(e => e.OrbitedObjectId == Id).ToList();
                return Ok(CelestialObject);
            }
        }

        [HttpGet("{name}")]
        public IActionResult GetByName(string name)
        {
            var CelestialObject = _context.CelestialObjects.Where(n => n.Name == name).ToList();

            if(!CelestialObject.Any())
            {
                return NotFound();
            }

            foreach(var celestialObject in CelestialObject)
            {
                celestialObject.Satellites = _context.CelestialObjects.Where(e => e.OrbitedObjectId == celestialObject.Id).ToList();
            }
            return Ok(CelestialObject);

        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var celestialObjects = _context.CelestialObjects.ToList();

            foreach(var celestialObject in celestialObjects)
            {
                celestialObject.Satellites = _context.CelestialObjects.Where(e => e.OrbitedObjectId == celestialObject.Id).ToList();
            }

            return Ok(celestialObjects);
        }

    }
}
