using UpServiceAPI.Infra.DTO;
using UpServiceAPI.Infra.Entities;
using UpServiceAPI.Infra.Interfaces;
using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Net;
using System.Threading.Tasks;

namespace UpService.UI.Controllers
{
    [Route("api/[controller]")]
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
        [Route("/api/v1/insert-client")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
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

        #endregion
    }
}