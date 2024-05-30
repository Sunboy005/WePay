﻿using wepay.Models;

namespace wepay.Repository.Interface
{
    public interface IRepositoryManager
    {
        IWalletRepository WalletRepository { get; }

        IOtpRepository OtpRepository { get; }
    }
}
