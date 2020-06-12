using Xamarin.Forms;
using UpService.Models;
using Xamarin.Forms.Xaml;
using UpService.ViewModels;

namespace UpService.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddJob : ContentPage
    {
        AddJobViewModel vm;
        public AddJob(Client c)
        {
            InitializeComponent();
            vm = (AddJobViewModel)BindingContext;
            vm.Usuario = c;
        }
    }
}