using CashFlow.Api.Security;
using CashFlow.Application.Services.Interfaces;
using CashFlow.Application.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using CashFlow.Application.Validations.Attributes;

namespace CashFlow.Application.Extensions
{
    public static class DIExtensions
    {
        public static IServiceCollection ApplicationConfig(this IServiceCollection services) 
        {                              

            services.AddControllers(o => { o.Filters.Add<ValidateModelStateAttribute>(); })
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.SuppressModelStateInvalidFilter = true;                    
                });

            services.AddEndpointsApiExplorer();

            services.AddScoped<ITransactionService, TransactionService>();            
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
