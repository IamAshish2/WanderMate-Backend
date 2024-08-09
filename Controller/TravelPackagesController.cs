using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using secondProject.context;
using secondProject.Dtos;
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
        public async Task<ActionResult<IEnumerable<TravelPackages>>> Get(){
            try
            {
                var travelPackages =await  _context.TravelPackages.ToListAsync();
                return Ok(travelPackages);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
    }

        //[HttpPost]
        //public async Task<ActionResult<TravelPackages>> Create([FromBody] TravelPackagesDto travelPackagesDto)
        //{
        //    try
        //    {
        //        var travelPackages = new TravelPackages
        //        {
        //            Name = travelPackagesDto.Name,
        //            ImageUrl = travelPackagesDto.ImageUrl,
        //            Description = travelPackagesDto.Description
        //        };

        //        _context.TravelPackages.Add(travelPackages);
        //        await _context.SaveChangesAsync();
        //        return Ok("Successfully created Travel package.");
        //    }
        //    catch (Exception e)
        //     {
        //            return BadRequest(e.Message);
        //      }

        //}
        [HttpPost]
        public  async Task<ActionResult<TravelPackages>> Create([FromBody] TravelPackages travelPackages)
        {
            try
            {
               if(travelPackages == null)
                {
                    return NotFound();
                }
                _context.TravelPackages.Add(travelPackages);
                await _context.SaveChangesAsync();
                return Ok("success.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Internal server error: {ex.Message}");
            }
        }


       [HttpDelete("{id}")]
        public async Task<ActionResult<TravelPackages>> Delete(int id){
            try
            {
                var findTravelPackage = await _context.TravelPackages.FindAsync(id);
                if (findTravelPackage == null)
                {
                    return NotFound();
                }
                _context.TravelPackages.Remove(findTravelPackage);
                await _context.SaveChangesAsync();
                return Ok("Successfully deleted the travel Package.");
            }
            catch (Exception ex) {
                return BadRequest( $"Internal server error: {ex.Message}");
            }
           
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TravelPackages>> Update(int id,TravelPackages updatedTravelPackage){
            try
            {
                var travelPackage = await _context.TravelPackages.FindAsync(id);
                if (travelPackage == null)
                {
                    return NotFound();
                }

                //travelPackage.Id = updatedTravelPackage.Id;
                travelPackage.Name = updatedTravelPackage.Name;
                travelPackage.ImageUrl = updatedTravelPackage.ImageUrl;
                travelPackage.Description = updatedTravelPackage.Description;

               await  _context.SaveChangesAsync();
                return Ok("Successfully updated Travel Package.");
            }
            catch (Exception ex) {
                return StatusCode(500,$"Error: {ex.Message}");
            }
          
        }

        [HttpGet("search")]
        public  IActionResult Search([FromQuery] string name){
            try
            {
                var travelPackages =   _context.TravelPackages
                              .Where(t => t.Name.Contains(name));
                              
                if (!travelPackages.Any())
                {
                    return NotFound();
                }

                return Ok(travelPackages);
            }
            catch (Exception ex) {
                return BadRequest($"Error: {ex.Message}");
            }
        }

    }
}