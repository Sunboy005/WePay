using Microsoft.AspNetCore.Identity;
using wepay.Models;
using wepay.Models.DTOs;

namespace wepay.Service.Interface
{
    public interface IWalletService
    {
        Task<Wallet> CreateWallet(WalletCreationDto walletcreationDto);
        Task<Wallet?> GetWalletById(string id);
        Task<Wallet?> GetWalletByAddress(string address);
        Task<Wallet?> LockWallet(String walletId);
        Task<Wallet?> EnableWallet(String walletId);
        Task<Wallet?>GetWalletByUserId(string userId);
        Task<bool> ChangeWalletPinAsync(ChangeWalletPinDto changeWalletPinDto);
        Task<Wallet> GetWalletBallance(String walletId);
        Task<string> GetUserByWalletAddress(string address);
        Task<bool> ReceiveMoney(string CurrencyId, int amount, int rate);
        Task<bool> TransferMoneyWithinWallet(Currency currencyFrom, Currency currencyTo, int amount);
        int GetWalletBallance(List<Currency> currencies);


    }
}
