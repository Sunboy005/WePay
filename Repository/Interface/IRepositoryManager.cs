using wepay.Models;

namespace wepay.Repository.Interface
{
    public interface IRepositoryManager
    {
        IWalletRepository WalletRepository { get; }
        ICurrencyRepository CurrencyRepository { get; }

        IOtpRepository OtpRepository { get; }
        IWalletCurrencyRepository WalletCurrencyRepository { get; }
        ITransactionRepository TransactionRepository { get; }   
    }
}
