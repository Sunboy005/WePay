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
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var user = _serviceManager.UserService.GetUserById(id); 
            return Ok(user);
         }
        
       

       

    }
}
