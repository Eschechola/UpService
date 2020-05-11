using System.Threading.Tasks;
using UpService.Models;

namespace UpService.Services
{
    public interface INavigationService
    {
        Task GoBack();
        Task NavigateTo_Cadastro();
        void NavigateTo_Home(Client c);
        Task NavigateTo_JobDetail(Job j);
        Task NavigateTo_AddJob(Client c);
    }
}
