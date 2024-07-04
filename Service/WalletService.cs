using AutoMapper;
using Entities.Exceptions;
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

        public async Task<String> CreateWallet(String pin, User user)
        {
            UserWallet wallet = new UserWallet{};
            
            wallet.Pin = WalletPinHasher.HashPassword(pin);
            wallet.CreatedDate = DateTime.Now;
            wallet.Address = WalletAddressGenerator.AddressGen(10);      
            wallet.UserId = user.Id;
            var result = await _repositoryManager.WalletRepository.CreateWallet(wallet);
            if (result == null)
            {
                throw new InternalServerErrorException("An error occurred");
            }                     
            return result.Address;
        }

        public async Task EnableWallet(UserWallet wallet)
        {
            wallet.IsLocked = false;
            _repositoryManager.WalletRepository.updateWallet(wallet);

        }

        public async Task<UserWallet?> GetWalletByAddress(string address)
        {
            var walletEntity = await _repositoryManager.WalletRepository.getWalletByAddress(address);
            return walletEntity;
        }

        public async Task<UserWallet?> GetWalletById(string id)
        {
            var walletEntity = await _repositoryManager.WalletRepository.getWalletById(id);
            return walletEntity;
        }

        public async Task<UserWallet?> GetWalletByUserId(string userId)
        {
            var wallet = await _repositoryManager.WalletRepository.GetWalletByUserId(userId);
            return wallet;
        }

        public async Task LockWallet(UserWallet wallet)
        {
            wallet.IsLocked = true;
            _repositoryManager.WalletRepository.updateWallet(wallet);
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

        public int GetWalletBallance(UserWallet wallet)
        {
            var walletCurrencies = wallet.WalletCurrencies;
            var balance = 0;
            var rate = 1500;

            foreach (var currency in walletCurrencies)
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

        public async Task TransferMoney(UserWallet walletFrom, UserWallet walletTo, TransferDto transferDto)
        {

            var walletCurrencyFrom = await _repositoryManager.WalletCurrencyRepository.GetWalletCurrencyByShortCode(walletFrom.Address, transferDto.CurrencyFromShortCode);
            var walletCurrencyTo = await _repositoryManager.WalletCurrencyRepository.GetWalletCurrencyByShortCode(walletTo.Address, transferDto.CurrencyToShortCode);
            if (walletCurrencyFrom == null || walletCurrencyTo == null)
            {
                throw new BadRequestException("Currency does not exist in wallet");
            }

            if(WalletPinHasher.VerifyPassword(transferDto.WalletPin, walletTo.Pin) == false)
            {
                throw new BadRequestException("Wrong pin");
            }

            if (walletCurrencyFrom.Balance < transferDto.Amount)
            {
                throw new BadRequestException("Balance is low");
            }

            

            var rate = 1000;
            walletCurrencyFrom.Balance = walletCurrencyFrom.Balance - transferDto.Amount;
            walletCurrencyTo.Balance = walletCurrencyTo.Balance + (transferDto.Amount * rate);
            await _repositoryManager.WalletCurrencyRepository.updateWalletCurrency(walletCurrencyTo);
            await _repositoryManager.WalletCurrencyRepository.updateWalletCurrency(walletCurrencyFrom);
            var transactionReference = TransactionReferenceGenerator.GenerateTransactionReference(10);
            Transaction fromTransaction = new Transaction
            {
                WalletCurrencyId = walletCurrencyFrom.Id,
                CurrencyName = walletCurrencyFrom.Currency.Name,
                Amount = transferDto.Amount,
                ConversionRate = rate,
                TransactionType = nameof(TransactionTypes.Debit),
                TransactionReference = nameof(transactionReference)
            };
            Transaction toTransaction = new Transaction
            {
                WalletCurrencyId = walletCurrencyTo.Id,
                CurrencyName = walletCurrencyTo.Currency.Name,
                Amount = transferDto.Amount * rate,
                ConversionRate = rate,
                TransactionType = nameof(TransactionTypes.Credit),
                TransactionReference = nameof(transactionReference)
            };
            await _repositoryManager.TransactionRepository.AddTransaction(fromTransaction);
            await _repositoryManager.TransactionRepository.AddTransaction(toTransaction);
        }

        public async Task TransferMoneyWithinWallet(UserWallet wallet, TransferWithinWalletDto transferWithinWalletDto)
        {
            var walletCurrencyFrom = await _repositoryManager.WalletCurrencyRepository.GetWalletCurrencyByShortCode(wallet.Address, transferWithinWalletDto.CurrencyFromShortCode);
            var walletCurrencyTo = await _repositoryManager.WalletCurrencyRepository.GetWalletCurrencyByShortCode(wallet.Address, transferWithinWalletDto.CurrencyToShortCode);
            if (walletCurrencyFrom == null || walletCurrencyTo == null)
            {
                throw new BadRequestException("Currency does not exist in wallet");
            }

            if (WalletPinHasher.VerifyPassword(transferWithinWalletDto.WalletPin, wallet.Pin) == false)
            {
                throw new BadRequestException("Wrong pin");
            }

            if (walletCurrencyFrom.Balance < transferWithinWalletDto.Amount)
            {
                throw new BadRequestException("Balance is low");
            }
            var rate = 1000;
            walletCurrencyFrom.Balance = walletCurrencyFrom.Balance - transferWithinWalletDto.Amount;
            walletCurrencyTo.Balance = walletCurrencyTo.Balance + (transferWithinWalletDto.Amount * rate);
            await _repositoryManager.WalletCurrencyRepository.updateWalletCurrency(walletCurrencyTo);
            await _repositoryManager.WalletCurrencyRepository.updateWalletCurrency(walletCurrencyFrom);
            var transactionReference = TransactionReferenceGenerator.GenerateTransactionReference(10);
            Transaction fromTransaction = new Transaction
            {
                WalletCurrencyId = walletCurrencyFrom.Id,
                CurrencyName = walletCurrencyFrom.Currency.Name,
                Amount = transferWithinWalletDto.Amount,
                ConversionRate = rate,
                TransactionType = nameof(TransactionTypes.Debit),
                TransactionReference = nameof(transactionReference)
            };
            Transaction toTransaction = new Transaction
            {
                WalletCurrencyId = walletCurrencyTo.Id,
                CurrencyName = walletCurrencyTo.Currency.Name,
                Amount = transferWithinWalletDto.Amount * rate,
                ConversionRate = rate,
                TransactionType = nameof(TransactionTypes.Credit),
                TransactionReference = nameof(transactionReference)
            };
            await _repositoryManager.TransactionRepository.AddTransaction(fromTransaction);
            await _repositoryManager.TransactionRepository.AddTransaction(toTransaction);

        }

        public async Task<string> GetUserByWalletAddress(string address)
        {
            var user = await _repositoryManager.WalletRepository.getUserByWalletAddress(address);
            var name = user.FirstName;
            return name;
        }
        public async Task<bool> ReceiveMoney(string CurrencyId, int amount, int rate)
        {
            var currency = await _repositoryManager.WalletCurrencyRepository.getWalletCurrencyById(CurrencyId);
            if (currency == null)
            {
                return false;
            }
            currency.Balance = currency.Balance + (amount * rate);
            await _repositoryManager.WalletCurrencyRepository.updateWalletCurrency(currency);
            return true;

        }

    }
}
