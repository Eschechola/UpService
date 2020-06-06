using UpServiceAPI.Infra.Entities;
using UpServiceAPI.Infra.Interfaces;
using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using UpServiceAPI.Services.DTO;
using UpServiceAPI.Application.DTO;
using System.Diagnostics;
using ESCHENet.Http.Functions;
using Microsoft.AspNetCore.Http;

namespace UpService.UI.Controllers
{
    [ApiController]
    public class ClientController : ControllerBase
    {
        #region DI Properties

        private readonly IClientService _clientService;
        private readonly IJobService _jobService;

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IStatusCodeActionResult _errorStatusCode;
        private readonly IMapper _mapper;

        #endregion


        #region Constructors

        public ClientController(
            IClientService clientService,
            IJobService jobService,

            IHttpContextAccessor httpContextAccessor,
            IStatusCodeActionResult errorStatusCode,
            IMapper mapper
        )
        {
            _clientService = clientService;
            _jobService = jobService;
            _httpContextAccessor = httpContextAccessor;
            _errorStatusCode = errorStatusCode;
            _mapper = mapper;
        }

        #endregion


        #region Actions

        [HttpPost]
        [Route("/api/v1/client/insert")]
        public IActionResult Insert([FromBody]ClientDTO clientDTO)
        {
            try
            {
                //verifica se já existe um cliente com o mesmo nome
                //ou com o mesmo CPF

                var cpfExists = _clientService.GetByCpf(clientDTO.CPF);

                if (cpfExists != null)
                    return BadRequest("Já existe um cliente cadastrado com o CPF informado");


                var emailExists = _clientService.GetByEmail(clientDTO.Email);

                if (emailExists != null)
                    return BadRequest("Já existe um cliente cadastrado com o EMAIL informado");


                //valida a entitidade, se estiver valida insere e retorna seus dados

                var errorsList = new List<string>();

                var client = _mapper.Map<Client>(clientDTO);

                var validator = client.Validate(client);

                foreach (var error in validator.Errors)
                    errorsList.Add(error.ErrorMessage);


                if (errorsList.Count > 0)
                    return BadRequest(errorsList);

                var clientAdded = _clientService.Insert(client);
                var clientMapped = _mapper.Map<ClientDTO>(clientAdded);

                return Ok(clientMapped);
            }
            catch (Exception)
            {
                return _errorStatusCode;
            }
        }


        [HttpPost]
        [Route("/api/v1/client/login")]
        public IActionResult Login([FromBody]LoginDTO login)
        {
            try
            {
                ClientDTO clientExists = null;
                
                //verifica se existe um CPF com o login informado
                var cpfExists = _clientService.GetByCpf(login.EmailOrCpf);

                if (cpfExists != null)
                    clientExists = _mapper.Map<ClientDTO>(cpfExists);


                //verifica se existe um EMAIL com o login informado
                var emailExists = _clientService.GetByEmail(login.EmailOrCpf);

                if (emailExists != null)
                    clientExists = _mapper.Map<ClientDTO>(emailExists);


                //se o cliente existir verifica se a senha está correta
                if (clientExists != null)
                {
                    if(clientExists.Password == login.Password)
                    {
                        return Ok(clientExists);
                    }
                }

                return BadRequest("Login e/ou senha estão inválidos!");
            }
            catch (Exception)
            {
                return _errorStatusCode;
            }
        }

        [HttpPost]
        [Route("/api/v1/client/forgot-password/{emailOrCpf}")]
        public IActionResult ForgotPassword(string emailOrCpf)
        {
            try
            {
                var ip = new IP(_httpContextAccessor).GetRequestIP();

                var emailExists = _clientService.GetByEmail(emailOrCpf);
                var cpfExists = _clientService.GetByCpf(emailOrCpf);

                if(emailExists != null)
                {
                    _clientService.ForgotPassword(emailExists, ip);

                    return Ok("Sua senha foi enviada com sucesso para seu endereço de email!");
                }

                if(cpfExists != null)
                {
                    _clientService.ForgotPassword(cpfExists, ip);

                    return Ok("Sua senha foi enviada com sucesso para seu endereço de email!");
                }

                return BadRequest("Email e/ou CPF não encontrado na nossa base de dados.");
                
            }
            catch (Exception)
            {
                return _errorStatusCode;
            }
        }

        [HttpPatch]
        [Route("/api/v1/client/send-requester-evaluation")]
        public IActionResult SendRequesterEvaluation([FromBody]RankDTO rank)
        {
            try
            {
                if(rank.Note < 1 || rank.Note > 5)
                    return BadRequest("A nota deve estar entre 1 e 5.");

                var clientExists = _clientService.Get(rank.FkIdClient);

                if(clientExists == null)
                    return BadRequest("O cliente informado não foi encontrado!");

                _clientService.SendRequesterEvaluation(rank.FkIdClient, rank.Note);

                return Ok("Avaliação enviada com sucesso!");
            }
            catch(Exception)
            {
                return _errorStatusCode;
            }
        }

        [HttpPut]
        [Route("/api/v1/client/update")]
        public IActionResult Update([FromBody]ClientDTO clientDTO)
        {
            try
            {
                //verifica se existe algum usuário utilziando o email informado
                var emailExists = _clientService.GetAllByEmail(clientDTO.Email);

                if (emailExists.Count > 1)
                    return BadRequest("O email já está sendo utilizado por outro usuário");

                //verifica se existe algum erro de validação
                var errorsList = new List<string>();

                var client = _mapper.Map<Client>(clientDTO);
                var clientValidator = client.Validate(client);

                foreach (var error in clientValidator.Errors)
                    errorsList.Add(error.ErrorMessage);

                if (errorsList.Count > 0)
                    return BadRequest(errorsList);

                //atualiza e retorna os novos dados do cliente
                var clientUpdated = _clientService.Update(client);

                var clientUpdatedMapper = _mapper.Map<ClientDTO>(clientUpdated);

                return Ok(clientUpdatedMapper);
            }
            catch (Exception)
            {
                return _errorStatusCode;
            }
        }

        [HttpGet]
        [Route("/api/v1/client/get/{id:int}")]
        public IActionResult Get(int id)
        {
            try
            {
                var client = _clientService.Get(id);

                if (client == null)
                    return BadRequest("Usuário não inexistente.");

                var clientMapped = _mapper.Map<ClientDTO>(client);

                return Ok(clientMapped);
            }
            catch (Exception)
            {
                return _errorStatusCode;
            }
        }
        #endregion
    }
}