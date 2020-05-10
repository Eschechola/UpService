using AutoMapper;
using System.Collections.Generic;
using UpServiceAPI.Infra.Entities;
using UpServiceAPI.Infra.Interfaces;

namespace UpServiceAPI.Application.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public ClientService(
            IClientRepository clientRepository,
            IMapper mapper
        )
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }

        public Client Insert(Client client)
        {
            //insere o cliente e retorna de acordo com o CPF
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

    }
}
