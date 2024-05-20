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
        public ServiceManager(IRepositoryManager repositoryManager, UserManager<User> userManager) {
            _userService = new Lazy<IUserService>(() => new UserService(userManager));
        }

        public IUserService UserService { get {  return _userService.Value; } }
    }
}
