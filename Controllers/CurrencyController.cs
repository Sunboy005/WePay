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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddCurrency([FromBody] CurrencyToAddDto currencyToAddDto)
        {
            var user = await _serviceManager.UserService.GetUserByEmail(currencyToAddDto.UserEmail);
            if (user == null)
            {
                return NotFound();
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
