using Entities.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using wepay.Models;
using wepay.Models.DTOs;
using wepay.Service.Interface;

namespace wepay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletCurrencyController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        public WalletCurrencyController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpPost("changebasecurrency")]
        [Authorize]
        public async Task<IActionResult> ChangeBaseWalletCurrency([FromBody] ChangeBaseCurrencyDto changeCurrencyDto)
        {
            var wallet = await _serviceManager.WalletService.GetWalletByAddress(changeCurrencyDto.WalletAddress);
            if (wallet == null)
            {
                throw new BadRequestException("Wallet not found");
            }

            await _serviceManager.WalletCurrencyService.ChangeBaseCurrency(wallet, changeCurrencyDto);         
            return Ok();

        }
        [HttpGet("getCurrencyById")]
        [Authorize]
        public async Task<IActionResult> GetWalletCurrencyById([FromQuery] string Id)
        {
            var walletcurrency = await _serviceManager.WalletCurrencyService.GetWalletCurrencyById(Id);
            if (walletcurrency == null)
            {
                return NotFound();
            }
            return Ok(walletcurrency);
        }
        [HttpPost("Add-Currency")]
        [Authorize]
        public async Task<IActionResult> AddWalletCurrency([FromBody] WalletCurrencyAdditionDto walletCurrencyAdditionDto)
        {
            var user = await _serviceManager.UserService.GetUserByEmail(walletCurrencyAdditionDto.UserEmail);
            if (user == null)
            {
                throw new BadRequestException("User not found");
            }
            
            if(user.Wallet == null)
            {
                throw new BadRequestException("Wallet not found");
            }

            if(user.Wallet.Address != walletCurrencyAdditionDto.WalletAddress)
            {
                throw new BadRequestException("Wallet does not belong to user");
            }

            var userRole = await _serviceManager.UserService.GetRoleOfUser(user);

            if(userRole == "Admin") {
                throw new BadRequestException("Admin cannot have a wallet currency");
            }
            
            await _serviceManager.WalletCurrencyService.AddWalletCurrency(userRole, user, walletCurrencyAdditionDto.ShortCode, user.Wallet.Id);

            return Ok();
        }

        [HttpDelete("delete-currency")]
        [Authorize]
        public async Task<IActionResult> DeleteWalletCurrency([FromBody] WalletCurrencyDeletionDto walletCurrencyDeletionDto)
        {
            await _serviceManager.WalletCurrencyService.DeleteWalletCurrency(walletCurrencyDeletionDto.WalletAddress, walletCurrencyDeletionDto.CurrencyCode);          
            return NoContent();

        }       

        [HttpGet("getCurrencyBalance")]
        [Authorize]
        public async Task<IActionResult> GetWalletCurrencyBalance([FromQuery] string currencyId)
        {
            var currency = await _serviceManager.WalletCurrencyService.GetWalletCurrencyBalance(currencyId);
            if (currency == null)
            {
                return NotFound();
            }
            return Ok(currency);
        }
    }

}
