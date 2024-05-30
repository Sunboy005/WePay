using wepay.Repository.Interface;

namespace wepay.Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoriesContext _repositoriesContext;
        private readonly Lazy<IWalletRepository> _walletRepository;
        private readonly Lazy<ICurrencyRepository> _currencyRepository;
        public RepositoryManager(RepositoriesContext repositoriesContext)
        {
            _repositoriesContext = repositoriesContext;
            _walletRepository = new Lazy<IWalletRepository>(() => new WalletRepository(repositoriesContext));
            _currencyRepository = new Lazy<ICurrencyRepository>(() => new CurrencyRepository(repositoriesContext));
        }

        public IWalletRepository WalletRepository { get { return _walletRepository.Value; } }

        public ICurrencyRepository CurrencyRepository { get { return _currencyRepository.Value; } }
    }
}
