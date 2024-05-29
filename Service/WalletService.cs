using AutoMapper;
using wepay.Models;
using wepay.Repository.Interface;
using wepay.Service.Interface;

namespace wepay.Service
{
    public class WalletService : IWalletService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        public WalletService(IRepositoryManager repositoryManager, IMapper mapper) { 
            _repositoryManager = repositoryManager;
            _mapper = mapper;
                }

        public async Task<Wallet> EnableWallet(string walletId)
        {
            var wallet = await _repositoryManager.WalletRepository.getWalletById(walletId);
            if (wallet == null)
            {
                return wallet;
            }
            wallet.IsLocked = false;
            _repositoryManager.WalletRepository.updateWallet(wallet);
            return wallet;
        }

        public async Task<Wallet> LockWallet(string walletId)
        {
         var wallet = await _repositoryManager.WalletRepository.getWalletById(walletId);
        if (wallet == null) {
                return wallet;
        }
            wallet.IsLocked = true;
            _repositoryManager.WalletRepository.updateWallet(wallet);
            return wallet;
        }
    }
}
