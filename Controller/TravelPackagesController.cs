using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using secondProject.context;
using secondProject.Models;

namespace secondProject.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class TravelPackagesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public TravelPackagesController(ApplicationDbContext context)
        {
            _context = context;
            
        }

        [HttpGet]
        public IActionResult Get(){
            return Ok(_context.TravelPackages.ToList());
        }

        [HttpPost]
        public IActionResult Create([FromBody] TravelPackages travelPackages){
            _context.TravelPackages.Add(travelPackages);
            _context.SaveChanges();
            return Ok("Successfully added a new Travel Packages");
        }

        [HttpDelete]
        public IActionResult Delete(int id){
            var findTravelPackage = _context.TravelPackages.Find(id);
            if(findTravelPackage == null){
                return NotFound();
            }
            _context.TravelPackages.Remove(findTravelPackage);
            _context.SaveChanges();
            return Ok("Successfully deleted the travel Package.");
        }

        [HttpGet("{id}")]
        public IActionResult FindById(int id){
            var travelPackage = _context.TravelPackages.Find(id);
            if(travelPackage == null){
                return NotFound();
            }
            return Ok(travelPackage);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id,TravelPackages updatedTravelPackage){
            var travelPackage = _context.TravelPackages.Find(id);
            if(travelPackage == null){
                return NotFound();
            }

            travelPackage.Id = updatedTravelPackage.Id;
            travelPackage.Name = updatedTravelPackage.Name;
            travelPackage.ImageUrl = updatedTravelPackage.ImageUrl;
            travelPackage.Description = updatedTravelPackage.Description;
            
            _context.SaveChanges();
            return Ok("Successfully updated Travel Package.");
        }

        [HttpGet("search")]
        public IActionResult Search([FromQuery] string name){
            var travelPackages = _context.TravelPackages
                                .Where(t => t.Name.Contains(name))
                                .ToList();
            if(!travelPackages.Any()){
                return NotFound();
            }

            return Ok(travelPackages);

        }

    }
}