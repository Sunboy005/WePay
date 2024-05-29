using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using wepay.Models;
using wepay.Repository.Interface;

namespace wepay.Repository
{
    public class WalletRepository : IWalletRepository
    {
        private readonly RepositoriesContext _repositoriesContext;
        public WalletRepository(RepositoriesContext repositoriesContext) {
            _repositoriesContext = repositoriesContext;
              }

        public async Task<Wallet> CreateWallet(Wallet wallet)
        {
            _repositoriesContext.Wallets.Add(wallet);
            await _repositoriesContext.SaveChangesAsync();
            return wallet;
        }

        public async Task<Wallet> getWalletById(string walletID)
        {
            var wallet = await _repositoriesContext.Wallets.FindAsync(walletID);
            return wallet;
        }

        public async Task<Wallet> getWalletByAddress(string address)
        {
            return _repositoriesContext.Wallets.Where(a => a.Address == address).FirstOrDefault();
        }

        public void updateWallet(Wallet wallet)
        {
            _repositoriesContext.Wallets.Update(wallet);
            _repositoriesContext.SaveChangesAsync();
        }


    }
}
