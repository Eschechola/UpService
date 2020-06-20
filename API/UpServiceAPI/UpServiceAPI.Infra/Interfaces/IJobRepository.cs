using System.Collections.Generic;
using UpServiceAPI.Infra.Entities;

namespace UpServiceAPI.Infra.Interfaces
{
    public interface IJobRepository : IBaseRepository<Job>
    {
        void Insert(Job job);
        void FinishJob(Job job);
        void AcceptOffer(string jobHash, int providerId);
        Job GetByHash(string hash);
        IList<Job> GetAllPublishedJobs(int startIndex = 1, int mountOfPage = 1);
        int GetMountOfJobsPublisheds();
        IList<Job> GetAllFinishedJobs(int clientID);
        IList<Job> GetAllAcceptedJobs(int clientID);
        IList<Job> SearchByTitle(string title);
        IList<Job> SearchByCity(string city);
        Job GetPublishedJob(int id);
        IList<Job> GetAllPublishedJobsByClient(int clientID);
        IList<Job> GetAllPublishedAndAcceptedJobsByClient(int clientID);
    }
}
