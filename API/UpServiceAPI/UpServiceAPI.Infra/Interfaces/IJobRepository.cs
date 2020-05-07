using UpServiceAPI.Infra.Entities;

namespace UpServiceAPI.Infra.Interfaces
{
    public interface IJobRepository : IBaseRepository<Job>
    {
        void Insert(Job job);
    }
}
