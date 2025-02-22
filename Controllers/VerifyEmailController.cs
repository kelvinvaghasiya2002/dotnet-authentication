using Authentication.MailServices;
using Authentication.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VerifyEmailController : ControllerBase
    {
        private readonly IEmailSender emailSender;

        
        public VerifyEmailController(IEmailSender emailSender)
        {
            this.emailSender = emailSender;
        }

        [HttpGet]
        public IActionResult SendEmail([FromQuery] string email)
        {
            try
            {
                emailSender.SendEmail(email);
                return Ok("Email Sent Successfully");
            }
            catch (Exception ex) 
            { 
                return BadRequest(ex.Message);
            }
            
        }
    }
}
