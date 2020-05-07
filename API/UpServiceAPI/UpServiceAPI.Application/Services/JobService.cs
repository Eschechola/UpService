using UpServiceAPI.Infra.DTO;
using UpServiceAPI.Infra.Entities;
using UpServiceAPI.Infra.Interfaces;

namespace UpServiceAPI.Application.Services
{
    public class JobService : IJobService
    {
        private readonly IJobRepository _jobRepository;
        
        public JobService(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public Job Get(int id)
        {
            return _jobRepository.Get(id);
        }

        public Job Insert(Job job)
        {
            return null;
            //return _jobRepository.Insert(job);
        }
    }
}
