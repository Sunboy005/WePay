using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using wepay.Models;
using wepay.Repository;
using wepay.Repository.Interface;
using wepay.Service.Interface;
using wepay.Service;
using Microsoft.EntityFrameworkCore;

using wepay.Utils;

namespace wepay.Extensions
{
    public static class ServiceExtensions
    {

        

        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
            services.AddScoped<IRepositoryManager, RepositoryManager>();

        public static void ConfigureServiceMAnager(this IServiceCollection services) =>
            services.AddScoped<IServiceManager, ServiceManager>();

        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
            services.AddDbContext<RepositoriesContext>(opts => opts.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentity<User, IdentityRole>(options =>
            {
                
                options.SignIn.RequireConfirmedEmail = true;   
                options.User.RequireUniqueEmail = true;               
                options.Password.RequiredLength = 8;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true; 
            }).AddEntityFrameworkStores<RepositoriesContext>()           
            .AddDefaultTokenProviders; 
        }

        public static void ConfigureApplicationsCookie(this IServiceCollection services)
        {
            services.ConfigureApplicationCookie(options =>
            {
                options.SlidingExpiration = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(10);

            }
            );                         
        }

     
    }
}
