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

        public Task NavigateTo_EsqueciSenha()
        {
            return Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new EsqueciSenha());
        }

        public Task NavigateTo_Finalizados(Client client)
        {
            return Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new JobFinalizados(client));
        }

        public void NavigateTo_Home(Client c)
        {
            Xamarin.Forms.Application.Current.MainPage = new NavigationPage(new Home(c));
        }

        public void NavigateTo_HomePrestador(Client c)
        {
            Xamarin.Forms.Application.Current.MainPage = new NavigationPage(new HomePrestador(c)); ;
        }

        public Task NavigateTo_JobDetail(Job j)
        {
            return Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new JobDetalhe(j));
        }

        public Task NavigateTo_JobDetailPrestador(Job itemSelected, Client user)
        {
            return Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new JobDetalhe_Prestador(itemSelected, user));
        }
        public void NavigateTo_Login()
        {
            Xamarin.Forms.Application.Current.MainPage = new NavigationPage(new Login());
        }

        public Task NavigateTo_PesquisaJobs(Client client)
        {
            // Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new PesquisaJobs(client));
            return Xamarin.Forms.Application.Current.MainPage.Navigation.PushModalAsync(new PesquisaJobs(client));
        }
    }
}
