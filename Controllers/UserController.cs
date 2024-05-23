using Microsoft.AspNetCore.Mvc;
using wepay.EmailService;
using wepay.Models;
using wepay.Models.DTOs;
using wepay.Service.Interface;


namespace wepay.Controllers
{
    [Route("wepay/user")]
    [ApiController]
    public class UserController : ControllerBase

    {
        private readonly IServiceManager _serviceManager;


        public UserController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }


        [HttpGet("id/{id}", Name = "GetUserById" )]
        public async Task<IActionResult> GetUserById( string id)
        {
            var identityUser = await _serviceManager.UserService.GetUserById(id);
            
            if (identityUser == null)
            {
                return NotFound();
            }

            return Ok(identityUser);
        }

        [HttpGet("email/{email}", Name = "GetUserByEmail")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var user = await _serviceManager.UserService.GetUserByEmail(email);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost("create")]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistrationDto)
        {
            var result = await _serviceManager.UserService.RegisterUser(userForRegistrationDto);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }

            return StatusCode(201);

        }
        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] UserForLoginDto userForLoginDto)
        {
            var result = await _serviceManager.UserService.LoginUser(userForLoginDto);
            if (result == false)
            {
                return Unauthorized();
            }

            return Ok(new { Token = _serviceManager.UserService.CreateToken() });

        }

        [HttpPost("password/change")]
        public async Task<IActionResult> ChangePassword([FromBody] UserForChangePasswordDto userForChangePasswordDto)
        {
            var result = await _serviceManager.UserService.ChangePassword(userForChangePasswordDto);

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
            var token = await _serviceManager.UserService.VerifyUserEmail(email, "");

            var confirmationLink = Url.Action(nameof(ConfirmUserEmail), "WePayAccount", new { token, email = email }, Request.Scheme);
            var message = new Message(new string[] { email }, "Wepay - Confirm Email Address. Click link to confirm your email address", confirmationLink);
            await _serviceManager.UserService.SendEmailAsync(message);
            return Ok("We have sent an email confirmation link to" + email);
        }

        [HttpPost("email/confirm")]
        public async Task<IActionResult> ConfirmUserEmail([FromBody] UserForEmailConfirmationDto userForEmailConfirmationDto)
        {
            var result = await _serviceManager.UserService.ConfirmUserEmail(userForEmailConfirmationDto);
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

        [HttpDelete("deleteuser")]
        public async Task<IActionResult> DeleteUser([FromBody]UserDeletionDto userDeletionDto)
        {
            var result = await _serviceManager.UserService.DeleteUser(userDeletionDto);
            if (result == false)
            {
                return BadRequest();
            }

            return NoContent();
        }

        [HttpPatch("updateuser/{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UserUpdateDto userUpdateDto)
        {
            var result = await _serviceManager.UserService.UpdateUserAsync(id, userUpdateDto);

            if (!result)
            {
                return NotFound("User not found or update failed.");
            }

            return NoContent();
        }
    }
}
    
