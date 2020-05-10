using UpServiceAPI.Infra.Entities;
using UpServiceAPI.Infra.Interfaces;
using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Net;
using UpServiceAPI.Services.DTO;
using UpServiceAPI.Application.DTO;

namespace UpService.UI.Controllers
{
    [ApiController]
    public class ClientController : ControllerBase
    {
        #region DI Properties

        private readonly IClientService _clientService;
        private readonly IJobService _jobService;

        private readonly IStatusCodeActionResult _errorStatusCode;
        private readonly IMapper _mapper;

        #endregion


        #region Constructors

        public ClientController(
            IClientService clientService,
            IJobService jobService,

            IStatusCodeActionResult errorStatusCode,
            IMapper mapper
        )
        {
            _clientService = clientService;
            _jobService = jobService;
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
                var cpfExists = _clientService.GetByCpf(login.Username);

                if (cpfExists != null)
                    clientExists = _mapper.Map<ClientDTO>(cpfExists);


                //verifica se existe um EMAIL com o login informado
                var emailExists = _clientService.GetByEmail(login.Username);

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


        #endregion
    }
}