using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System.Net;
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
            wallet.Address = WalletAddressGenerator.AddressGen(10);

            var result = await _repositoryManager.WalletRepository.CreateWallet(wallet);

            return result;
        }

        public async Task<Wallet?> EnableWallet(string walletId)
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

        public async Task<Wallet?> GetWalletByAddress(string address)
        {
            var walletEntity = await _repositoryManager.WalletRepository.getWalletByAddress(address);
            return walletEntity;
        }

        public async Task<Wallet?> GetWalletById(string id)
        {
            var walletEntity =await _repositoryManager.WalletRepository.getWalletById(id);           
            return walletEntity;
        }

        public async Task<Wallet?> GetWalletByUserId(string userId)
        {
            var wallet = await _repositoryManager.WalletRepository.GetWalletByUserId(userId);          
            return wallet;
        }

        public async Task<Wallet?> LockWallet(string walletId)
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
            var wallet = await _repositoryManager.WalletRepository.getWalletByAddress(changeWalletPinDto.Address);
            if (wallet == null || wallet.Pin == changeWalletPinDto.Pin)
            {
                return false;
            }
            wallet.Pin = changeWalletPinDto.Pin;
            _repositoryManager.WalletRepository.updateWallet(wallet);
            return true;
        }

        public Task<Wallet> GetWalletBallance(string walletId)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetUserByWalletAddress(string address)
        {
            var user = await _repositoryManager.WalletRepository.getWalletByAddress(address);
            var name = user.user.FirstName;
            return name;
        }
        public async Task<bool> ReceiveMoney (string CurrencyId, int amount, int rate)
        {
            var currency = await _repositoryManager.WalletCurrencyRepository.getWalletCurrencyById(CurrencyId);
            if (currency == null)
            {
                return false ;
            }
            currency.Balance = currency.Balance + (amount * rate);
            await _repositoryManager.WalletCurrencyRepository.updateWalletCurrency(currency);
            return true;
           
        }
    }
}
