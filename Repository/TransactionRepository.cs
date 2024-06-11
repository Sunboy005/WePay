using wepay.Models;
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
    }
}
