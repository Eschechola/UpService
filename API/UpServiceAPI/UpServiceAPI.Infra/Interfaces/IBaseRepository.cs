namespace UpServiceAPI.Infra.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        T Get(int id);
    }
}
