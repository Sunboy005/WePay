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

        public async Task<List<Currency>>? getCurrencyListByWalletId(string walletId)
        {
            var currency =  _repositoriesContext.Currencies.Where(currency=> currency.WalletId == walletId).ToList();
                
            return currency;
        }
    }
}
