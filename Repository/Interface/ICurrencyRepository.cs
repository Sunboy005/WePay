using wepay.Models;

namespace wepay.Repository.Interface
{
    public interface ICurrencyRepository
    {
        Task updateCurrency(Currency currency);
        Task<Currency> getCurrencyById(string id);
        Task<Currency> AddCurrency(Currency currency);
        Task deleteCurrency(Currency currency);
        Task<List<Currency>>? getCurrencyListByWalletAddress(string walletAddress);
        Task<Currency> GetCurrencyByShortCodeForAWallet(string walletAddress, string shortCode);    
    }
}
