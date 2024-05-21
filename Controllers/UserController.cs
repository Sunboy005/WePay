using Microsoft.AspNetCore.Mvc;

using wepay.Models;
using wepay.Service.Interface;


namespace wepay.Controllers
{
    [Route("wepay/authentication")]
    [ApiController]
    public class UserController : ControllerBase

    {
        private readonly IServiceManager _serviceManager;

      
        public UserController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }


        [HttpGet("getuserbyid")]
        public async Task<IActionResult> GetUserById([FromBody] string id)
        {
            var user = _serviceManager.UserService.GetUserById(id); 
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
         }

        [HttpGet("getuserbyemail")]
        public async Task<IActionResult> GetUserByEmail([FromBody] string email)
        {
            var user = _serviceManager.UserService.GetUserByEmail(email);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost("createuser")]
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
    }
}
    
