using wepay.Repository.Interface;

namespace wepay.Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoriesContext _repositoriesContext;
        public RepositoryManager(RepositoriesContext repositoriesContext)
        {
            _repositoriesContext = repositoriesContext;
        }
    }
}
