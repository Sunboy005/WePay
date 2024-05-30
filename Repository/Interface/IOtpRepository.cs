using wepay.Models;
using wepay.Models.DTOs;

namespace wepay.Repository.Interface
{
    public interface IOtpRepository
    {
        void CreateOtp(Otp otp);
        Task<Otp?> GetOtpByCode(string code);
        Task<Otp?> GetUnexpiredOtp(OtpRequestDto otpRequestDto);

        Task UpdateOtp(Otp otp);    
    }
}
