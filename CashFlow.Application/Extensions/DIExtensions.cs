using CashFlow.Api.Security;
using CashFlow.Application.Repositories;
using CashFlow.Application.Services.Interfaces;
using CashFlow.Application.Services;
using JSM.FluentValidation.AspNet.AsyncFilter;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CashFlow.Application.Extensions
{
    public static class DIExtensions
    {
        public static IServiceCollection ApplicationConfig(this IServiceCollection services) 
        {                              

            services.AddControllers().AddModelValidationAsyncActionFilter()
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.SuppressModelStateInvalidFilter = true;
                });

            services.AddEndpointsApiExplorer();

            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();


            return services;
        }

        public static IServiceCollection JwtConfig(this IServiceCollection services) 
        {
            var key = Encoding.ASCII.GetBytes(Settings.Secret);
            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(o =>
                {
                    o.RequireHttpsMetadata = false;
                    o.SaveToken = true;
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,

                    };
                });

            return services;
        }
    }
}
