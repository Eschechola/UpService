using UpServiceAPI.Infra.DTO;
using UpServiceAPI.Infra.Entities;

namespace UpServiceAPI.Infra.Interfaces
{
    public interface IClientService : IBaseService<Client>
    {
        Client Insert(Client client);
        Client GetByCpf(string cpf);
        Client GetByEmail(string email);
    }
}
