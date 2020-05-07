using UpServiceAPI.Infra.DTO;
using UpServiceAPI.Infra.Entities;

namespace UpServiceAPI.Infra.Interfaces
{
    public interface IJobService : IBaseService<Job>
    {
        Job Insert(Job job);
    }
}
