using System.Collections.Generic;
using UpServiceAPI.Infra.Entities;

namespace UpServiceAPI.Infra.Interfaces
{
    public interface IClientService : IBaseService<Client>
    {
        Client Insert(Client client);
        Client Update(Client client);
        Client GetByCpf(string cpf);
        Client GetByEmail(string email);
        IList<Client> GetAllByEmail(string email);
        void SendRequesterEvaluation(int fkIdClient, double note);
    }
}
