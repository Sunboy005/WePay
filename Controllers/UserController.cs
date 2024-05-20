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
        }
       

       

    }
}
