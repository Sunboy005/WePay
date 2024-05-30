using wepay.Models;
using wepay.Models.DTOs;

namespace wepay.Service.Interface
{
    public interface IOtpService
    {
        Task<string> CreateNewOtp(OtpRequestDto otpRequestDto);

        bool OtpIsExpired(Otp otp);

        Task<bool> ValidateOtp(OtpValidationDto otpValidationDto);

        Task updateOtp(Otp otp);
    }
}
