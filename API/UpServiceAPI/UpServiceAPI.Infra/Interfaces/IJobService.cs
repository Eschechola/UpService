using System.Collections.Generic;
using UpServiceAPI.Infra.Entities;

namespace UpServiceAPI.Infra.Interfaces
{
    public interface IJobService : IBaseService<Job>
    {
        Job Insert(Job job);
        Job GetByHash(string hash);
        IList<Job> GetAllPublishedJobs(int page = 1, int mountOfPage = 1);
        int GetMountOfJobsPublisheds();
        IList<Job> GetAllFinishedJobs(int clientID = 1);
        IList<Job> GetAllAcceptedJobs(int clientID = 1);
        IList<Job> SearchByTitle(string title);
        IList<Job> SearchByCity(string city);
        Job GetPublishedJob(int id);
        void SendOffer(JobOffer offer);
        void AcceptOffer(string jobHash, int providerId, double valueAccept);
        void FinishJob(int id);
        int GetMaxPublishedPage(int mountOfPage);
    }
}
