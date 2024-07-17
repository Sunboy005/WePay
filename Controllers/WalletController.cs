using Entities.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MimeKit.Encodings;
using wepay.Models;
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
        public async Task<IActionResult> CreateWallet([FromBody] WalletCreationDto walletCreationDto)
        {
            var user = await _serviceManager.UserService.GetUserById(walletCreationDto.UserId);
            if (user == null)
            {
                throw new BadRequestException("User not found");
            }            
            
            if(user.Wallet != null)
            {
                throw new BadRequestException("User already has a wallet");
            }
            

            var address = await _serviceManager.WalletService.CreateWallet(walletCreationDto.Pin, user);                    
            
            return Created("WalletAddress", address);
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

            var walletDto = _serviceManager.Mapper.Map<WalletDto>(wallet);

            return Ok(walletDto);
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

            var walletDto = _serviceManager.Mapper.Map<WalletDto>(wallet);

            return Ok(walletDto);
        }

        [HttpPost("lock/{address}")]
        [Authorize]
        public async Task<IActionResult> LockWallet(string address)
        {

            var wallet = await _serviceManager.WalletService.GetWalletByAddress(address);

            if (wallet == null)
            {
                return NotFound();
            }

            await _serviceManager.WalletService.LockWallet(wallet);

            return Ok();
        }

        [HttpPost("enable/{address}")]
        [Authorize]
        public async Task<IActionResult> EnableWallet(string address)
        {
            var wallet = await _serviceManager.WalletService.GetWalletByAddress(address);

            if (wallet == null)
            {
                return NotFound();
            }

            await _serviceManager.WalletService.EnableWallet(wallet);

            return Ok();

        }       



        [HttpGet("userId", Name = "GetWalletByUserId")]
        [Authorize]
        public async Task<IActionResult> GetWalletByUserId([FromQuery] string userId)
        {
            var user = await _serviceManager.UserService.GetUserById(userId);

            if (user == null)
            {
                throw new BadRequestException("User does not exist");
            }

            if(user.Wallet == null)
            {
                return NotFound();
            }

            var wallet = _serviceManager.Mapper.Map<WalletDto>(user.Wallet);
            return Ok(wallet);

        }

        [HttpGet("balance/{walletAddress}")]
        [Authorize]
        public async Task<IActionResult> GetWalletBalance(string walletAddress)
        {
            var wallet = await _serviceManager.WalletService.GetWalletByAddress(walletAddress);
            if (wallet == null)
            {
                throw new BadRequestException("Wallet does not exist");
            }

            var balance = _serviceManager.WalletService.GetWalletBallance(wallet);

            return Ok(balance);
        }

        [HttpPost("transfer-within-wallet")]
        [Authorize]
        public async Task<IActionResult> TransferMoneyWithinWallet([FromBody] TransferWithinWalletDto transferWithinWalletDto)
        {
            var wallet = await _serviceManager.WalletService.GetWalletByAddress(transferWithinWalletDto.WalletAddress);
            if (wallet == null)
            {
                throw new BadRequestException("Wallet does not exist");
            }

            await _serviceManager.WalletService.TransferMoneyWithinWallet(wallet, transferWithinWalletDto);
            return Ok();
        }

        [HttpPost("transfer")]
        [Authorize]
        public async Task<IActionResult> TransferMoney([FromBody] TransferDto transferDto)
        {
            var walletFrom = await _serviceManager.WalletService.GetWalletByAddress(transferDto.WalletAddressFrom);
            var walletTo = await _serviceManager.WalletService.GetWalletByAddress(transferDto.WalletAddressTo);

            if (walletFrom == null || walletTo == null)
            {
                throw new BadRequestException("Wallet does not exist");
            }

            await _serviceManager.WalletService.TransferMoney(walletFrom, walletTo, transferDto);
            return Ok();
        }

        [HttpGet("Name/Address")]
        [Authorize]
        public async Task<IActionResult> GetUserByWalletAddress([FromQuery] string address)
        {
            var user = await _serviceManager.WalletService.GetUserByWalletAddress(address);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
    }
}
