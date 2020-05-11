using System.Threading.Tasks;
using UpService.Models;
using UpService.Views;
using Xamarin.Forms;

namespace UpService.Services
{
    public class NavigationService : INavigationService
    {
        public Task GoBack()
        {
            return Xamarin.Forms.Application.Current.MainPage.Navigation.PopAsync();
        }

        public Task NavigateTo_AddJob(Client c)
        {
            return Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new AddJob(c));
        }

        public Task NavigateTo_Cadastro()
        {
            return Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new Cadastro());
        }

        public void NavigateTo_Home(Client c)
        {
            Xamarin.Forms.Application.Current.MainPage = new NavigationPage(new Home(c));
        }

        public Task NavigateTo_JobDetail(Job j)
        {
            return Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new JobDetalhe(j));
        }
    }
}
