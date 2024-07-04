using wepay.Models.DTOs;
using wepay.Models;

namespace wepay.Service.Interface
{
    public interface IWalletCurrencyService
    {
        Task ChangeBaseCurrency(UserWallet wallet, ChangeBaseCurrencyDto changeBaseCurrencyDto);
        Task AddWalletCurrency(string userRole, User user, string shortCode, string walletId);
        Task<WalletCurrency> GetWalletCurrencyById(string Id);
        Task DeleteWalletCurrency(string walletAddress, string shortCode);
        Task<int?> GetWalletCurrencyBalance(string Id);
        Task<WalletCurrency?> GetWalletCurrencyByShortCode(string WalletAddress, string ShortCode);
    }
}
