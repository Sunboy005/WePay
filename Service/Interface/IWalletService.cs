using Microsoft.AspNetCore.Identity;
using wepay.Models;
using wepay.Models.DTOs;

namespace wepay.Service.Interface
{
    public interface IWalletService
    {
        Task<Wallet> CreateWallet(WalletCreationDto walletcreationDto);
        Task<WalletDto?>GetWalletById(string id);
        Task<WalletDto?> GetWalletByAddress(string address);
        Task<Wallet?> LockWallet(String walletId);
        Task<Wallet?> EnableWallet(String walletId);
        Task<WalletDto?>GetWalletByUserId(string userId);
        Task<bool> ChangeWalletPinAsync(ChangeWalletPinDto changeWalletPinDto);      
        Task<string> GetUserByWalletAddress(string address);
        Task<bool> ReceiveMoney(string CurrencyId, int amount, int rate);
        Task<bool> TransferMoneyWithinWallet(Currency currencyFrom, Currency currencyTo, int amount);
        int GetWalletBallance(List<Currency> currencies);


    }
}
