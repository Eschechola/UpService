using System.Threading.Tasks;

namespace UpService.Services
{
    public interface INavigationService
    {
        Task GoBack();
        Task NavigateTo_Cadastro();

    }
}
