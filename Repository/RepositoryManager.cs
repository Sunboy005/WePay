using wepay.Repository.Interface;

namespace wepay.Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoriesContext _repositoriesContext;
        private readonly Lazy<IWalletRepository> _walletRepository;
        private readonly Lazy<IOtpRepository> _otpRepository;   
        private readonly Lazy<ICurrencyRepository> _currencyRepository;
        private readonly Lazy<IWalletCurrencyRepository> _walletcurrencyRepository;
        public RepositoryManager(RepositoriesContext repositoriesContext)
        {
            _repositoriesContext = repositoriesContext;
            _walletRepository = new Lazy<IWalletRepository> (() => new WalletRepository(repositoriesContext));
            _otpRepository = new Lazy<IOtpRepository>(() => new OtpRepository(repositoriesContext));
            _currencyRepository = new Lazy<ICurrencyRepository> (() => new CurrencyRepository(repositoriesContext));
            _walletcurrencyRepository = new Lazy<IWalletCurrencyRepository>(() => new WalletCurrencyRepository(repositoriesContext));
        }

        public IWalletRepository WalletRepository { get { return _walletRepository.Value; } }

        public IOtpRepository OtpRepository { get { return _otpRepository.Value; } }

        public ICurrencyRepository CurrencyRepository { get { return _currencyRepository.Value;  } }

        public IWalletCurrencyRepository WalletCurrencyRepository { get { return _walletcurrencyRepository.Value; } }
    }
}
