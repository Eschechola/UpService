using UpServiceAPI.Infra.Entities;

namespace UpServiceAPI.Infra.Interfaces
{
    public interface IClientRepository : IBaseRepository<Client>
    {
        void Insert(Client address);
        Client GetByCPF(string cpf);
        Client GetByEmail(string email);
    }
}
