using Microsoft.AspNetCore.Mvc;

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


        [HttpGet("{id}", Name = "GetUserById" )]
        public async Task<IActionResult> GetUserById( string id)
        {
            var user = _serviceManager.UserService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet("{email}", Name = "GetUserByEmail")]
        public async Task<IActionResult> GetUserByEmail( string email)
        {
            var user = _serviceManager.UserService.GetUserByEmail(email);
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
    }
}
    
