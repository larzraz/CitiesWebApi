using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CitiesWebApi.DataContext;
using CitiesWebApi.Model;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CitiesWebApi.Controllers
{
    [Route("api/[controller]")]
    public class AttractionController : Controller
    {
        // GET: api/<controller>
        private readonly CityDataContext _db;

        public AttractionController(CityDataContext db)
        {
            _db = db;
        }
        [HttpGet]

        [Route("all")]

        public IActionResult GetAllAttrations()
        {
            var Attrations = _db.Attractions;

            return Ok(Attrations);
        }
        [HttpGet]

        [Route("{cityID}")]
       
        public IActionResult GetAttrations(long cityID)
        {
            List<Attraction> Attrations = _db.Attractions.Where(x => x.CityID == cityID).ToList();

            return Ok(Attrations);
        }

        [HttpGet]

        [Route("{cityID}/{attractionID}")]
        public IActionResult GetAttrations(long cityID, long attractionID)
        {
            List<Attraction> Attration = _db.Attractions.Where(x => x.CityID == cityID && x.ID == attractionID).ToList();

            return Ok(Attration);
        }
        
       
    }
}
