using System.Linq;
using Microsoft.AspNetCore.Mvc;
using StarChart.Data;

namespace StarChart.Controllers
{
    [Route("")]
    [ApiController]
    public class CelestialObjectController: ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CelestialObjectController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id:int}", Name = "GetById")]
        public IActionResult GetById(int id)
        {
            var celestialObject = _context.CelestialObjects
                .FirstOrDefault(co => co.Id == id);

            if (celestialObject == null)
            {
                return NotFound();
            }

            var orbitedObject = _context.CelestialObjects
                .FirstOrDefault(oo => oo.Id == celestialObject.OrbitedObjectId);
            
            if (orbitedObject != null)
            {
                orbitedObject.Satellites.Add(celestialObject);
            }

            return Ok(celestialObject);
        }

        [HttpGet("{name}", Name = "GetByName")]
        public IActionResult GetByName(string name)
        {
            var celestialObjects = _context.CelestialObjects
                .Where(co => co.Name == name)
                .ToList();

            if (celestialObjects.Count == 0)
            {
                return NotFound();
            }

            foreach (var celestialObject in celestialObjects)
            {
                var orbitedObject = _context.CelestialObjects
                    .FirstOrDefault(oo => oo.Id == celestialObject.OrbitedObjectId);
            
                if (orbitedObject != null)
                {
                    orbitedObject.Satellites.Add(celestialObject);
                }
            }
            
            return Ok(celestialObjects);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var celestialObjects = _context.CelestialObjects
                .ToList();

            if (celestialObjects.Count == 0)
            {
                return NotFound();
            }

            foreach (var celestialObject in celestialObjects)
            {
                var orbitedObject = _context.CelestialObjects
                    .FirstOrDefault(oo => oo.Id == celestialObject.OrbitedObjectId);
            
                if (orbitedObject != null)
                {
                    orbitedObject.Satellites.Add(celestialObject);
                }
            }
            
            return Ok(celestialObjects);
        }
    }
}
