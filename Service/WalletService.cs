using AutoMapper;
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
        public WalletService(IRepositoryManager repositoryManager, IMapper mapper)
        {
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

        public async Task<WalletDto?> GetWalletByAddress(string address)
        {
            var walletEntity = await _repositoryManager.WalletRepository.getWalletByAddress(address);
            var walletDto = _mapper.Map<WalletDto>(walletEntity);
            return walletDto;
        }

        public async Task<WalletDto?> GetWalletById(string id)
        {
            var walletEntity = await _repositoryManager.WalletRepository.getWalletById(id);
            var walletDto = _mapper.Map<WalletDto>(walletEntity);
            return walletDto;
        }

        public async Task<WalletDto?> GetWalletByUserId(string userId)
        {
            var wallet = await _repositoryManager.WalletRepository.GetWalletByUserId(userId);

            var walletDto = _mapper.Map<WalletDto>(wallet);

            return walletDto;
        }

        public async Task<Wallet?> LockWallet(string walletId)
        {
            var wallet = await _repositoryManager.WalletRepository.getWalletById(walletId);
            if (wallet == null)
            {
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

        public int GetWalletBallance(List<Currency> currencies)
        {
            var balance = 0;
            var rate = 1500;

            foreach (var currency in currencies)
            {
                if (currency.IsBase)
                {
                    balance = balance + currency.Balance;
                }
                else
                {
                    balance = balance + (rate * currency.Balance);
                }
            }

            return balance;
        }

        public async Task<bool> TransferMoneyWithinWallet(Currency currencyFrom, Currency currencyTo, int amount)
        {
            var rate = 1000;
            currencyFrom.Balance = currencyFrom.Balance - amount;
            currencyTo.Balance = currencyTo.Balance + (amount * rate);
            await _repositoryManager.CurrencyRepository.updateCurrency(currencyTo);
            await _repositoryManager.CurrencyRepository.updateCurrency(currencyFrom);
            return true;
        }

        public async Task<string> GetUserByWalletAddress(string address)
        {
            var user = await _repositoryManager.WalletRepository.getWalletByAddress(address);
            var name = user.user.FirstName;
            return name;
        }
        public async Task<bool> ReceiveMoney(string CurrencyId, int amount, int rate)
        {
            var currency = await _repositoryManager.CurrencyRepository.getCurrencyById(CurrencyId);
            if (currency == null)
            {
                return false;
            }
            currency.Balance = currency.Balance + (amount * rate);
            await _repositoryManager.CurrencyRepository.updateCurrency(currency);
            return true;

        }
     
    }
}
