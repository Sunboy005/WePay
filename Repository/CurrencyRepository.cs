using wepay.Models;
using wepay.Repository.Interface;

namespace wepay.Repository
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly RepositoriesContext _repositoriesContext;
        public CurrencyRepository(RepositoriesContext repositoriesContext)
        {
            _repositoriesContext = repositoriesContext;
        }
        public async Task<Currency> getCurrencyById(string currencyId)
        {
            var currency = await _repositoriesContext.Currencies.FindAsync(currencyId);
            return currency;
        }

        public async Task<Currency> AddCurrency (Currency currency)
        {
            _repositoriesContext.Currencies.Add(currency);
            await _repositoriesContext.SaveChangesAsync();
            return currency;
        } 

        public async Task updateCurrency(Currency currency)
        {
            _repositoriesContext.Currencies.Update(currency);
           await _repositoriesContext.SaveChangesAsync();
        }

        public async Task deleteCurrency(Currency currency)
        {
            _repositoriesContext.Currencies.Remove(currency);
            await _repositoriesContext.SaveChangesAsync();
        }

        public async Task<List<Currency>>? getCurrencyListByWalletAddress(string walletAddress)
        {
            var currency =  _repositoriesContext.Currencies.Where(currency=> currency.WalletAddress == walletAddress).ToList();
                
            return currency;
        }

        public async Task<Currency?> GetCurrencyByShortCodeForAWallet(string walletAddress, string shortCode)
        {
            var currency =   _repositoriesContext.Currencies.Where(currency => currency.ShortCode == shortCode && currency.WalletAddress == walletAddress).FirstOrDefault(); 
            return currency;

            
        }
    }
}
