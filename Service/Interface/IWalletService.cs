using wepay.Models;
using wepay.Models.DTOs;

namespace wepay.Service.Interface
{
    public interface IWalletService
    {
        Task<String> CreateWallet(String pin, User user);
        Task<UserWallet?> GetWalletById(string id);
        Task<UserWallet?> GetWalletByAddress(string address);
        Task LockWallet(UserWallet wallet);
        Task EnableWallet(UserWallet wallet);
        Task<UserWallet?> GetWalletByUserId(string userId);
        Task<bool> ChangeWalletPinAsync(ChangeWalletPinDto changeWalletPinDto);       
        Task<string> GetUserByWalletAddress(string address);
        Task<bool> ReceiveMoney(string CurrencyId, int amount, int rate);
        Task TransferMoneyWithinWallet(UserWallet wallet, TransferWithinWalletDto transferWithinWalletDto);
        int GetWalletBallance(UserWallet wallet);
        Task TransferMoney(UserWallet walletFrom, UserWallet walletTo, TransferDto transferDto);

    }
}
