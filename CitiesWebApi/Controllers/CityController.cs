using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CitiesWebApi.DataContext;
using CitiesWebApi.Model;
using CitiesWebApi.Model.DTO;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CitiesWebApi.Controllers
{
    [Produces("application/json", "application/xml")]
    [Route("api/[controller]")]
    public class CityController : Controller
    {

        private readonly CityDataContext _db;
        private readonly IMapper _mapper;

        public CityController(CityDataContext db, IMapper map)
        {
            _db = db;
            _mapper = map;
        }
        // GET: api/<controller>
        [HttpGet]
        [Route("allCities")]
        public IActionResult GetCities(bool showAttractions = false)
        {
            var citiesWA = _db.Cities.Include("Attractions").Select(x => _mapper.Map<CityWithAttractionsDTO>(x)).ToList();

            if (!showAttractions)
            {
                var cities = _db.Cities.Select(x => _mapper.Map<CityDTO>(x)).ToList();
                return Ok(cities);

            }
            return Ok(citiesWA);
           
           
            
           
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetCity(int id,bool showAttractions = false)
        {
            var city = _db.Cities.Select(x=> _mapper.Map<CityDTO>(x)).FirstOrDefault(x => x.ID == id);
            if(city is null)
            {
                return NotFound();
            }

            if (showAttractions)
            {
                var citiesWA = _db.Cities.Include("Attractions").Select(x => _mapper.Map<CityWithAttractionsDTO>(x)).FirstOrDefault(x => x.ID == id);
                
                return Ok(citiesWA);
            }

            return Ok(city);



        }
       

        // POST api/<controller>
        [HttpPost]
        [Route("Create")]
        public IActionResult CreateCity([FromBody]CityDTO newCity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            City createdCity = _mapper.Map<City>(newCity); 
            _db.Add(createdCity);
            _db.SaveChanges();
            return CreatedAtAction("GetCity", new { id = createdCity.ID }, createdCity);
        }

        [HttpPut]
        [Route("Update/{cityId:int}")]
        public IActionResult UpdateCity([FromBody]City city, int cityId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _db.Cities.Update(city);
            _db.SaveChanges();
            return Accepted();
        }


        // DELETE api/<controller>/5
        [HttpDelete("Delete/{id:int}")]
      
        public IActionResult DeleteCity(int id)
        {
            City city = _db.Cities.FirstOrDefault(x => x.ID == id);

            if (city == null)
            {
                return BadRequest();
            }
            _db.Cities.Remove(city);
            _db.SaveChanges();
            return Accepted();
        }
        [HttpPatch("{id:int}")]
        public IActionResult UpdateSpecific([FromBody]JsonPatchDocument<City> Patch, int id)
        {
            City city = _db.Cities.FirstOrDefault(x => x.ID == id);
            Patch.ApplyTo(city, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var model = new
            {
                original = city,
                patched = Patch
            };
            _db.Cities.Update(city);
            _db.SaveChanges();
            return Ok(model);

        }


    }




}
