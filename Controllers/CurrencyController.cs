using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using wepay.Models;
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
     
        [HttpGet("getCurrencyById")]
        
        public async Task<IActionResult> GetCurrencyById([FromQuery] string currencyId)
        {
           var currency = await _serviceManager.CurrencyService.GetCurrencyById(currencyId);
            if (currency == null)
            {
                return NotFound();
            }
            var currencyDto = _serviceManager.Mapper.Map<CurrencyDto>(currency);
            return Ok(currencyDto);
        }

        [HttpGet("getCurrencyByShortCode")]
        
        public async Task<IActionResult> GetCurrencyByShortCode([FromQuery] string shortCode)
        {
            var currency = await _serviceManager.CurrencyService.getCurrencyByShortCode(shortCode);
            if (currency == null)
            {
                return NotFound();
            }
            var currencyDto = _serviceManager.Mapper.Map<CurrencyDto>(currency);
            return Ok(currencyDto);
        }

        [HttpPost("Add-Currency")]
        
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
    }
}
