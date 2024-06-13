using wepay.Models;
using wepay.Models.DTOs;

namespace wepay.Repository.Interface
{
    public interface ITransactionRepository
    {
        Task<Transaction> GetTransactionById(string id);
        Task AddTransaction(Transaction transaction);
    }
}
