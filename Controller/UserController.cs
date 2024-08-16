using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using secondProject.context;
using secondProject.Dtos;
using secondProject.Dtos.UserDTOs;
using secondProject.Models;
using secondProject.Service;

namespace secondProject.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly EmailService _emailService;
        private readonly PasswordReset _passwordReset;

        public UserController(ApplicationDbContext context,EmailService emailService)
        {
            _context = context;
            _emailService = emailService;
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

            //if (user.Password != user.ConformPassword) return BadRequest("Passwords do not match.");

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
            //return Ok(new { Message = "User Created Successfully!" })
            return Ok(new { Message = "User Created Successfully!" });

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

        [HttpPost("Forgot Password")]
        public async Task<ActionResult> ForgotPassword([FromBody] ForgotPasswordDto model)    
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == model.Email);
            if (user == null)  return NotFound();

            // Simulate generating a password reset token (In reality, you'd generate a secure, unique token)
            var resetToken = Guid.NewGuid().ToString();   // Generate a secure token

            var emailBody = $"This is your Reset Token: {resetToken}";
             await _emailService.SendEmailAsync(user.Email, "Password Reset", emailBody);


            // // Store the reset token with the user's information in a secure way (e.g., in a database)
            _context.PasswordResets.Add(new PasswordReset { Token = resetToken });
            await _context.SaveChangesAsync();


            return Ok("A password reset link has been sent.");

        }


        [HttpPost("Update password")]
        public async Task<ActionResult> UpdatePassWord([FromBody] UpdatePasswordDto model)
        {
            var token = await _context.PasswordResets.Where(p => p.Token == model.Token).FirstOrDefaultAsync();
            if (token == null) return BadRequest("The token did not match");

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);

            var findUser = await _context.Users.Where(u => u.Email == model.Email).FirstOrDefaultAsync();
            if(findUser == null) return BadRequest("The email was not found!");

            // Update the user's password
            findUser.Password = hashedPassword;

            // Remove the used reset token or mark it as used
            _context.PasswordResets.Remove(token);

            // Save changes to the database
            await _context.SaveChangesAsync();

            return Ok("Password updated successfully.");

        }
    }
}