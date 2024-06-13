using Microsoft.EntityFrameworkCore;
using wepay.Models;
using wepay.Models.DTOs;
using wepay.Repository.Interface;

namespace wepay.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly RepositoriesContext _repositoriesContext;
       public TransactionRepository(RepositoriesContext repositoriesContext)
        {
            _repositoriesContext = repositoriesContext;
        }
        public async Task<Transaction> GetTransactionById(string id)
        {
            var transaction = await _repositoriesContext.Transactions.FindAsync(id);
            return transaction; 
        }
        public async Task AddTransaction(TransactionDto transactionDto)
        {
            var transaction = await _repositoriesContext.Transactions.AddAsync(transaction);
            await _repositoriesContext.SaveChangesAsync();
            return transaction;
        }
    }
}
