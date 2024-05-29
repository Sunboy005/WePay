using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using wepay.EmailService;
using wepay.Models.DTOs;
using wepay.Service.Interface;

namespace wepay.Controllers
{
    [Route("wepay/auth")]
    [ApiController]

    public class AuthenticationController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public AuthenticationController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] UserForLoginDto userForLoginDto)
        {
            var result = await _serviceManager.AuthService.LoginUser(userForLoginDto);
            if (result == false)
            {
                return Unauthorized();
            }

            return Ok(new { Token = _serviceManager.AuthService.CreateToken() });

        }

        [HttpPost("password/change")]
        public async Task<IActionResult> ChangePassword([FromBody] UserForChangePasswordDto userForChangePasswordDto)
        {
            var result = await _serviceManager.AuthService.ChangePassword(userForChangePasswordDto);

            if (result.Item1 == false)
            {
                foreach (var error in result.Item2.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);

            }

            return Ok();
        }

        [HttpPost("email/verify/{email}")]
        public async Task<IActionResult> VerifyUserEmail(string email)
        {
            var token = await _serviceManager.AuthService.VerifyUserEmail(email, "");

            var confirmationLink = Url.Action(nameof(ConfirmUserEmail), "WePayAccount", new { token, email = email }, Request.Scheme);
            var message = new Message(new string[] { email }, "Wepay - Confirm Email Address. Click link to confirm your email address", confirmationLink);
            await _serviceManager.AuthService.SendEmailAsync(message);
            return Ok("We have sent an email confirmation link to" + email);
        }

        [HttpPost("email/confirm")]
        public async Task<IActionResult> ConfirmUserEmail([FromBody] UserForEmailConfirmationDto userForEmailConfirmationDto)
        {
            var result = await _serviceManager.AuthService.ConfirmUserEmail(userForEmailConfirmationDto);
            if (result.Succeeded)
            {
                return Ok();

            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }

        }
    }
}
