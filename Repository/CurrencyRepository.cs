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

        public async Task AddCurrency (Currency currency)
        {
            _repositoriesContext.Currencies.Add(currency);
            await _repositoriesContext.SaveChangesAsync();
        } 

        public async Task updateCurrency(Currency currency)
        {
            _repositoriesContext.Currencies.Update(currency);
           await _repositoriesContext.SaveChangesAsync();
        }
    }
}
