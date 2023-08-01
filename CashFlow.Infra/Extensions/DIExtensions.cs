using CashFlow.Application.Repositories;
using CashFlow.Infra.Persistence;
using CashFlow.Infra.Persistence.Interfaces;
using CashFlow.Infra.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CashFlow.Infra.Extensions
{
    public static class DIExtensions
    {
        public static IServiceCollection PersistenceConfig(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<ITransactionRespository, TransactionRepository>();            
            services.AddScoped<IUserRepository, UserRepository>();

            var host = configuration["DBHOST"] ?? "localhost";
            var port = configuration["DBPORT"] ?? "3306";
            var password = configuration["MYSQL_PASSWORD"] ?? configuration.GetConnectionString("MYSQL_PASSWORD");
            var userid = configuration["MYSQL_USER"] ?? configuration.GetConnectionString("MYSQL_USER");
            var cashflowdb = configuration["MYSQL_DATABASE"] ?? configuration.GetConnectionString("MYSQL_DATABASE");

            string mySqlConnStr = $"server={host}; userid={userid};pwd={password};port={port};database={cashflowdb}";

            services.AddDbContextPool<CashFlowDbContext>(options =>
                 options.UseMySql(mySqlConnStr,
                 ServerVersion.AutoDetect(mySqlConnStr)));            


            return services;
        }
    }
}
