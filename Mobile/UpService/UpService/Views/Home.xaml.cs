using UpService.Models;
using UpService.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UpService.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : MasterDetailPage
    {
        private HomeViewModel viewModel;
        public Home()
        {
            InitializeComponent();
            viewModel = (HomeViewModel)BindingContext;
        }

        public Home(Client c)
        {
            InitializeComponent();
            viewModel = (HomeViewModel)BindingContext;
        }
    }
}