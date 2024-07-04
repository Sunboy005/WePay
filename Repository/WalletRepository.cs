using Microsoft.EntityFrameworkCore;
using wepay.Models;
using wepay.Repository.Interface;

namespace wepay.Repository
{
    public class WalletRepository : IWalletRepository
    {
        private readonly RepositoriesContext _repositoriesContext;
        public WalletRepository(RepositoriesContext repositoriesContext)
        {
            _repositoriesContext = repositoriesContext;
        }

        public async Task<UserWallet> CreateWallet(UserWallet wallet)
        {
            _repositoriesContext.Wallets.Add(wallet);
            await _repositoriesContext.SaveChangesAsync();
            return wallet;
        }

        public async Task<UserWallet> getWalletById(string walletID)
        {
            var wallet = await _repositoriesContext.Wallets.Include
                (wallet => wallet.WalletCurrencies).
                ThenInclude(wallet => wallet.Currency).
                Include(wallet => wallet.User).Where(wallet => wallet.Id == walletID).FirstAsync();
            return wallet;
        }

        public async Task<UserWallet> getWalletByAddress(string address)
        {
            var wallet = _repositoriesContext.Wallets.
                Include(wallet => wallet.WalletCurrencies).
                ThenInclude(wallet => wallet.Currency).
                Include(wallet => wallet.User).Where(a => a.Address == address).FirstOrDefault();
            return wallet;
        }

        public async Task updateWallet(UserWallet wallet)
        {
            _repositoriesContext.Wallets.Update(wallet);
            await _repositoriesContext.SaveChangesAsync();

        }

        public async Task<UserWallet> GetWalletByUserId(string userId)
        {
            var wallet = _repositoriesContext.Wallets.
                Include(wallet => wallet.WalletCurrencies).
               ThenInclude(wallet => wallet.Currency).
                Where(wallet => wallet.UserId == userId).FirstOrDefault();
            return wallet;
        }

        public async Task<User> getUserByWalletAddress(string address)
        {
            var user = await _repositoriesContext.Users.Include(user => user.Wallet).ThenInclude(wallet => wallet.WalletCurrencies).ThenInclude(wallet => wallet.Currency).Where(user => user.Wallet.Address == address).FirstOrDefaultAsync();
            return user;
        }
    }
}
