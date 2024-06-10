using wepay.Models;
using wepay.Repository.Interface;

namespace wepay.Repository
{
    public class WalletCurrencyRepository : IWalletCurrencyRepository
    {
        private readonly RepositoriesContext _repositoriesContext;
        public WalletCurrencyRepository(RepositoriesContext repositoriesContext)
        {
            _repositoriesContext = repositoriesContext;
        }
        public async Task<WalletCurrency> AddWalletCurrency(WalletCurrency walletcurrency)
        {
            _repositoriesContext.WalletCurrencies.Add(walletcurrency);
            await _repositoriesContext.SaveChangesAsync();
            return walletcurrency;
        }

        public async Task deleteWalletCurrency(WalletCurrency walletcurrency)
        {
            _repositoriesContext.WalletCurrencies.Remove(walletcurrency);
            await _repositoriesContext.SaveChangesAsync();
        }

        public async Task<WalletCurrency> getWalletCurrencyById(string Id)
        {
            var walletcurrency = await _repositoriesContext.WalletCurrencies.FindAsync(Id);
            return walletcurrency;
        }

        public async Task updateWalletCurrency(WalletCurrency walletcurrency)
        {
            _repositoriesContext.WalletCurrencies.Update(walletcurrency);
            await _repositoriesContext.SaveChangesAsync();
        }     
    }
}
