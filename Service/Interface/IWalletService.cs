using Microsoft.AspNetCore.Identity;
using wepay.Models;
using wepay.Models.DTOs;

namespace wepay.Service.Interface
{
    public interface IWalletService
    {
        Task<Wallet> CreateWallet(WalletCreationDto walletcreationDto);
        Task<Wallet> GetWalletById(string id);
        Task<Wallet> GetWalletByAddress(string address);
        Task<Wallet> LockWallet(String walletId);
        Task<Wallet> EnableWallet(String walletId);
        Task<bool> ChangeWalletPinAsync(ChangeWalletPinDto changeWalletPinDto);
    }
}
