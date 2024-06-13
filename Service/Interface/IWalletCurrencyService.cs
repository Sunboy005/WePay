using wepay.Models.DTOs;
using wepay.Models;

namespace wepay.Service.Interface
{
    public interface IWalletCurrencyService
    {
        Task<WalletCurrency?> ChangeBaseCurrency(string currencyIdFrom, string currencyIdTo);
        Task<WalletCurrency> AddWalletCurrency(WalletCurrencyAdditionDto walletcurrencyAdditionDto);
        Task<WalletCurrency> GetWalletCurrencyById(string Id);      
        Task<bool> DeleteWalletCurrency(WalletCurrencyDeletionDto walletcurrencyDeletionDto);
        Task<int?> GetWalletCurrencyBalance(string Id);
        Task<WalletCurrency> GetCurrencyByShortCodeForAWallet(string WalletAddress, string ShortCode);
    }
}
