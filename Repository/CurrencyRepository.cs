using wepay.Repository.Interface;

namespace wepay.Repository
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly RepositoriesContext _repositoriesContext;
        public CurrencyRepository(RepositoriesContext repositoriesContext)
        {
            _repositoriesContext = repositoriesContext;
        }
    }
}
