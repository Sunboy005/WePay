using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using wepay.Models.DTOs;
using wepay.Service;
using wepay.Service.Interface;

namespace wepay.Controllers
{
    [Route("wepay/wallet")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public WalletController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateWallet([FromBody] WalletCreationDto walletcreationDto)
        {
            var user = await _serviceManager.UserService.GetUserById(walletcreationDto.UserId);
            if(user == null)
            {
                return NotFound(ModelState);
            }
            var result = await _serviceManager.WalletService.CreateWallet(walletcreationDto);

            return Created("WalletAddress", result.Address);
            

        }

        [HttpPost("lock/{walletId}")]
        public async Task<IActionResult> LockWallet(string walletId)
        {
            var result = await  _serviceManager.WalletService.LockWallet(walletId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok();

        }

        [HttpPost("enable/{walletId}")]
        public async Task<IActionResult> EnableWallet( string walletId)
        {
            var result = await _serviceManager.WalletService.EnableWallet(walletId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok();

        }


    }
}
