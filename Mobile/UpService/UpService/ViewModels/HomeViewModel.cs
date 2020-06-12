using System.Collections.Generic;
using System.Windows.Input;
using UpService.Services;
using UpService.Models;
using Xamarin.Forms;
using UpService.DB;
using System;

namespace UpService.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private bool _LabelEmptyView_IsVisible;
        private bool _ListView_IsVisible;
        private List<Job> _EmProgresso;
        private bool _IsRefreshing;
        private Job _SelectedItem;
        private Client _usuario;

        public Client Usuario
        {
            get => this._usuario;
            set => SetProperty<Client>(ref _usuario, value, nameof(Usuario));
        }
        public bool IsRefreshing
        {
            get => _IsRefreshing;
            set => SetProperty<bool>(ref _IsRefreshing, value, nameof(IsRefreshing));
        }
        public Job SelectedItem
        {
            get => _SelectedItem;
            set
            {
                if (value != null)
                {
                    SetProperty<Job>(ref _SelectedItem, value, nameof(SelectedItem));
                    GoTo_JobDetail();
                }
                else
                {
                    SetProperty<Job>(ref _SelectedItem, value, nameof(SelectedItem));
                }
            }
        }
        public List<Job> EmProgresso
        {
            get => _EmProgresso;
            set
            {
                if (value == null || value.Count == 0)
                {
                    SetProperty<List<Job>>(ref _EmProgresso, value, nameof(EmProgresso));
                    LabelEmptyView_IsVisible = true;
                    ListView_IsVisible = false;
                }
                else
                {
                    SetProperty<List<Job>>(ref _EmProgresso, value, nameof(EmProgresso));
                    LabelEmptyView_IsVisible = false;
                    ListView_IsVisible = true;
                }
            }
        }
        public bool ListView_IsVisible
        {
            get => _ListView_IsVisible;
            set => SetProperty<bool>(ref _ListView_IsVisible, value, nameof(ListView_IsVisible));
        }
        public bool LabelEmptyView_IsVisible
        {
            get => _LabelEmptyView_IsVisible;
            set => SetProperty<bool>(ref _LabelEmptyView_IsVisible, value, nameof(LabelEmptyView_IsVisible));
        }

        public ICommand RefreshCommand { get; private set; }
        public ICommand SelectedItemChanged { get; private set; }
        public ICommand AddNewJob { get; private set; }
        public ICommand LogoutCommand { get; private set; }
        public ICommand GoToFinalizadosCommand { get; private set; }
        public async void SetList()
        {
            try
            {
                List<Job> lista1 = await ConnectionAPI.Connection.GetAllAcceptedJobs(Usuario.Id);
                List<Job> lista2 = await ConnectionAPI.Connection.GetAllPublishedJobsByClient(Usuario.Id);
                List<Job> em_progresso = new List<Job>();
                em_progresso = lista1;
                for (int i = 0; i < lista2.Count; i++)
                {
                    em_progresso.Add(lista2[i]);
                }

                if (em_progresso != null)
                {
                    EmProgresso = em_progresso;
                }
            }
            catch (Exception ex)
            {
                MostrarMensagem.Mostrar(ex.Message);
            }
        }
        private async void RefreshingPage()
        {
            try
            {
                if (Usuario == null)
                {
                    IsRefreshing = false;
                    return;
                }
                List<Job> lista1 = await ConnectionAPI.Connection.GetAllAcceptedJobs(Usuario.Id);
                List<Job> lista2 = await ConnectionAPI.Connection.GetAllPublishedJobsByClient(Usuario.Id);
                List<Job> em_progresso = new List<Job>();
                em_progresso = lista1;
                for (int i = 0; i < lista2.Count; i++)
                {
                    em_progresso.Add(lista2[i]);
                }

                if (em_progresso != null)
                {
                    EmProgresso = em_progresso;
                }
                IsRefreshing = false;
            }
            catch (Exception ex)
            {
                IsRefreshing = false;
                MostrarMensagem.Mostrar(ex.Message);
            }
        }
        private async void GoTo_JobDetail()
        {
            try
            {
                if (SelectedItem != null)
                {
                    Job j = SelectedItem;
                    SelectedItem = null;
                    await DependencyService.Get<INavigationService>().NavigateTo_JobDetail(j);
                }
            }
            catch (Exception ex)
            {
                MostrarMensagem.Mostrar(ex.Message);
            }
        }
        private async void GoTo_AddJob()
        {
            try
            {
                if (Usuario != null)
                {
                    await DependencyService.Get<INavigationService>().NavigateTo_AddJob(Usuario);
                }
                else
                {
                    MostrarMensagem.Mostrar("Erro: Faça o logout e tente novamente");
                    return;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("GoTo_AddJob");
            }
        }
        private async void GoTo_Finalizados()
        {
            try
            {
                await DependencyService.Get<INavigationService>().NavigateTo_Finalizados(Usuario);
            }
            catch (Exception ex)
            {
                MostrarMensagem.Mostrar(ex.Message);
            }
        }
        private void Logout()
        {
            using (ManagerLocalDB db = new ManagerLocalDB())
            {
                bool r = db.DeleteAllClient();
                if (r)
                {
                    DependencyService.Get<INavigationService>().NavigateTo_Login();
                }
                else
                {
                    MostrarMensagem.Mostrar("Erro ao tentar deslogar...");
                }
            }
        }

        public HomeViewModel()
        {
            GoToFinalizadosCommand = new Command(GoTo_Finalizados);
            SelectedItemChanged = new Command(GoTo_JobDetail);
            RefreshCommand = new Command(RefreshingPage);
            AddNewJob = new Command(GoTo_AddJob);
            LogoutCommand = new Command(Logout);
        }
        
    }
}

