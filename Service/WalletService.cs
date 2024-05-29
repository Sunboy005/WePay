using AutoMapper;
using Microsoft.AspNetCore.Identity;
using wepay.Models;
using wepay.Models.DTOs;
using wepay.Repository.Interface;
using wepay.Service.Interface;
using wepay.Utils;

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

        public async Task<Wallet> CreateWallet(WalletCreationDto walletcreationDto)
        {
           // var walletCreation = _mapper.Map<WalletCreationDto>(walletcreationDto);

            var wallet = _mapper.Map<Wallet>(walletcreationDto);
            wallet.CreatedDate = DateTime.Now;
            wallet.Address = AddressGenerator.AddressGen(10);

            var result = await _repositoryManager.WalletRepository.CreateWallet(wallet);

            return result;
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
