using wepay.Repository.Interface;

namespace wepay.Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoriesContext _repositoriesContext;
        private readonly Lazy<IWalletRepository> _walletRepository;
        public RepositoryManager(RepositoriesContext repositoriesContext)
        {
            _repositoriesContext = repositoriesContext;
            _walletRepository = new Lazy<IWalletRepository> (() => new WalletRepository(repositoriesContext));
        }

        public IWalletRepository WalletRepository { get { return _walletRepository.Value; } }
    }
}
