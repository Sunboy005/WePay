using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using wepay.Models;
using wepay.Models.DTOs;
using wepay.Service.Interface;

namespace wepay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TransactionController(IServiceManager serviceManager, IHttpContextAccessor httpContextAccessor)
        {
            _serviceManager = serviceManager;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("GetTransactionByWalletAddress")]
        public async Task<IActionResult> GetTransactionBywalletAddress([FromQuery] string address)
        {

            var history = await _serviceManager.TransactionService.GetTransactionsByWalletAddress(address);
            if (history == null)
            {
                return NotFound();
            }
            return Ok(history);
        }
    }
}
