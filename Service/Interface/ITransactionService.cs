using wepay.Models;
using wepay.Models.DTOs;


namespace wepay.Service.Interface
{
    public interface ITransactionService
    {
        Task<Transaction?> GetTransactionById(string id);
        Task<Transaction> AddTransaction(TransactionDto transactionDto);


    }
}
