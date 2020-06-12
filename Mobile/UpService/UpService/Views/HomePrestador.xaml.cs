using Xamarin.Forms;
using UpService.Models;
using UpService.Services;
using Xamarin.Forms.Xaml;
using UpService.ViewModels;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace UpService.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePrestador : MasterDetailPage
    {
        HomePrestadorViewModel vm;
        public HomePrestador(Client c)
        {
            InitializeComponent();
            vm = (HomePrestadorViewModel)BindingContext;
            vm.Usuario = c;
            SetList();            
        }
        private async void SetList()
        {
            try
            {
                AllJobsPublishedResult obj = await ConnectionAPI.Connection.GetJobList(1, 10);
                if (obj != null)
                {
                    vm.EmAberto = new ObservableCollection<Job>(obj.jobs);
                    vm.TotalDePaginas = obj.allPages;
                    vm.SetaEsquerda_Visibility = false;
                    if (vm.TotalDePaginas <= 1)
                    {
                        vm.SetaDireita_Visibility = false;
                    }
                    else
                    {
                        vm.SetaDireita_Visibility = true;
                    }
                }
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine("HomePrestador: " + ex.Message);
                MostrarMensagem.Mostrar(ex.Message);
                return;
            }
        }
    }
}