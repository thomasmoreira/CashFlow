using Microsoft.Extensions.Configuration;
using MySqlConnector;
using System.Data;

namespace CashFlow.Infra.Persistence
{
    public class DapperDataContext
    {
        private readonly IConfiguration _configuration;
        public DapperDataContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IDbConnection CreateConnection()
            => new MySqlConnection(_configuration.GetConnectionString("SqlConnection"));

    }
}
