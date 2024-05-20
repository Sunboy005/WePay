using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using wepay.Models;

namespace wepay.Repository
{
    public class RepositoriesContext : IdentityDbContext<User>
    {
        public RepositoriesContext(DbContextOptions options) : base(options) { }


    }
}
