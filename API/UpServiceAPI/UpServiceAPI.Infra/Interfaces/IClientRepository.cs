using System.Collections.Generic;
using UpServiceAPI.Infra.Entities;

namespace UpServiceAPI.Infra.Interfaces
{
    public interface IClientRepository : IBaseRepository<Client>
    {
        void Insert(Client address);
        void Update(Client client);
        Client GetByCPF(string cpf);
        Client GetByEmail(string email);
        IList<Client> GetAllByEmail(string email);
    }
}
