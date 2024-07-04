using AutoMapper;
using Entities.Exceptions;
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

        public OtpService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }


        public async Task<string> CreateNewOtp(OtpRequestDto otpRequestDto)
        {
            var unexpiredOtp = await _repositoryManager.OtpRepository.GetUnexpiredOtp(otpRequestDto);

            if (unexpiredOtp != null && unexpiredOtp.IsExpired == false)
            {
                unexpiredOtp.IsExpired = true;
                await updateOtp(unexpiredOtp);
            }

            var code = OtpCodeGenerator.GenerateOtp(4);
            var existingOtp = await _repositoryManager.OtpRepository.GetOtpByCode(code);
            if (existingOtp != null)
            {
                if (OtpIsExpired(existingOtp))
                {
                    await CreateNewOtp(otpRequestDto);
                }
            }

            var otp = _mapper.Map<Otp>(otpRequestDto);
            otp.Code = code;
            otp.DateExpired = DateTime.Now.AddDays(1);
            otp.Reason = otpRequestDto.Reason;
            otp.UserId = otpRequestDto.UserId;
            await _repositoryManager.OtpRepository.CreateOtp(otp);
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
            if (otp == null || otp.User.Email != otpValidationDto.UserEmail || otp.IsExpired || otp.DateExpired < DateTime.Now || otp.Reason != otpValidationDto.Reason)
            {
                return false;
            }
            otp.IsExpired = true;
            await updateOtp(otp);
            return true;
        }

        public async Task<String?> VerifyUserEmail(string userId)
        {
            var otpRequestDto = new OtpRequestDto
            {
                UserId = userId,
                Reason = nameof(OtpReasons.EmailConfirmation)
            };

            var otp = await CreateNewOtp(otpRequestDto);
            return otp;
        }

        public async Task ConfirmUserEmail(User user, UserForEmailConfirmationDto userForEmailConfirmationDto)
        {
            var otpValidationDto = new OtpValidationDto
            {
                UserEmail = userForEmailConfirmationDto.Email,
                Code = userForEmailConfirmationDto.Code,
                Reason = nameof(OtpReasons.EmailConfirmation)
            };

            var otpIsValid = await ValidateOtp(otpValidationDto);
            if (otpIsValid == false)
            {
                throw new BadRequestException("Otp is invalid");
            }

            user.EmailConfirmed = true;
            var result = await _repositoryManager.UserManager.UpdateAsync(user);
            if (result.Succeeded == false)
            {
                throw new InternalServerErrorException("An error occurred");
            }

        }

        public async Task<String?> RequestChangePassword(string userId)
        {
            var otpRequestDto = new OtpRequestDto
            {
                UserId = userId,
                Reason = nameof(OtpReasons.PasswordChange)
            };

            var otp = await CreateNewOtp(otpRequestDto);
            return otp;
        }

        public async Task ChangePassword(User user, UserForChangePasswordDto userForChangePasswordDto)
        {

            var otpValidationDto = new OtpValidationDto
            {
                UserEmail = userForChangePasswordDto.Email,
                Code = userForChangePasswordDto.Code,
                Reason = nameof(OtpReasons.PasswordChange)
            };

            var otpIsValid = await ValidateOtp(otpValidationDto);

            if (otpIsValid == false)
            {
                throw new BadRequestException("Otp is invalid");
            }

            var hasedPassword = _repositoryManager.UserManager.PasswordHasher.HashPassword(user, userForChangePasswordDto.NewPassword);
            if (hasedPassword == null)
            {
                throw new InternalServerErrorException("An error occurred");
            }

            user.PasswordHash = hasedPassword;
            var result = await _repositoryManager.UserManager.UpdateAsync(user);
            if (result.Succeeded == false)
            {
                throw new InternalServerErrorException("An error occurred");
            }
        }

        public async Task<String?> RequestChangeWalletPin(string userId)
        {
            var otpRequestDto = new OtpRequestDto
            {
                UserId = userId,
                Reason = nameof(OtpReasons.WalletPinChange)
            };

            var otp = await CreateNewOtp(otpRequestDto);
            return otp;
        }

        public async Task ChangeWalletPin(User user, ChangeWalletPinDto changeWalletPinDto)
        {

            var otpValidationDto = new OtpValidationDto
            {
                UserEmail = changeWalletPinDto.UserEmail,
                Code = changeWalletPinDto.Pin,
                Reason = nameof(OtpReasons.WalletPinChange)
            };

            var otpIsValid = await ValidateOtp(otpValidationDto);

            if (otpIsValid == false)
            {
                throw new BadRequestException("Otp is invalid");
            }

            var wallet = await _repositoryManager.WalletRepository.getWalletByAddress(changeWalletPinDto.Address);
            if (wallet == null)
            {
                throw new BadRequestException("Wallet not found");
            }

            wallet.Pin = WalletPinHasher.HashPassword(changeWalletPinDto.Pin);
            _repositoryManager.WalletRepository.updateWallet(wallet);
        }
    }
}
