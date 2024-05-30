using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using wepay.Service.Interface;

namespace wepay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public CurrencyController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }
    }
}
