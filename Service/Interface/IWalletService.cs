using Microsoft.AspNetCore.Identity;
using wepay.Models;
using wepay.Models.DTOs;

namespace wepay.Service.Interface
{
    public interface IWalletService
    {
        Task<Wallet> CreateWallet(WalletCreationDto walletcreationDto);
        Task<Wallet> LockWallet(String walletId);
        Task<Wallet> EnableWallet(String walletId);
    }
}
