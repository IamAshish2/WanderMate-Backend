using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using secondProject.context;
using secondProject.Dtos.UserDTOs;
using secondProject.Service;

namespace secondProject.Controller
{
     [ApiController]
     [Route("api/[Controller]")]
    public class AuthenticationController:ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly TokenService _tokenService;

        public AuthenticationController(ApplicationDbContext context,TokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        //[HttpPost("Login")]
        //public async Task<IActionResult> UserLogin([FromBody] LoginDto loginDto)
        //{
        //    try
        //    {
        //        var user = await _context.Users.SingleOrDefaultAsync(u => u.UserName == loginDto.UserName);

        //        if (user == null)
        //        {
        //            return BadRequest("Username does not exist.");
        //        }

        //        // Verify password using BCrypt.Net.BCrypt.Verify:
        //        bool isPasswordValid = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password);
        //        if (!isPasswordValid)
        //        {
        //            return BadRequest("Password is incorrect.");
        //        }
        //        //var token = _tokenService.GenerateToken(user);
        //        //return Ok(token);
        //        var token = _tokenService.GenerateToken(user);

        //        // Store the token and user data in the session
        //        HttpContext.Session.SetString("AuthToken", token);
        //        HttpContext.Session.SetString("UserName", user.Id.ToString());
        //        HttpContext.Session.SetString("UserName", user.UserName);
        //        HttpContext.Session.SetString("UserRole", user.Role);

        //        return Ok(new { token });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, ex.Message);
        //    }
        //}

        
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

                // Verify password using BCrypt.Net.BCrypt.Verify
                bool isPasswordValid = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password);
                if (!isPasswordValid)
                {
                    return BadRequest("Password is incorrect.");
                }

                // Generate token
                var token = _tokenService.GenerateToken(user);

                // Store the token and user data in the session
                HttpContext.Session.SetString("AuthToken", token);
                HttpContext.Session.SetString("Id", user.Id.ToString());  // Storing UserId instead of UserName
                HttpContext.Session.SetString("UserName", user.UserName);
                HttpContext.Session.SetString("UserRole", user.Role);

                return Ok(new { token });
            }
            catch (Exception ex)
            {
                // Log the exception (optional)
                // _logger.LogError(ex, "An error occurred during login.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [Authorize]
        [HttpGet("verify-token")]
        public IActionResult VerifyToken()
        {
            return Ok("User Authorized");
        }

    }
}
