﻿using wepay.Models;

namespace wepay.Models
{
    public class WalletCurrency
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public bool IsBase { get; set; } = false;
        public int Balance { get; set; } = 0;
        public UserWallet Wallet { get; set; } = null!;
        public string WalletId { get; set; }
        public string CurrencyId { get; set; }
        public Currency Currency { get; set; } = null!;
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; } = DateTime.Now;

    }
}


