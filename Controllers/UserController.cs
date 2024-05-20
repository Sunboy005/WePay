using Microsoft.AspNetCore.Mvc;
<<<<<<< HEAD
using wepay.Models;
=======
using wepay.Service.Interface;
>>>>>>> e06645cbc996d52b330e44d26e4e745bf8f4a647

namespace wepay.Controllers
{
    public class UserController : ControllerBase

    {
<<<<<<< HEAD
        [HttpGet]
        public async Task<User>GetUserById(Guid id)
        {
            var user = 
        }
=======
        private readonly IServiceManager _serviceManager;

        public UserController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

       
>>>>>>> e06645cbc996d52b330e44d26e4e745bf8f4a647
    }
}
