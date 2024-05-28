using wepay.Models;

namespace wepay.Service.Interface
{
    public interface IWalletService
    {
        Task<Wallet> LockWallet(String walletId);

        Task<Wallet> EnableWallet(String walletId);
    }
}
