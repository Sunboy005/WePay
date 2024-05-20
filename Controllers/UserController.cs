using Microsoft.AspNetCore.Mvc;
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

       
    }
}
