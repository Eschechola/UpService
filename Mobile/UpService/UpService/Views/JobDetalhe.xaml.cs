using UpService.Models;
using UpService.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UpService.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class JobDetalhe : ContentPage
    {
        JobDetalheViewModel vm;
        public JobDetalhe(Job job)
        {
            InitializeComponent();
            vm = (JobDetalheViewModel)BindingContext;
            vm.JobInDetail = job;
        }
    }
}