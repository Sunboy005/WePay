﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using wepay.Models;
using wepay.Service.Interface;

namespace wepay.Service
{
    public class UserService : IUserService
    {
        private readonly UserManager<User>  _userManager;
        private readonly IMapper _mapper;

        private User _user;

        public UserService(UserManager<User> userManager, IMapper mapper)

        {
            _mapper = mapper;
            _userManager = userManager;
        }
    }
}
