using Microsoft.AspNetCore.Mvc;

using wepay.Models;
using wepay.Service.Interface;


namespace wepay.Controllers
{
    public class UserController : ControllerBase

    {
        private readonly IServiceManager _serviceManager;

        public UserController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

<<<<<<< HEAD
        [HttpGet]
        public async Task<IActionResult> GetUserById([FromBody] Guid id)
        {
            var user = _serviceManager.UserService.GetUserById(id); 
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
         }

        [HttpGet]
        public async Task<IActionResult> GetUserByEmail([FromBody] string email)
        {
            var user = _serviceManager.UserService.GetUserByEmail(email); 
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
=======
        [HttpPost]

        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistrationDto)
        {
            var result = await _serviceManager.UserService.RegisterUser(userForRegistrationDto);

            if(!result.Succeeded)
            {
                foreach(var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }

            return StatusCode(201);
>>>>>>> 9af1d20c4e9b8549e9a8c18e1d88120a8bce5026
        }
       

       

    }
}
