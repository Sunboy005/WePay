using AutoMapper;
using Entities.Exceptions;
using MimeKit.Encodings;
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
        public async Task AddWalletCurrency(string userRole, User user, string shortCode, string walletId)
        {
            var currency = await _repositoryManager.CurrencyRepository.getCurrencyByShortCode(shortCode);
            if(currency== null)
            {
                throw new BadRequestException("Currency is not supported");
            }

            var walletCurrencies = user.Wallet.WalletCurrencies;
            if (userRole == "Noob")
            {                
                if (walletCurrencies.Count == 0)
                {
                    if(walletCurrencies.Where(walletCurrency => walletCurrency.CurrencyId == currency.Id).Count() > 0)
                    {
                        throw new BadRequestException("User already has the wallet currency");
                    }
                    WalletCurrency walletCurrency = new WalletCurrency
                    {
                       WalletId = walletId,
                        IsBase = true,
                        CurrencyId = currency.Id,
                        DateCreated = DateTime.Now
                    };

                    await _repositoryManager.WalletCurrencyRepository.AddWalletCurrency(walletCurrency);
                }
                else
                {
                    throw new BadRequestException("Noob user cannot have more than one Wallet Currency");
                }

            }
            else if (userRole == "Elite")
            {
                if (walletCurrencies.Where(walletCurrency => walletCurrency.CurrencyId == currency.Id).Count() > 0)
                {
                    throw new BadRequestException("User already has the wallet currency");
                }
                if (walletCurrencies.Count == 0)
                {                   
                    WalletCurrency walletCurrency = new WalletCurrency
                    {
                        WalletId = walletId,
                        IsBase = true,  
                        CurrencyId = currency.Id,
                        DateCreated = DateTime.Now
                    };
                    await _repositoryManager.WalletCurrencyRepository.AddWalletCurrency(walletCurrency);
                }
                else if (walletCurrencies.Count >= 1)
                {
                    WalletCurrency walletCurrency = new WalletCurrency
                    {
                     WalletId = walletId,
                        IsBase = false,
                        CurrencyId = currency.Id,
                        DateCreated = DateTime.Now
                    };
                    await _repositoryManager.WalletCurrencyRepository.AddWalletCurrency(walletCurrency);
                }
            }                                   
        }

        public async Task ChangeBaseCurrency(UserWallet wallet, ChangeBaseCurrencyDto changeBaseCurrencyDto)
        {

            var previousBase = wallet.WalletCurrencies.Where(walletCurrency => walletCurrency.Currency.ShortCode == changeBaseCurrencyDto.CurrencyCodeFrom).First();
            var newBase = wallet.WalletCurrencies.Where(walletCurrency => walletCurrency.Currency.ShortCode == changeBaseCurrencyDto.CurrencyCodeTo).First();

            if (previousBase == null || newBase == null)
            {
                throw new BadRequestException("One or all of the wallet currencies does not exist in the wallet");
            }

            previousBase.IsBase = false;
            newBase.IsBase = true;
            await _repositoryManager.WalletCurrencyRepository.updateWalletCurrency(previousBase);
            await _repositoryManager.WalletCurrencyRepository.updateWalletCurrency(newBase);
        }

        public async Task DeleteWalletCurrency(string walletAddress, string shortCode)
        {
            var walletcurrency = await GetWalletCurrencyByShortCode(walletAddress, shortCode);
            if (walletcurrency == null)
            {
                throw new BadRequestException("Wallet not found");
            }

            await _repositoryManager.WalletCurrencyRepository.deleteWalletCurrency(walletcurrency);
            
        }

        public async Task<WalletCurrency?> GetWalletCurrencyByShortCode(string WalletAddress, string ShortCode)
        {
            var walletCurrency = await _repositoryManager.WalletCurrencyRepository.GetWalletCurrencyByShortCode(WalletAddress, ShortCode);
            return walletCurrency;
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
