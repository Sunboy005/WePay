using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> ChangeBaseCurrency(string currencyIdFrom, string currencyIdTo)
        {
            var result = await _serviceManager.WalletCurrencyService.ChangeBaseCurrency(currencyIdFrom, currencyIdTo);
            if (result == null)
            {
                return NotFound();
            }
            return Ok();

        }
        [HttpGet("getCurrencyById")]
        public async Task<IActionResult> GetCurrencyById([FromQuery] string Id)
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
        public async Task<IActionResult> AddCurrency([FromBody] WalletCurrencyAdditionDto walletCurrencyAdditionDto)
        {
            var user = await _serviceManager.UserService.GetUserByEmail(walletCurrencyAdditionDto.UserEmail);
            if (user == null)
            {
                return NotFound();
            }
            var userRole = user.Role;
            if (userRole == "Noob")
            {
                var wallet = await _serviceManager.WalletService.GetWalletByUserId(user.Id);
                
                if (wallet == null)
                {
                    return NotFound();
                }
                var currencies = wallet.WalletCurrencies;
                if (currencies.Count == 0)
                {
                    await _serviceManager.WalletCurrencyService.AddWalletCurrency(walletCurrencyAdditionDto);
                }
                else
                {
                    return BadRequest();
                }

            }
            else if (userRole == "Elite")
            {
                var wallet = await _serviceManager.WalletService.GetWalletByUserId(user.Id);
                if (wallet == null)
                {
                    return NotFound();
                }
                var currencies = wallet.WalletCurrencies;
                if (currencies.Count == 0)
                {
                    await _serviceManager.WalletCurrencyService.AddWalletCurrency(walletCurrencyAdditionDto);
                }
                else if (currencies.Count >= 1)
                {
                    await _serviceManager.WalletCurrencyService.AddWalletCurrency(walletCurrencyAdditionDto);
                }
            }
            var result = await _serviceManager.WalletCurrencyService.AddWalletCurrency(walletCurrencyAdditionDto);
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);


        }
        [HttpDelete("delete-currency")]
        [Authorize]
        public async Task<IActionResult> DeleteCurrency([FromBody] WalletCurrencyDeletionDto walletCurrencyDeletionDto)
        {
            var result = await _serviceManager.WalletCurrencyService.DeleteWalletCurrency(walletCurrencyDeletionDto);
            if (result == false)
            {
                return BadRequest();
            }
            return NoContent();

        }       

        [HttpGet("getCurrencyBalance")]
        public async Task<IActionResult> GetCurrencyBalance([FromQuery] string currencyId)
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
