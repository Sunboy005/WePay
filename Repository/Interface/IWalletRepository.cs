using wepay.Models;

namespace wepay.Repository.Interface
{
    public interface IWalletRepository
    {
       void updateWallet(Wallet wallet);

       Task<Wallet> getWalletById(string id);

       Task<Wallet>CreateWallet(Wallet wallet);

    }
}
