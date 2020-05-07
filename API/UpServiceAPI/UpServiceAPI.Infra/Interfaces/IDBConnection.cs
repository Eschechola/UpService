namespace UpServiceAPI.Infra.Interfaces
{
    public interface IDBConnection<T> where T : class
    {
        T GetConnection();
        T GetConnection(string connectionString);
    }
}
