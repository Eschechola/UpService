using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UpServiceAPI.Infra.Interfaces;

namespace UpService.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly IClientService _clientService;
        private readonly IJobService _jobService;

        public JobController(
            IClientService clientService,
            IJobService jobService
        )
        {
            _clientService = clientService;
            _jobService = jobService;
        }



    }
}