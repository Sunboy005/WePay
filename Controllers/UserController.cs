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


        [HttpGet("id", Name = "GetUserById" )]
        public async Task<IActionResult> GetUserById( [FromQuery] string id)
        {
            var identityUser = await _serviceManager.UserService.GetUserById(id);
            
            if (identityUser == null)
            {
                return NotFound();
            }

            return Ok(identityUser);
        }

        [HttpGet("email", Name = "GetUserByEmail")]
        public async Task<IActionResult> GetUserByEmail([FromQuery] string email)
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


        [HttpPost("create/admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] AdminForRegistrationDto adminForRegistrationDto)
        {
            var result = await _serviceManager.UserService.RegisterAdmin(adminForRegistrationDto);

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
    }
}
    
