using MySql.Data.MySqlClient;

namespace UpServiceAPI.Infra.Interfaces
{
    public interface IMySqlConnection : IDBConnection<MySqlConnection>
    {}
}
