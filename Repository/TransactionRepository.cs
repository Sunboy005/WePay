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

        public async Task<List<Transaction>> GetTransactionsByWalletAddress(string Address)
        {
            var history =  _repositoriesContext.Transactions.Where(a => a.WalletAddress == Address).ToList();
            return history;
        }
        public async Task AddTransaction(TransactionDto transactionDto)
        {
            var transaction = await _repositoriesContext.Transactions.AddAsync(transaction);
            await _repositoriesContext.SaveChangesAsync();
            return transaction;
        }
    }
}
