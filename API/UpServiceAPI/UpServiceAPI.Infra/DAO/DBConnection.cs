using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using UpServiceAPI.Infra.Interfaces;

namespace UpServiceAPI.Infra.DAO
{
    public class DBConnection : IMySqlConnection
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DBConnection()
        {}

        public DBConnection(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DBConnection(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public MySqlConnection GetConnection()
        {
            return !string.IsNullOrEmpty(_connectionString) ?
                new MySqlConnection(_connectionString)
                : 
                new MySqlConnection(_configuration["ConnectionString"]);
        }

        public MySqlConnection GetConnection(string connectionString)
        {
            return new MySqlConnection(connectionString);
        }
    }
}
