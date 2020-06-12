using Xamarin.Forms;
using UpService.Models;
using Xamarin.Forms.Xaml;
using UpService.ViewModels;

namespace UpService.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class JobDetalhe_Prestador : ContentPage
    {
        JobDetalhePrestadorViewModel vm;
        public JobDetalhe_Prestador(Job item, Client user)
        {
            InitializeComponent();
            vm = (JobDetalhePrestadorViewModel)BindingContext;
            vm.Servico = item;
            vm.Usuario = user;
            vm.SetButtonVisibility();
        }
    }
}