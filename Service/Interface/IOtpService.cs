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

        Task<String?> VerifyUserEmail(string userId);

        Task ConfirmUserEmail(User user, UserForEmailConfirmationDto userForEmailConfirmationDto);

        Task<String?> RequestChangePassword(string userId);

        Task ChangePassword(User user, UserForChangePasswordDto userForChangePasswordDto);

        Task<String?> RequestChangeWalletPin(string userId);

        Task ChangeWalletPin(User user, ChangeWalletPinDto changeWalletPinDto);
    }
}
