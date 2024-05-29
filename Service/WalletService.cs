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

        public async Task<WalletDto> GetWalletByAddress(string address)
        {
            var walletEntity = await _repositoryManager.WalletRepository.getWalletByAddress(address);
            var walletDto = _mapper.Map<WalletDto>(walletEntity);
            return walletDto;
        }

        public async Task<WalletDto> GetWalletById(string id)
        {
            var walletEntity =await _repositoryManager.WalletRepository.getWalletById(id);
            var walletDto= _mapper.Map<WalletDto>(walletEntity);
            return walletDto;
        }

        public async Task<WalletDto> GetWalletByUserId(string userId)
        {
            var wallet = await _repositoryManager.WalletRepository.GetWalletByUserId(userId);

            var walletDto = _mapper.Map<WalletDto>(wallet);

            return walletDto;
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
        public async Task<bool> ChangeWalletPinAsync(ChangeWalletPinDto changeWalletPinDto)
        {
            var wallet = await GetWalletByAddress(changeWalletPinDto.Address);
            if (wallet == null || wallet.Pin != changeWalletPinDto.Pin)
            {
                return false;
            }
            wallet.Pin = changeWalletPinDto.Pin;
            _repositoryManager.WalletRepository.updateWallet(wallet);
            return true;
        }
    }
}
