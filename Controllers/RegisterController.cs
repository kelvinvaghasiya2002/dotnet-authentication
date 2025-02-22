using Authentication.Data;
using Authentication.MailServices;
using Authentication.Models.Entities;
using Authentication.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly ApplicationDbContext dbcontext;
        private readonly IEmailSender emailSender;

        public RegisterController(ApplicationDbContext Dbcontext , IEmailSender emailSender)
        {
            dbcontext = Dbcontext;
            this.emailSender = emailSender;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            if(user == null || string.IsNullOrEmpty(user.Password))
            {
                return BadRequest("Invalid User Data");
            }
            var password = user.Password;
            try
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(password);
                dbcontext.Users.Add(user);
                await dbcontext.SaveChangesAsync();
                return Ok("User saved successfully!");
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"An error occured in saving user : {ex}");
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userDto, JwtTokenService jwtTokenService)
        {
            var user = await dbcontext.Users
                .FirstOrDefaultAsync(u => u.Email == userDto.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(userDto.Password, user.Password))
            {
                return Unauthorized("Invalid credentials.");
            }

            var token = jwtTokenService.GenerateToken(user.Id.ToString(), user.role);
            return Ok(new { Token = token });
        }

        public class UserLoginDto
        {
            public required string Email { get; set; }
            public required string Password { get; set; }
        }
    }
}
