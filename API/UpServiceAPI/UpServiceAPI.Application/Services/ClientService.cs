using AutoMapper;
using System.Collections.Generic;
using UpServiceAPI.Infra.Entities;
using UpServiceAPI.Infra.Interfaces;

namespace UpServiceAPI.Application.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;

        public ClientService(
            IClientRepository clientRepository,
            IEmailService emailService,
            IMapper mapper
        )
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
            _emailService = emailService;
        }

        public Client Insert(Client client)
        {
            //insere o cliente e retorna de acordo com o CPF
            client.MountNotes = 0;
            client.SumNotes = 0;
            client.Ranking = 0;
            
             _clientRepository.Insert(client);

            return GetByCpf(client.CPF);
        }

        public Client Update(Client client)
        {
            //insere o cliente e retorna de acordo com o CPF
            _clientRepository.Update(client);

            return GetByCpf(client.CPF);
        }

        public Client Get(int id)
        {
            return _clientRepository.Get(id);
        }

        public Client GetByCpf(string cpf)
        {
            return _clientRepository.GetByCPF(cpf);
        }

        public Client GetByEmail(string email)
        {
            return _clientRepository.GetByEmail(email.ToLower());
        }

        public IList<Client> GetAllByEmail(string email)
        {
            return _clientRepository.GetAllByEmail(email);
        }

        public void SendRequesterEvaluation(int fkIdClient, double note)
        {
            var client = Get(fkIdClient);
            
            client.MountNotes += 1;
            client.SumNotes += note;
            client.Ranking = (client.SumNotes / client.MountNotes);

            _clientRepository.Update(client);
        }

        public void ForgotPassword(Client client, string ip)
        {
            _emailService.SendForgotPassword(client.Email, client.Password, ip);
        }
    }
}
