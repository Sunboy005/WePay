using Microsoft.AspNetCore.Authentication;

namespace wepay.Service.Interface
{
    public interface IServiceManager
    {
        IUserService UserService { get; }

        IAuthService AuthService { get; }

        IWalletService WalletService { get; }
        IWalletCurrencyService WalletCurrencyService { get; }
        

        ICurrencyService CurrencyService { get; }

        IOtpService OtpService { get; }
        ITransactionService TransactionService { get; }
    }
}
