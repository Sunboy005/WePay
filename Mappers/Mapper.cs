using AutoMapper;
using wepay.Models;
using wepay.Models.DTOs;

namespace wepay.Mappers
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<UserCreationDto, User>();
            CreateMap<UserForRegistrationDto, UserCreationDto>();
            CreateMap<UserCreationDto, UserForRegistrationDto>();
            CreateMap<UserUpdateDto, User>();
            CreateMap<User, IdentityUserDto>();
            CreateMap<AdminCreationDto, AdminForRegistrationDto>();
            CreateMap<AdminForRegistrationDto, AdminCreationDto>();
            CreateMap<AdminCreationDto, User>();
            CreateMap<UserWallet, WalletCreationDto>();
            CreateMap<WalletCreationDto, UserWallet>();
            CreateMap<UserWallet, WalletDto>();
            CreateMap<OtpRequestDto, Otp>();
            CreateMap<CurrencyToAddDto, Currency>();
            CreateMap<WalletCurrencyDeletionDto, WalletCurrency>();
            CreateMap<WalletCurrencyAdditionDto, WalletCurrency>();
            CreateMap<TransactionDto, Transaction>();
            CreateMap<UserWallet, WalletDto>();
            CreateMap<Currency, CurrencyDto>();
            CreateMap<WalletCurrency, WalletCurrencyDto>();
                
        }
    }
}
