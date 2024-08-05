  using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using secondProject.context;
using secondProject.Dtos;
using secondProject.Models;

namespace secondProject.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
         private readonly ApplicationDbContext _context;
        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Get(){
            var users = _context.Users.ToList();
            return Ok(users); 
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] UserDto user){
            var newUser = new User{
                Name = user.Name,
                Role = user.Role,
                UserName = user.UserName,
                Password = user.Password
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();
            return Ok("User Created Successfully!");
        }

    }
}