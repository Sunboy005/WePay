using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using wepay.Models.DTOs;
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
        [Authorize]
        public async Task<IActionResult> CreateWallet([FromBody] WalletCreationDto walletcreationDto)
        {
            var user = await _serviceManager.UserService.GetUserById(walletcreationDto.UserId);
            if (user == null)
            {
                return NotFound(ModelState);
            }
            var result = await _serviceManager.WalletService.CreateWallet(walletcreationDto);

            return Created("WalletAddress", result.Address);
        }

        [HttpGet("id", Name = "GetWalletById")]
        [Authorize]
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
        [Authorize]
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
        [Authorize]
        public async Task<IActionResult> LockWallet(string walletId)
        {
            var result = await _serviceManager.WalletService.LockWallet(walletId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok();

        }

        [HttpPost("enable/{walletId}")]
        [Authorize]
        public async Task<IActionResult> EnableWallet(string walletId)
        {
            var result = await _serviceManager.WalletService.EnableWallet(walletId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok();

        }

        //[HttpPost("requesst-change-wallet-pin")]
        //public async Task<IActionResult> RequestChangeWalletPin([FromBody][FromBody] string email,)
        //{
        //    var user = await _serviceManager.UserService.GetUserByEmail(email);
        //    if (user == null)
        //    {
        //        return NotFound("No user found with email: " + email);
        //    }

        //    var otpRequestDto = new OtpRequestDto
        //    {
        //        Email = email,
        //        Reason = nameof(OtpReasons.PasswordChange)
        //    };
        //    var otpCode = await _serviceManager.OtpService.CreateNewOtp(otpRequestDto);
        //    if (string.IsNullOrEmpty(otpCode))
        //    {

        //        return BadRequest("We couldn't generate otp");
        //    }

        //    return Ok("We have sent an password change code to " + email + "with OTP code " + otpCode);
        //}


        [HttpPost("change-wallet-pin")]
        public async Task<IActionResult> ChangeWalletPin([FromBody] ChangeWalletPinDto changeWalletPinDto)
        {
            var result = await _serviceManager.WalletService.ChangeWalletPinAsync(changeWalletPinDto);
            if (!result)
            {
                return BadRequest("Failed to change PIN, the current PIN might be incorrect or the wallet does not exist.");
            }
            return Ok("PIN changed successfully");
        }



        [HttpGet("userId", Name = "GetWalletByUserId")]
        [Authorize]
        public async Task<IActionResult> GetWalletByUserId(string userId)
        {
            var user = await _serviceManager.UserService.GetUserById(userId);

            if (user == null)
            {
                return NotFound("User with " + userId + "does not exist");
            }

            var wallet = await _serviceManager.WalletService.GetWalletByUserId(userId);
            if (wallet == null)
            {
                return NotFound("User with " + userId + "does not have a wallet");
            }

            return Ok(wallet);

        }

        [HttpGet("balance/{walletId}")]
        [Authorize]
        public async Task<IActionResult> GetWalletBalance(string walletId)
        {
            var wallet = await _serviceManager.WalletService.GetWalletById(walletId);
            if (wallet == null)
            {
                return NotFound("No wallet found");
            }
            var currencies = await _serviceManager.CurrencyService.GetCurrencyListByWalletAddress(walletId);
            if (currencies.IsNullOrEmpty())
            {
                return NotFound("No currencies found");
            }

            var balance = _serviceManager.WalletService.GetWalletBallance(currencies);

            return Ok(balance);
        }

        [HttpPost("transfer-within-wallet")]
        [Authorize]
        public async Task<IActionResult> TransferMoneyWithinWallet([FromBody] TransferWithinWalletDto transferWithinWalletDto)
        {
            var wallet = await _serviceManager.WalletService.GetWalletByAddress(transferWithinWalletDto.WalletAddress);
            if (wallet == null)
            {
                return BadRequest();
            }
            var currencyFrom = await _serviceManager.CurrencyService.GetCurrencyByShortCodeForAWallet(wallet.Address, transferWithinWalletDto.CurrencyFromShortCode);
            var currentyTo = await _serviceManager.CurrencyService.GetCurrencyByShortCodeForAWallet(wallet.Address, transferWithinWalletDto.CurrencyToShortCode);
            if (currencyFrom == null || currentyTo == null)
            {
                return BadRequest();
            }

            if (currencyFrom.Balance < transferWithinWalletDto.Amount)
            {
                return BadRequest();
            }
            var result = await _serviceManager.WalletService.TransferMoneyWithinWallet(currencyFrom, currentyTo, transferWithinWalletDto.Amount);
            if (result == true)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpGet("Name/Address")]
        public async Task<IActionResult> GetUserByWalletAddress([FromQuery] string address)
        {
            var user = await _serviceManager.WalletService.GetUserByWalletAddress(address); 
            if(user == null)
            {
                return NotFound();
            }
            return Ok(user);
        } 


    }
}
