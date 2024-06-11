using wepay.Models;

namespace wepay.Repository.Interface
{
    public interface ITransactionRepository
    {
        Task <List<Transaction>> GetTransactionsByWalletAddress(string Address);
    }
}
