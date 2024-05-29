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

        [HttpGet("id", Name = "GetWalletById")]
        public async Task<IActionResult> GetWalletById([FromQuery] string id)
        {
            var wallet = await _serviceManager.WalletService.GetWalletById(id);

            if (wallet == null)
            {
                return NotFound();
            }

            return Ok(wallet);
        }

        [HttpGet("address", Name = "GetWalletByAddress")]
        public async Task<IActionResult> GetWalletByAddress([FromQuery] string address)
        {
            var wallet = await _serviceManager.WalletService.GetWalletByAddress(address);

            if (wallet == null)
            {
                return NotFound();
            }

            return Ok(wallet);
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
        [HttpPost("change-pin")]
        public async Task<IActionResult> ChangeWalletPin([FromBody] ChangeWalletPinDto changeWalletPinDto)
        {
            var result = await _serviceManager.WalletService.ChangePinAsync(changeWalletPinDto);
            if (!result)
            {
                return BadRequest("Failed to change PIN, the current PIN might be incorrect or the wallet does not exist.");
            }
            return Ok("PIN changed successfully");
        }



        [HttpGet("userId", Name ="GetWalletByUserId")]
        public async Task<IActionResult> GetWalletByUserId(string userId)
        {
            var user = await _serviceManager.UserService.GetUserById(userId);

            if (user == null)
            {
                return NotFound("User with " + userId + "does not exist");
            }

            var wallet = await _serviceManager.WalletService.GetWalletByUserId(userId);
            if(wallet == null)
            {
                return NotFound("User with " + userId + "does not have a wallet");
            }

            return Ok(wallet);

        }


    }
}
