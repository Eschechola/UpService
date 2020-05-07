namespace UpServiceAPI.Infra.Interfaces
{
    public interface IBaseService<T> where T : class
    {
        T Get(int id);
    }
}
