using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using wepay.EmailService;
using wepay.Models;
using wepay.Repository.Interface;
using wepay.Service.Interface;

namespace wepay.Service
{
    public sealed class ServiceManager: IServiceManager
    {
        private readonly Lazy<IUserService> _userService;
        private readonly Lazy<IAuthService> _authService;
        
        
        public ServiceManager(IRepositoryManager repositoryManager, UserManager<User> userManager, IMapper mapper, IConfiguration configuration, IEmailSender emailSender) {

            _userService = new Lazy<IUserService>(() => new UserService(userManager, mapper));
            _authService = new Lazy<IAuthService>(() => new AuthService(userManager, mapper, configuration, emailSender));
        }

        public IUserService UserService { get {  return _userService.Value; } }

        public IAuthService AuthService { get { return _authService.Value; } }
    }
}
