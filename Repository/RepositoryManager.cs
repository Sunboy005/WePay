using Microsoft.AspNetCore.Identity;
using wepay.Models;
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
        private readonly Lazy<ITransactionRepository> _transactionRepository;
        private readonly UserManager<User> _userManager;
        
        public RepositoryManager(RepositoriesContext repositoriesContext, UserManager<User> userManager)
        {
            _repositoriesContext = repositoriesContext;
            _walletRepository = new Lazy<IWalletRepository> (() => new WalletRepository(repositoriesContext));
            _otpRepository = new Lazy<IOtpRepository>(() => new OtpRepository(repositoriesContext));
            _currencyRepository = new Lazy<ICurrencyRepository> (() => new CurrencyRepository(repositoriesContext));
            _walletcurrencyRepository = new Lazy<IWalletCurrencyRepository>(() => new WalletCurrencyRepository(repositoriesContext));
            _transactionRepository = new Lazy<ITransactionRepository>(() => new TransactionRepository(repositoriesContext));
            _userManager = userManager;
        }

        public IWalletRepository WalletRepository { get { return _walletRepository.Value; } }

        public IOtpRepository OtpRepository { get { return _otpRepository.Value; } }

        public ICurrencyRepository CurrencyRepository { get { return _currencyRepository.Value;  } }

        public IWalletCurrencyRepository WalletCurrencyRepository { get { return _walletcurrencyRepository.Value; } }
        public ITransactionRepository TransactionRepository { get { return _transactionRepository.Value; } }

        public UserManager<User> UserManager => _userManager;

        
    }
}
