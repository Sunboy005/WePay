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
        [HttpGet("id", Name = "GetTransactionById")]
        [Authorize]
        public async Task<IActionResult> GetTransactionById([FromQuery] string id)
        {
            var transaction = await _serviceManager.TransactionService.GetTransactionById(id);

            if (transaction == null)
            {
                return NotFound();
            }

            return Ok(transaction);
        }
        [HttpPost("AddTransaction")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddTransaction([FromBody] TransactionDto transactionDto)
        {
            var wallet = await _serviceManager.WalletService.GetWalletByAddress(transactionDto.WalletId);
            if (wallet == null)
            {
                return NotFound();
            }

            var result = await _serviceManager.TransactionService.AddTransaction(transactionDto);
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);


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
