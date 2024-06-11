using wepay.Models;
using wepay.Models.DTOs;


namespace wepay.Service.Interface
{
    public interface ITransactionService
    {
        Task<List<Transaction>> GetTransactionsByWalletAddress(string address);
    }
}
