using AutoMapper;
using wepay.Models;
using wepay.Models.DTOs;
using wepay.Repository.Interface;
using wepay.Service.Interface;

namespace wepay.Service
{
    public class WalletCurrencyService : IWalletCurrencyService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        public WalletCurrencyService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }
        public async Task<WalletCurrency> AddWalletCurrency(WalletCurrencyAdditionDto walletcurrencyAdditionDto)
        {
            var walletcurrency = _mapper.Map<WalletCurrency>(walletcurrencyAdditionDto);
            await _repositoryManager.WalletCurrencyRepository.AddWalletCurrency(walletcurrency);
            return walletcurrency;
        }

        public async Task<WalletCurrency?> ChangeBaseCurrency(string currencyIdFrom, string currencyIdTo)
        {
            var previousbase = await _repositoryManager.WalletCurrencyRepository.getWalletCurrencyById(currencyIdFrom);
            if (previousbase == null)
            {
                return null;
            }

            var newbase = await _repositoryManager.WalletCurrencyRepository.getWalletCurrencyById(currencyIdTo);
            if (newbase == null)
            {
                return null;
            }

            previousbase.IsBase = false;
            await _repositoryManager.WalletCurrencyRepository.updateWalletCurrency(previousbase);
            newbase.IsBase = true;
            await _repositoryManager.WalletCurrencyRepository.updateWalletCurrency(newbase);
            return newbase;
        }

        public async Task<bool> DeleteWalletCurrency(WalletCurrencyDeletionDto walletcurrencyDeletionDto)
        {
            var walletcurrency = await _repositoryManager.WalletCurrencyRepository.getWalletCurrencyById(walletcurrencyDeletionDto.WalletCurrencyId);
            if (walletcurrency == null)
            {
                return false;
            }

            await _repositoryManager.WalletCurrencyRepository.deleteWalletCurrency(walletcurrency);
            return true;
        }

        public async Task<WalletCurrency> GetCurrencyByShortCodeForAWallet(string WalletAddress, string ShortCode)
        {
            return await _repositoryManager.WalletCurrencyRepository.GetCurrencyByShortCodeForAWallet(WalletAddress, ShortCode);
        }

        public async Task<int?> GetWalletCurrencyBalance(string Id)
        {
            var walletcurrency = await _repositoryManager.WalletCurrencyRepository.getWalletCurrencyById(Id);
            var balance = walletcurrency.Balance;
            return balance;
        }

        public async Task<WalletCurrency> GetWalletCurrencyById(string Id)
        {
            return await _repositoryManager.WalletCurrencyRepository.getWalletCurrencyById(Id);
        }
    }
}
