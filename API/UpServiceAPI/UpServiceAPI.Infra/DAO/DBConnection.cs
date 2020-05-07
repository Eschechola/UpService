using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using UpServiceAPI.Infra.Interfaces;

namespace UpServiceAPI.Infra.DAO
{
    public class DBConnection : IMySqlConnection
    {
        private readonly IConfiguration _configuration;

        public DBConnection()
        {}

        public DBConnection(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(_configuration["ConnectionString"]);
        }

        public MySqlConnection GetConnection(string connectionString)
        {
            return new MySqlConnection(connectionString);
        }
    }
}
