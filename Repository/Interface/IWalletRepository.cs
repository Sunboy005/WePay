using wepay.Models;

namespace wepay.Repository.Interface
{
    public interface IWalletRepository
    {
       Task updateWallet(UserWallet wallet);

       Task<UserWallet> getWalletById(string id);
       Task<UserWallet> getWalletByAddress(string address);

       Task<UserWallet>CreateWallet(UserWallet wallet);
        Task<UserWallet>GetWalletByUserId(string userId);

        Task<User> getUserByWalletAddress(string address);



    }
}
