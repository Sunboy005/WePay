using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using wepay.Models;
using wepay.Models.DTOs;
using wepay.Service.Interface;

namespace wepay.Controllers
{
    [Route("wepay/auth")]
    [ApiController]

    public class AuthenticationController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public AuthenticationController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] UserForLoginDto userForLoginDto)
        {
            var result = await _serviceManager.AuthService.LoginUser(userForLoginDto);
            if (result == false)
            {
                return Unauthorized();
            }

            return Ok();

        }

        [HttpPost("request-change-password")]
        public async Task<IActionResult> RequestChangePassword([FromBody] string email )
        {
            var user = await _serviceManager.UserService.GetUserByEmail(email);
            if (user == null)
            {
                return NotFound("No user found with email: " + email);
            }

            var otpRequestDto = new OtpRequestDto
            {
                Email = email,
                Reason = nameof(OtpReasons.PasswordChange)    
            };
            var otpCode = await _serviceManager.OtpService.CreateNewOtp(otpRequestDto);
            if (string.IsNullOrEmpty(otpCode))
            {

                return BadRequest("We couldn't generate otp");
            }

            return Ok("We have sent an password change code to " + email + "with OTP code " + otpCode);
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] UserForChangePasswordDto userForChangePasswordDto)
        {
            var user = await _serviceManager.UserService.GetUserByEmail(userForChangePasswordDto.Email);
            if (user == null)
            {
                return NotFound("No user found with email: " + userForChangePasswordDto.Email);
            }


            var otpValidationDto = new OtpValidationDto
            {
                UserEmail = userForChangePasswordDto.Email,
                Code = userForChangePasswordDto.Code,
                Reason = nameof(OtpReasons.PasswordChange)
            };

            var otpIsValid = await _serviceManager.OtpService.ValidateOtp(otpValidationDto);

            if (otpIsValid == false)
            {
                return Unauthorized("Otp is not valid");
            }

            var result = await _serviceManager.AuthService.ChangePassword(userForChangePasswordDto.Email, userForChangePasswordDto.NewPassword);
            
            if(result.Succeeded == false)
            {
                return BadRequest("We couldn't change the Password");
            }

            return Ok();
        }

        [HttpPost("verify-email")]
        public async Task<IActionResult> VerifyUserEmail([FromBody] string email)
        {
            var user = await _serviceManager.UserService.GetUserByEmail(email);
            if (user == null)
            {
                return NotFound("No user found with email: " + email);
            }

            var otpRequestDto = new OtpRequestDto
            {
                Email = email,
                Reason = nameof(OtpReasons.EmailConfirmation)
            };
            var otpCode = await _serviceManager.OtpService.CreateNewOtp(otpRequestDto);
            if (string.IsNullOrEmpty(otpCode))
            {

                return BadRequest("We couldn't generate otp");
            }

            return Ok("We have sent an OTP code to the email you provided: "  + otpCode );
        }

        [HttpPost("confirm-email")]
        public async Task<IActionResult> ConfirmUserEmail([FromBody] UserForEmailConfirmationDto userForEmailConfirmationDto)
        {
            var user = await _serviceManager.UserService.GetUserByEmail(userForEmailConfirmationDto.Email);
            if (user == null)
            {
                return NotFound("No user found with email: " +  userForEmailConfirmationDto.Email);
            }


            var otpValidationDto = new OtpValidationDto
            {
                UserEmail = userForEmailConfirmationDto.Email,
                Code = userForEmailConfirmationDto.Code,
                Reason = nameof(OtpReasons.EmailConfirmation)
            };

            var otpIsValid = await _serviceManager.OtpService.ValidateOtp(otpValidationDto);

            if (otpIsValid == false)
            {
                return Unauthorized("Otp is not valid");
            }

     
            var identityResult = await _serviceManager.AuthService.ConfirmUserEmail(userForEmailConfirmationDto);

            if(identityResult.Succeeded == false)
            {
                return BadRequest("We couldn't verify email");
            }            

            return Ok();
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> LogOutUser([FromBody]string email) {
           await _serviceManager.AuthService.LogoutUser();
            return Ok();
        }
       
    }
}
