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
        private readonly Lazy<IOtpService> _otpService;
        
        
        public ServiceManager(IRepositoryManager repositoryManager, SignInManager<User> signInManager, UserManager<User> userManager, IMapper mapper, IConfiguration configuration) {
                
            _userService = new Lazy<IUserService>(() => new UserService(userManager, mapper));
            _authService = new Lazy<IAuthService>(() => new AuthService(signInManager, userManager, mapper, configuration));
            _walletService = new Lazy<IWalletService>(() => new WalletService(repositoryManager, mapper));
        }

        public IUserService UserService { get {  return _userService.Value; } }

        public IAuthService AuthService { get { return _authService.Value; } }

        public IWalletService WalletService {  get { return _walletService.Value; } }
    }
}
