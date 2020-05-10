using System;
using System.Collections.Generic;
using UpServiceAPI.Infra.Entities;
using UpServiceAPI.Infra.Interfaces;

namespace UpServiceAPI.Application.Services
{
    public class JobService : IJobService
    {
        private readonly IJobRepository _jobRepository;
        private readonly IEmailService _emailService;
        private readonly IClientService _clientService;

        public JobService(
            IJobRepository jobRepository,
            IEmailService emailService,
            IClientService clientService
        )
        {
            _jobRepository = jobRepository;
            _emailService = emailService;
            _clientService = clientService;
        }

        public Job Get(int id)
        {
            return _jobRepository.Get(id);
        }

        public int GetMountOfJobsPublisheds()
        {
            return _jobRepository.GetMountOfJobsPublisheds();
        }

        public IList<Job> GetAllPublishedJobs(int page = 1, int mountOfPage = 1)
        {
            if (page < 1)
                page = 1;

            if (mountOfPage < 1)
                mountOfPage = 1;

            //qual job ele irá iniciar
            //- 1 pois ele inicia em 1 após o index informado
            var startIndex = ((page - 1) * mountOfPage);

            //qual job ele irá iniciar e quantos ele irá buscar
            return _jobRepository.GetAllPublishedJobs(startIndex, mountOfPage);
        
        }

        public IList<Job> GetAllFinishedJobs(int clientID)
        {
            return _jobRepository.GetAllFinishedJobs(clientID);
        }

        public IList<Job> GetAllAcceptedJobs(int clientID)
        {
            return _jobRepository.GetAllFinishedJobs(clientID);
        }

        public Job GetByHash(string hash)
        {
            return _jobRepository.GetByHash(hash);
        }

        public Job Insert(Job job)
        {
            //gera o hash do pedido e insere
            var hash = Guid.NewGuid().ToString();
            job.Hash = hash;

            job.State = "PB";
            job.PublicationDate = DateTime.Now;
            job.ConclusionDate = null;
            job.FkIdClientJobProvider = null;


            _jobRepository.Insert(job);


            //retorna através do hash gerado
            var jobAdded = GetByHash(hash);

            return jobAdded;
        }

        public IList<Job> SearchByTitle(string title)
        {
            return _jobRepository.SearchByTitle(title.ToLower());
        }

        public IList<Job> SearchByCity(string city)
        {
            return _jobRepository.SearchByCity(city.ToLower());
        }

        public Job GetPublishedJob(int id)
        {
            return _jobRepository.GetPublishedJob(id);
        }

        public void SendOffer(JobOffer offer)
        {
            
            var provider = _clientService.Get(offer.IdJobProvider);
            var job = _jobRepository.Get(offer.IdJob);
            var requester = _clientService.Get(job.FkIdClientJobRequester);

            if (requester != null && provider != null && job != null)
                _emailService.SendOffer(requester, provider, job, offer.ValueOffered);

            else
                throw new Exception();
        }

        public void FinishJob(int id)
        {
            var job = Get(id);

            if (job.State == "PB" || job.State == "FS")
                throw new Exception();

            job.State = "FS";
            job.ConclusionDate = DateTime.Now;

            _jobRepository.FinishJob(job);


            var providerEmail = _clientService.Get(Convert.ToInt32(job.FkIdClientJobProvider)).Email;
            _emailService.SendFinishJob(job, providerEmail);
        }

        public void AcceptOffer(string jobHash, int providerId, double valueAccept)
        {
            _jobRepository.AcceptOffer(jobHash, providerId);

            var job = GetByHash(jobHash);
            var providerEmail = _clientService.Get(providerId).Email;
            _emailService.SendAcceptedOffer(job, providerEmail, valueAccept);
        }
    }
}
