﻿using Microsoft.AspNetCore.Authentication;

namespace wepay.Service.Interface
{
    public interface IServiceManager
    {
        IUserService UserService { get; }

        IAuthService AuthService { get; }
    }
}
