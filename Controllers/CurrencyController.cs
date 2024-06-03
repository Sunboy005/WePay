using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
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
            var result = await _serviceManager.CurrencyService.ChangeBaseCurrency(currencyIdFrom,currencyIdTo);
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
            var result = await _serviceManager.CurrencyService.AddCurrency(currencyToAddDto);
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }
        [HttpDelete("delete-currency")]
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
        public async Task<IActionResult> GetCurrencyListByWalletId([FromQuery] string walletId)
        {
            var currency = await _serviceManager.CurrencyService.GetCurrencyListByWalletId(walletId);
            if (currency == null)
            {
                return NotFound();
            }
            return Ok(currency);
        }

        [HttpGet("getCurrencyBalancebyWalletId")]
        public async Task<IActionResult> GetCurrencyBalance([FromQuery] string currencyId)
        {
            var currency = await _serviceManager.CurrencyService.GetCurrencyBalance( currencyId);
            if (currency == null)
            {
                return NotFound();
            }
            return Ok(currency);
        }
    }
}
