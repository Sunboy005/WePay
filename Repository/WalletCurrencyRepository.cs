using Microsoft.EntityFrameworkCore;
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

        public async Task<WalletCurrency?> GetWalletCurrencyByShortCode(string Address, string shortcode)
        {
            var walletcurrency = await _repositoriesContext.WalletCurrencies.
                Include(walletcurrency => walletcurrency.Currency).
                Where(a => a.Wallet.Address == Address && a.Currency.ShortCode == shortcode).FirstOrDefaultAsync();
            return walletcurrency;
        }

        public async Task<WalletCurrency> getWalletCurrencyById(string Id)
        {
            var walletcurrency = await _repositoriesContext.WalletCurrencies.
                Include(walletcurrency => walletcurrency.Currency).
                Where(walletcurrency => walletcurrency.Id == Id).FirstAsync();
            return walletcurrency;
        }

        public async Task updateWalletCurrency(WalletCurrency walletcurrency)
        {
            _repositoriesContext.WalletCurrencies.Update(walletcurrency);
            await _repositoriesContext.SaveChangesAsync();
        }     
    }
}
