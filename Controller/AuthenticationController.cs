
using Microsoft.AspNetCore.Http.HttpResults;
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
        public async Task<IActionResult> LoginUser([FromBody] LoginDto login)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.UserName == login.UserName);
            if (user == null) return BadRequest("The username does not exist.");

            bool isPassValid = BCrypt.Net.BCrypt.Verify(login.Password, user.Password);
            if (!isPassValid) return BadRequest("The password does not match.");

            return Ok("Login Successful.");
        }


    }
}
