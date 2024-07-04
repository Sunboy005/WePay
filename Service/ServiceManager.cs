using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

using wepay.Models;
using wepay.Repository.Interface;
using wepay.Service.Interface;

namespace wepay.Service
{
    public sealed class ServiceManager: IServiceManager
    {
        private readonly Lazy<IUserService> _userService;
        private readonly Lazy<IAuthService> _authService;
        private readonly Lazy<IWalletService> _walletService;
        private readonly Lazy<ICurrencyService> _currencyService;
        private readonly Lazy<IOtpService> _otpService;
        private readonly Lazy<IWalletCurrencyService> _walletcurrencyService;
        private readonly Lazy<ITransactionService> _transactionService;
        private readonly IMapper _mapper;


        public ServiceManager(IRepositoryManager repositoryManager, SignInManager<User> signInManager, UserManager<User> userManager, IMapper mapper, IConfiguration configuration) {
                
            _userService = new Lazy<IUserService>(() => new UserService(userManager, mapper));
            _authService = new Lazy<IAuthService>(() => new AuthService(signInManager, userManager, mapper, configuration, repositoryManager));
            _walletService = new Lazy<IWalletService>(() => new WalletService(repositoryManager, mapper));
            _currencyService = new Lazy<ICurrencyService>(() => new CurrencyService(repositoryManager, mapper));
            _otpService = new Lazy<IOtpService>(() => new OtpService(repositoryManager, mapper));
            _walletcurrencyService = new Lazy<IWalletCurrencyService>(() => new WalletCurrencyService(repositoryManager, mapper));
            _transactionService = new Lazy<ITransactionService>(() => new TransactionService(repositoryManager, mapper));
            _mapper = mapper;
        }

        public IUserService UserService { get {  return _userService.Value; } }

        public IAuthService AuthService { get { return _authService.Value; } }

        public IWalletService WalletService {  get { return _walletService.Value; } }

        public ICurrencyService CurrencyService {  get { return _currencyService.Value; } }

        public IOtpService OtpService {  get { return _otpService.Value; } }

        public IWalletCurrencyService WalletCurrencyService { get { return _walletcurrencyService.Value; } }
        public ITransactionService TransactionService { get { return _transactionService.Value; } }

        public IMapper Mapper { get { return _mapper; } }
    }
}
