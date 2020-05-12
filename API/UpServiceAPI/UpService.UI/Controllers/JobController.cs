using System;
using System.Collections.Generic;
using UpServiceAPI.Infra.Entities;
using UpServiceAPI.Infra.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using UpServiceAPI.Application.DTO;

namespace UpService.UI.Controllers
{
    [ApiController]
    public class JobController : ControllerBase
    {
        #region DI Properties

        private readonly IClientService _clientService;
        private readonly IJobService _jobService;

        private readonly IMapper _mapper;
        private readonly IStatusCodeActionResult _errorStatusCode;

        #endregion


        #region Constructors

        public JobController(
            IClientService clientService,
            IJobService jobService,
            
            IMapper mapper,
            IStatusCodeActionResult errorStatusCode)
        {
            _clientService = clientService;
            _jobService = jobService;

            _mapper = mapper;
            _errorStatusCode = errorStatusCode;
        }

        #endregion


        #region Actions

        [HttpPost]
        [Route("/api/v1/job/insert")]
        public IActionResult Insert([FromBody]JobDTO jobDTO)
        {
            try
            {
                //mapeia e valida as entidades 

                var errorsList = new List<string>();

                var job = _mapper.Map<Job>(jobDTO);

                var validator = job.Validate(job);

                foreach (var error in validator.Errors)
                    errorsList.Add(error.ErrorMessage);

                //se estiver válido insere

                if(errorsList.Count > 0)
                    return BadRequest(errorsList);

                var jobAdded = _jobService.Insert(job);
                var jobMapped = _mapper.Map<JobDTO>(jobAdded);

                return Ok(jobMapped);
            }
            catch (Exception)
            {
                return _errorStatusCode;
            }
        }

        [HttpPost]
        [Route("/api/v1/job/send-offer")]
        public IActionResult SendOffer([FromBody]JobOfferDTO jobOffer)
        {
            try
            {
                //verifica se o valor da oferta está nos padrões
                if (jobOffer.ValueOffered < 30 || jobOffer.ValueOffered > 12000)
                    return BadRequest("O valor da oferta deve estar entre R$30,00 e R$12.000,00");


                //verifica se o trabalho existe e não foi realizado ainda
                var jobExists = _jobService.GetPublishedJob(jobOffer.IdJob);

                if (jobExists == null)
                    return BadRequest("O serviço não existe ou já foi finalizado!");


                //verifica se o prestador de serviços existe
                var providerExists = _clientService.Get(jobOffer.IdJobProvider);

                if (providerExists == null)
                    return BadRequest("O prestador de serviços não foi encontrado");


                //caso tudo esteja correto, envia a proposta por email
                var offer = _mapper.Map<JobOffer>(jobOffer);

                _jobService.SendOffer(offer);

                return Ok("A proposta foi enviada para os solicitadores, em breve entrará em contato, Obrigado!");
            }
            catch (Exception)
            {
                return _errorStatusCode;
            }
        }

        [HttpGet]
        [Route("/api/v1/job/accept-offer")]
        public IActionResult AcceptOffer([FromQuery]string jobHash, [FromQuery]int providerId, [FromQuery]double valueAccepted)
        {
            try
            {
                var jobIsAvailable = _jobService.GetByHash(jobHash);

                if (jobIsAvailable.State == "AC" || jobIsAvailable.State == "FS")
                    return BadRequest("O já foi finalizado ou não está mais disponível!");

                _jobService.AcceptOffer(jobHash, providerId, valueAccepted);

                return Ok("O orçamento foi aceito, iremos notificar o prestador de serviços, obrigado!");
            }
            catch (Exception)
            {
                return _errorStatusCode;
            }
        }


        [HttpGet]
        [Route("/api/v1/job/get-all-published-jobs/{page:int}/{mountOfPage:int}")]
        public IActionResult GetAllPublishedJobs(int page = 1, int mountOfPage = 1)
        {
            try
            {
                var allJobs = _jobService.GetAllPublishedJobs(page, mountOfPage);
                
                var jobsMapped = _mapper.Map<IList<JobDTO>>(allJobs);

                var maxPages = _jobService.GetMaxPublishedPage(mountOfPage);

                return Ok( new
                    {
                        allPages = maxPages,
                        jobs = jobsMapped
                    });
            }
            catch (Exception)
            {
                return _errorStatusCode;
            }
        }

        [HttpGet]
        [Route("/api/v1/job/get-all-finished-jobs/{clientID:int}")]
        public IActionResult GetAllFinishedJobs(int clientID = 1)
        {
            try
            {
                var alljobs = _jobService.GetAllFinishedJobs(clientID);

                var jobsMapped = _mapper.Map<IList<JobDTO>>(alljobs);

                return Ok(jobsMapped);
            }
            catch (Exception)
            {
                return _errorStatusCode;
            }
        }


        [HttpGet]
        [Route("/api/v1/job/get-all-accepted-jobs/{clientID:int}")]
        public IActionResult GetAllAcceptedJobs(int clientID = 1)
        {
            try
            {
                var alljobs = _jobService.GetAllAcceptedJobs(clientID);

                var jobsMapped = _mapper.Map<IList<JobDTO>>(alljobs);

                return Ok(jobsMapped);
            }
            catch (Exception)
            {
                return _errorStatusCode;
            }
        }


        [HttpGet]
        [Route("/api/v1/job/search-by-title")]
        public IActionResult SearchByTitle([FromQuery]string title = "")
        {
            try
            {
                var allJobs = _jobService.SearchByTitle(title);

                if (allJobs.Count > 0)
                    return Ok(_mapper.Map<IList<JobDTO>>(allJobs));

                else
                    return BadRequest("Nenhum trabalho encontrado");

            }
            catch (Exception)
            {
                return _errorStatusCode;
            }
        }


        [HttpGet]
        [Route("/api/v1/job/search-by-city")]
        public IActionResult SearchByCity([FromQuery]string city = "")
        {
            try
            {
                var allJobs = _jobService.SearchByCity(city);

                if (allJobs.Count > 0)
                    return Ok(_mapper.Map<IList<JobDTO>>(allJobs));

                else
                    return BadRequest("Nenhum trabalho encontrado");

            }
            catch (Exception)
            {
                return _errorStatusCode;
            }
        }

        [HttpPut]
        [Route("/api/v1/job/finish-job/{id:int}")]
        public IActionResult FinishJob(int id)
        {
            try
            {
                var job = _jobService.Get(id);

                if (job == null)
                    return BadRequest("O Serviço não foi encontrado!");

                _jobService.FinishJob(id);

                return Ok("Trabalho finalizado com sucesso!");
            }
            catch (Exception)
            {
                return _errorStatusCode;
            }
        }

        #endregion
    }
}