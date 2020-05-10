using UpServiceAPI.Infra.Entities;

namespace UpServiceAPI.Infra.Interfaces
{
    public interface IEmailService
    {
        void SendOffer(Client requester, Client provider, Job job, double offerValue);
        void SendAcceptedOffer(Job job, string emailProvider, double valueAccept);
        void SendFinishJob(Job job, string emailProvider);
    }
}
