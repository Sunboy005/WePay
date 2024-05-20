using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using wepay.Models;

namespace wepay.Extensions
{
    public static class ServiceExtensions
    {

        public static void configureIdentity(this IServiceCollection services)
        {
           // var builder = services.AddIdentity<User, IdentityRole>(user => 
           
        }

        public static void ConfigureJWT(this IServiceCollection services, IConfiguration
configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            // var secretKey = Environment.GetEnvironmentVariable("SECRET");

            var secretKey = "MY_SECRET_KEYMY_SECRET_KEYMY_SECRET_KEYMY_SECRET_KEYMY_SECRET_KEYMY_SECRET_KEY" +
                "MY_SECRET_KEYMY_SECRET_KEYMY_SECRET_KEYMY_SECRET_KEYMY_SECRET_KEYMY_SECRET_KEY" +
                "MY_SECRET_KEYMY_SECRET_KEYMY_SECRET_KEYMY_SECRET_KEYMY_SECRET_KEYMY_SECRET_KEYMY_SECRET_KEY" +
                "MY_SECRET_KEYMY_SECRET_KEYMY_SECRET_KEYMY_SECRET_KEYMY_SECRET_KEY";
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["validIssuer"],
                    ValidAudience = jwtSettings["validAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                };
            });
        }

        public static void configureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(s =>
            {
                s.AddSecurityDefinition("token", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Place to add JWT with Bearer",
                    Name = "Authorization",
                    BearerFormat = "JWT",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                s.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "token"
                        },
                        Name = "Bearer",
                    },
                    new List<string>()}
                });

            });
        }
    }
}
