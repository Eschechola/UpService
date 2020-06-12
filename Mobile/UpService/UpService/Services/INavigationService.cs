using System.Threading.Tasks;
using UpService.Models;

namespace UpService.Services
{
    public interface INavigationService
    {
        Task GoBack();
        Task NavigateTo_Cadastro();
        Task NavigateTo_EsqueciSenha();
        void NavigateTo_Home(Client c);
        void NavigateTo_HomePrestador(Client c);
        Task NavigateTo_JobDetail(Job j);
        Task NavigateTo_AddJob(Client c);
        Task NavigateTo_JobDetailPrestador(Job itemSelected, Client user);
        void NavigateTo_Login();
        Task NavigateTo_Finalizados(Client client);
        Task NavigateTo_PesquisaJobs(Client client);
    }
}
