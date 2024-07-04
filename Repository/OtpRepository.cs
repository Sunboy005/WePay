using Microsoft.EntityFrameworkCore;
using wepay.Models;
using wepay.Models.DTOs;
using wepay.Repository.Interface;

namespace wepay.Repository
{
    public class OtpRepository : IOtpRepository
    {
        private readonly RepositoriesContext _repositoriesContext;

        public OtpRepository(RepositoriesContext repositoriesContext)
        {
            _repositoriesContext = repositoriesContext;
        }

        public async Task CreateOtp(Otp otp)
        {
            await _repositoriesContext.Otps.AddAsync(otp);
            await _repositoriesContext.SaveChangesAsync();

        }

        public async Task<Otp?> GetOtpByCode(string code)
        {
            var otp = await _repositoriesContext.Otps.Where(otp => otp.Code == code).FirstOrDefaultAsync();
            return otp;
        }

        public async Task<Otp?> GetUnexpiredOtp(OtpRequestDto otpRequestDto)
        {
            var otp = await _repositoriesContext.Otps.Where(otp => otp.User.Id == otpRequestDto.UserId && otp.Reason == otpRequestDto.Reason && otp.DateExpired > DateTime.Now).FirstOrDefaultAsync();
            return otp;

        }

        public async Task UpdateOtp(Otp otp)
        {
            _repositoriesContext.Otps.Update(otp);
            await _repositoriesContext.SaveChangesAsync();

        }
    }
}
