using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using secondProject.context;
using secondProject.Dtos.UserDTOs;
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
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<GetUserDto>>> GetUsers()
        {
            try
            {
                var users = await _context.Users.ToListAsync();
                var userDto = users.Select(u => new GetUserDto
                {
                    Id = u.Id,  
                    UserName = u.UserName,
                    Role = u.Role,
                    Email = u.Email,    
                    Password = u.Password
                });
                return Ok(userDto);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

        }

        [HttpGet("{UserId}")]
        public async Task<ActionResult<User>> GetUser(int UserId)
        {
            var user = await _context.Users.FindAsync(UserId);
            if (user == null)
            {
                return NotFound();
            }

            var userDto = new GetUserDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Role = user.Role,   
                Email = user.Email,
                Password = user.Password
            };

            return Ok(userDto);
        }

        [HttpPost("Create")]
        public async Task<ActionResult<User>> CreateUser([FromBody] UserDto user)
        {
            var emailExists = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
            if (emailExists != null) return BadRequest("The email already exists.");

            var userNameExists = await _context.Users.FirstOrDefaultAsync(u => u.UserName == user.UserName);
            if (userNameExists != null) return BadRequest("The userName already exists.");

            if (user.Password != user.ConformPassword) return BadRequest("Passwords do not match.");

            // Use BCrypt.Net.BCrypt.HashPassword to generate secure salt and hash
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);

            var newUser = new User
            {
                UserName = user.UserName,
                Email = user.Email,
                Role = user.Role,
                Password = hashedPassword,  // Store the complete hashed password (incorporates salt)
            };

            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();
            return Ok("User Created Successfully!");
        }


        [HttpDelete("{UserId}")]
        public async Task<ActionResult<User>> DeleteUser(int UserId)
        {
            var user = await _context.Users.FindAsync(UserId);

            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            _context.SaveChanges();
            return Ok("Deleted Successfully.");
        }

        [HttpPost("Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Ok(new { message = "Logged out successfully" });
        }
    }
}