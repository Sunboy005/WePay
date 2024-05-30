using AutoMapper;
using wepay.Models;
using wepay.Models.DTOs;
using wepay.Repository.Interface;
using wepay.Service.Interface;
using wepay.Utils;

namespace wepay.Service
{
    public class OtpService : IOtpService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public OtpService(IRepositoryManager repositoryManager, IMapper mapper) {
        _repositoryManager = repositoryManager;               
            _mapper = mapper;
        }

        
        public async Task<string> CreateNewOtp(OtpRequestDto otpRequestDto)
        {
            var unexpiredOtp =  await _repositoryManager.OtpRepository.GetUnexpiredOtp(otpRequestDto);

            if (unexpiredOtp != null && unexpiredOtp.IsExpired == false)
            {
                unexpiredOtp.IsExpired = true;
                updateOtp(unexpiredOtp);
            }

            var code = OtpCodeGenerator.GenerateOtp(4);
            var existingOtp = await _repositoryManager.OtpRepository.GetOtpByCode(code);
            if (existingOtp != null) { 
                if (OtpIsExpired(existingOtp))
                {
                    await CreateNewOtp(otpRequestDto);
                }                            
            }

            var otp = _mapper.Map<Otp>(otpRequestDto);
            otp.Code = code;
            otp.DateExpired = DateTime.Now.AddDays(1);
            otp.Reason = otpRequestDto.Reason;
            _repositoryManager.OtpRepository.CreateOtp(otp);
            return code;
        }


        public bool OtpIsExpired(Otp otp)
        {
            var otpIsExpired = (otp.DateExpired < DateTime.Now) || (otp.IsExpired);
            return otpIsExpired;
        }

        public async Task updateOtp(Otp otp)
        {
            await _repositoryManager.OtpRepository.UpdateOtp(otp);    
        }

        public async Task<bool> ValidateOtp(OtpValidationDto otpValidationDto)
        {
            var otp = await _repositoryManager.OtpRepository.GetOtpByCode(otpValidationDto.Code);
            if (otp == null || otp.Email != otpValidationDto.UserEmail || otp.IsExpired || otp.DateExpired < DateTime.Now || otp.Reason != otpValidationDto.Reason) {
                return false;
            }
            otp.IsExpired = true;
            await updateOtp(otp);
            return true;
        }
    }
}
