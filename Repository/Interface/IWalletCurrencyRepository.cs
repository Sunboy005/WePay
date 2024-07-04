using wepay.Models;

namespace wepay.Repository.Interface
{
    public interface IWalletCurrencyRepository
    {
        Task updateWalletCurrency(WalletCurrency walletcurrency);
        Task<WalletCurrency> getWalletCurrencyById(string id);
        Task<WalletCurrency> AddWalletCurrency(WalletCurrency walletcurrency);
        Task deleteWalletCurrency(WalletCurrency walletcurrency);
        Task<WalletCurrency?> GetWalletCurrencyByShortCode(string WalletAddress, string shortcode);
    }
}
