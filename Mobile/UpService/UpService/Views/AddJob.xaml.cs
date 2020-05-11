using UpService.Models;
using UpService.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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