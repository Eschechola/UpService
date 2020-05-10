using System.Threading.Tasks;
using UpService.Views;

namespace UpService.Services
{
    public class NavigationService : INavigationService
    {
        public Task GoBack()
        {
            return Xamarin.Forms.Application.Current.MainPage.Navigation.PopAsync();
        }

        public Task NavigateTo_Cadastro()
        {
            return Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new Cadastro());
        }
    }
}
