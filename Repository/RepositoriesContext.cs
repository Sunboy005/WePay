﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using wepay.Models;
using wepay.Utils;

namespace wepay.Repository
{
    public class RepositoriesContext : IdentityDbContext<User>
    {
        public RepositoriesContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasOne(x => x.Wallet)
                .WithOne(x => x.User);
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new RoleConfiguration());
        }

        public DbSet<UserWallet>? Wallets { get; set; } 

        public DbSet<Currency>? Currencies { get; set;}

        public DbSet<Otp> Otps { get; set; }

        public DbSet<WalletCurrency> WalletCurrencies { get; set; } 
        public DbSet<Transaction> Transactions { get; set; }

        }
    }
