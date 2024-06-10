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
    }
}
