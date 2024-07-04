using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using wepay.Models;
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

            return Ok();

        }       


        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> LogOutUser([FromBody] string email)
        {
            await _serviceManager.AuthService.LogoutUser();
            return Ok();
        }

    }
}
