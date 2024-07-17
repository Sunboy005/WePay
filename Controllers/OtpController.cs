using Entities.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using wepay.Models.DTOs;
using wepay.Service.Interface;

namespace wepay.Controllers
{
    [Route("api/wepay/otp")]
    [ApiController]
    public class OtpController : ControllerBase
    {


        private readonly IServiceManager _serviceManager;

        public OtpController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }


        [HttpGet("verify-email/{email}")]
        public async Task<IActionResult> VerifyUserEmail(string email)
        {
            var user = await _serviceManager.UserService.GetUserByEmail(email);
            if (user == null)
            {
                throw new NotFoundException("No user found with email " + email);
            }


            var otp = await  _serviceManager.OtpService.VerifyUserEmail(user.Id);

            if (otp == null)
            {
                throw new InternalServerErrorException("We couldn't create otp. Try again.");
            }

            return Ok("We have sent an OTP code to the email you provided: " + otp);
        }

        [HttpPost("confirm-email")]
        public async Task<IActionResult> ConfirmUserEmail([FromBody] UserForEmailConfirmationDto userForEmailConfirmationDto)
        {
            var user = await _serviceManager.UserService.GetUserByEmail(userForEmailConfirmationDto.Email);

            if (user == null)
            {
                throw new NotFoundException("No user found with email " + userForEmailConfirmationDto.Email);
            }

            await _serviceManager.OtpService.ConfirmUserEmail(user, userForEmailConfirmationDto);

            return Ok();

        }


        [HttpGet("request-change-password/{email}")]
        public async Task<IActionResult> RequestChangePassword(string email)
        {
            var user = await _serviceManager.UserService.GetUserByEmail(email);

            if (user == null)
            {
                throw new NotFoundException("No user found with email " + email);
            }


            var otpCode = await _serviceManager.OtpService.RequestChangePassword(user.Id);

            if (string.IsNullOrEmpty(otpCode))
            {

                throw new InternalServerErrorException("We couldn't generate OTP");
            }

            return Ok("We have sent a password change code to " + email + "with OTP code " + otpCode);
        }

        
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] UserForChangePasswordDto userForChangePasswordDto)
        {
            var user = await _serviceManager.UserService.GetUserByEmail(userForChangePasswordDto.Email);
            if (user == null)
            {
                throw new NotFoundException("No user found with email " + userForChangePasswordDto.Email);
            }

            await _serviceManager.OtpService.ChangePassword(user, userForChangePasswordDto);

            return Ok();
        }

        [Authorize]
        [HttpPost("request-change-wallet-pin")]
        public async Task<IActionResult> RequestChangeWalletPin([FromBody] string userEmail)
        {
            var user = await _serviceManager.UserService.GetUserByEmail(userEmail);

            if (user == null)
            {
                throw new NotFoundException("No user found with email " + userEmail);
            }

            var otp = await _serviceManager.OtpService.RequestChangeWalletPin(user.Id);

            return Ok("We have sent a wallet pin change to " + userEmail + "with OTP code " + otp);
        }

        [Authorize]
        [HttpPost("change-wallet-pin")]
        public async Task<IActionResult> ChangeWalletPin([FromBody] ChangeWalletPinDto changeWalletPinDto)
        {
            var user = await _serviceManager.UserService.GetUserByEmail(changeWalletPinDto.UserEmail);
            if (user == null)
            {
                throw new NotFoundException("No user found with email " + changeWalletPinDto.Pin);
            }

            await _serviceManager.OtpService.ChangeWalletPin(user, changeWalletPinDto);

            return Ok();
        }

    }
}
