using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using wepay.Models.DTOs;
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

        [HttpPost("changebasecurrency")]
        [Authorize]
        public async Task<IActionResult> ChangeBaseCurrency(string currencyIdFrom, string currencyIdTo)
        {
            var result = await _serviceManager.WalletCurrencyService.ChangeBaseCurrency(currencyIdFrom,currencyIdTo);
            if (result == null)
            {
                return NotFound();
            }
            return Ok();

        }
        [HttpGet("getCurrencyById")] 
        public async Task<IActionResult> GetCurrencyById([FromQuery] string currencyId)
        {
           var currency = await _serviceManager.CurrencyService.GetCurrencyById(currencyId);
            if (currency == null)
            {
                return NotFound();
            }
            return Ok(currency);
        }
        [HttpPost("Add-Currency")]
        [Authorize]
        public async Task<IActionResult> AddCurrency([FromBody] CurrencyToAddDto currencyToAddDto)
        {
            var user = await _serviceManager.UserService.GetUserByEmail(currencyToAddDto.UserEmail);
            if (user == null)
            {
                return NotFound();
            }
            var userRole = user.Role;
            if(userRole == "Noob")
            {
                var wallet = await _serviceManager.WalletService.GetWalletByUserId(user.Id);
                if (wallet == null)
                {
                    return NotFound();
                }
                var currencies = await _serviceManager.CurrencyService.GetCurrencyListByWalletId(wallet.WalletId);
                if(currencies.Count == 0)
                {
                    await _serviceManager.CurrencyService.AddCurrency(currencyToAddDto);
                }
                else
                {
                    return BadRequest();
                }
                
            }
            else if (userRole == "Elite")
            {
                var wallet = await _serviceManager.WalletService.GetWalletByUserId(user.Id);
                if(wallet == null)
                {
                    return NotFound();
                }
                var currencies = await _serviceManager.CurrencyService.GetCurrencyListByWalletId(wallet.WalletId);
                if(currencies.Count == 0)
                {
                    await _serviceManager.CurrencyService.AddCurrency(currencyToAddDto);
                }
                else if(currencies.Count >= 1)
                {
                    await _serviceManager.CurrencyService.AddCurrency(currencyToAddDto);
                }
            }
            var result = await _serviceManager.CurrencyService.AddCurrency(currencyToAddDto);
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);
            
         
        }        
        [HttpDelete("delete-currency")]
        [Authorize]
        public async Task<IActionResult> DeleteCurrency([FromBody] CurrencyDeletionDto currencyDeletionDto)
        {
            var result = await _serviceManager.CurrencyService.DeleteCurrency(currencyDeletionDto);
            if (result == false)
            {
                return BadRequest();
            }
            return NoContent();

        }

        [HttpGet("getCurrencyListByWalletId")]
        [Authorize]
        public async Task<IActionResult> GetCurrencyListByWalletId([FromQuery] string walletId)
        {
            var currency = await _serviceManager.CurrencyService.GetCurrencyListByWalletId(walletId);
            if (currency == null)
            {
                return NotFound();
            }
            return Ok(currency);
        }

        [HttpGet("getCurrencyBalance")]
        public async Task<IActionResult> GetCurrencyBalance([FromQuery] string currencyId)
        {
            var currency = await _serviceManager.WalletCurrencyService.GetWalletCurrencyBalance( currencyId);
            if (currency == null)
            {
                return NotFound();
            }
            return Ok(currency);
        }
    }
}
