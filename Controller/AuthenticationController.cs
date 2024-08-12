using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using secondProject.context;
using secondProject.Dtos.UserDTOs;

namespace secondProject.Controller
{
     [ApiController]
     [Route("api/[Controller]")]
    public class AuthenticationController:ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AuthenticationController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> UserLogin([FromBody] LoginDto loginDto)
        {
            try
            {
                var user = await _context.Users.SingleOrDefaultAsync(u => u.UserName == loginDto.UserName);

                if (user == null)
                {
                    return BadRequest("Username does not exist.");
                }

                // Verify password using BCrypt.Net.BCrypt.Verify:
                bool isPasswordValid = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password);
                if (!isPasswordValid)
                {
                    return BadRequest("Password is incorrect.");
                }

                return Ok("User signed in successfully!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
