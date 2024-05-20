using Microsoft.AspNetCore.Identity;

namespace wepay.Service.Interface
{
    public interface IUserService
    {     
        Task<bool> ChangeUserPassword(string oldPassword, string newPassword);
    }
}
