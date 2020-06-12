using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Diagnostics;
using UpService.Services;
using UpService.Models;
using Xamarin.Forms;
using UpService.DB;
using System;

namespace UpService.ViewModels
{
    public class HomePrestadorViewModel : BaseViewModel
    {
        private ObservableCollection<Job> _jobsFinalizados;
        private ObservableCollection<Job> _emProgresso;
        private ObservableCollection<Job> _emAberto;

        private bool _IsRefreshing;
        private Job _ItemSelected;
        private Client _usuario;
        private bool _ListView_IsVisible;
        private bool _LabelEmptyView_IsVisible;
        private bool _SetaEsquerda_Visibility;
        private bool _SetaDireita_Visibility;
        private bool _LayoutPai_Visibility;
        private string _TextoEmptyView;
        private string _TextoPesquisa;
        private int _TotalDePaginas;
        private int _PaginaAtual;

        public ICommand LogoutCommand { get; private set; }
        public ICommand RefreshCommand { get; private set; }
        public ICommand BackPageCommand { get; private set; }
        public ICommand ForwardPageCommand { get; private set; }
        public ICommand SelectedItemChanged { get; private set; }
        public ICommand GoTo_FinalizadosCommand { get; private set; }
        public ICommand GoTo_PesquisaJobsCommand { get; private set; }

        public ObservableCollection<Job> Finalizados
        {
            get => this._jobsFinalizados;
            set => SetProperty<ObservableCollection<Job>>(ref _jobsFinalizados, value, nameof(Finalizados));
        }
        public ObservableCollection<Job> EmProgresso
        {
            get => this._emProgresso;
            set => SetProperty<ObservableCollection<Job>>(ref _emProgresso, value, nameof(EmProgresso));
        }
        public ObservableCollection<Job> EmAberto
        {
            get => this._emAberto;
            set
            {
                if (value == null || value.Count == 0)
                {
                    SetProperty<ObservableCollection<Job>>(ref _emAberto, value, nameof(EmAberto));
                    ListView_IsVisible = false;
                    LabelEmptyView_IsVisible = true;
                }
                else
                {
                    SetProperty<ObservableCollection<Job>>(ref _emAberto, value, nameof(EmAberto));
                    ListView_IsVisible = true;
                    LabelEmptyView_IsVisible = false;
                }
            }
        }
        public bool LabelEmptyView_IsVisible
        {
            get => _LabelEmptyView_IsVisible;
            set => SetProperty<bool>(ref _LabelEmptyView_IsVisible, value, nameof(LabelEmptyView_IsVisible));
        }
        public bool SetaEsquerda_Visibility
        {
            get => _SetaEsquerda_Visibility;
            set => SetProperty<bool>(ref _SetaEsquerda_Visibility, value, nameof(SetaEsquerda_Visibility));
        }
        public bool SetaDireita_Visibility
        {
            get => _SetaDireita_Visibility;
            set => SetProperty<bool>(ref _SetaDireita_Visibility, value, nameof(SetaDireita_Visibility));
        }
        public bool LayoutPai_Visibility
        {
            get => _LayoutPai_Visibility;
            set => SetProperty<bool>(ref _LayoutPai_Visibility, value, nameof(LayoutPai_Visibility));
        }
        public bool ListView_IsVisible
        {
            get => _ListView_IsVisible;
            set => SetProperty<bool>(ref _ListView_IsVisible, value, nameof(ListView_IsVisible));
        }
        public int TotalDePaginas
        {
            get => _TotalDePaginas;
            set
            {
                if (value <= 0)
                {
                    SetProperty<int>(ref _TotalDePaginas, 1, nameof(TotalDePaginas));
                }
                else
                {
                    SetProperty<int>(ref _TotalDePaginas, value, nameof(TotalDePaginas));
                }
            }
        }
        public bool IsRefreshing
        {
            get => this._IsRefreshing;
            set => SetProperty<bool>(ref _IsRefreshing, value, nameof(IsRefreshing));
        }
        public Job ItemSelected
        {
            get => this._ItemSelected;
            set
            {
                if (value != null)
                {
                    SetProperty<Job>(ref _ItemSelected, value, nameof(ItemSelected));
                    GoTo_JobDetailPrestador();
                }
                else
                {
                    SetProperty<Job>(ref _ItemSelected, value, nameof(ItemSelected));
                }
            }
        }
        public int PaginaAtual
        {
            get => _PaginaAtual;
            set 
            {   if(value <= 0)
                {
                    SetProperty<int>(ref _PaginaAtual, 1, nameof(PaginaAtual));
                }
                else
                {
                    SetProperty<int>(ref _PaginaAtual, value, nameof(PaginaAtual)); 
                }
            }
        }
        public Client Usuario
        {
            get => this._usuario;
            set => SetProperty<Client>(ref _usuario, value, nameof(Usuario));
        }




        private void Logout()
        {
            try
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
            catch (Exception ex)
            {
                Debug.WriteLine("HomePrestadorViewModel > Logout " + ex.Message);
            }
        }
        private void SetSetaStates()
        {
            if(PaginaAtual == 0)
            {
                PaginaAtual = 1;
            }
            if(PaginaAtual == 1)
            {
                SetaEsquerda_Visibility = false;
            }
            if(PaginaAtual == TotalDePaginas)
            {
                SetaDireita_Visibility = false;
            }
            else if(PaginaAtual < TotalDePaginas)
            {
                SetaDireita_Visibility = true;
            }
        }
        private async void BackPage()
        {
            if(PaginaAtual <= 1)
            {
                SetaEsquerda_Visibility = false;
                return;
            }
            else
            {
                try
                {
                    AllJobsPublishedResult obj = await ConnectionAPI.Connection.GetJobList(PaginaAtual--, 10);
                    if (obj != null)
                    {
                        EmAberto = new ObservableCollection<Job>(obj.jobs);

                        PaginaAtual--;
                        if (PaginaAtual == 1)
                        {
                            SetaEsquerda_Visibility = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MostrarMensagem.Mostrar(ex.Message);
                    Debug.WriteLine("HomePrestadorViewModel > BackPage: " + ex.Message);
                }
            }
        }
        private async void RefreshPage()
        {
            try
            {
                if (PaginaAtual > TotalDePaginas)
                {
                    PaginaAtual = 1;
                }
                AllJobsPublishedResult obj = await ConnectionAPI.Connection.GetJobList(PaginaAtual, 10);
                if (obj != null)
                {
                    EmAberto = new ObservableCollection<Job>(obj.jobs);
                }

                TotalDePaginas = obj.allPages;
                SetSetaStates();
                IsRefreshing = false;
            }
            catch (Exception ex)
            {
                IsRefreshing = false;
                Debug.WriteLine("HomePrestadorViewModel > RefreshPage " + ex.Message);
                MostrarMensagem.Mostrar(ex.Message);
            }
        }
        private async void ForwardPage()
        {
            if(PaginaAtual >= TotalDePaginas)
            {
                SetaDireita_Visibility = false;
                return;
            }
            else
            {
                try
                {
                    AllJobsPublishedResult obj = await ConnectionAPI.Connection.GetJobList(PaginaAtual++, 10);
                    if (obj != null)
                    {
                        EmAberto = new ObservableCollection<Job>(obj.jobs);
                        TotalDePaginas = obj.allPages;
                        PaginaAtual++;
                        if (PaginaAtual == TotalDePaginas)
                        {
                            SetaDireita_Visibility = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("HomePrestadorViewModel > ForwardPage " + ex.Message);
                    MostrarMensagem.Mostrar(ex.Message);
                }
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
                Debug.WriteLine("HomePrestadorVIewModel > GoTo_Finalizados " + ex.Message);
            }
        }
        private void GoTo_JobDetailPrestador()
        {
            if (ItemSelected != null && Usuario != null)
            {
                DependencyService.Get<INavigationService>().NavigateTo_JobDetailPrestador(ItemSelected, Usuario);
                ItemSelected = null;
            }
        }
        private async void GoTo_PesquisaJobs()
        {
            try
            {
                await DependencyService.Get<INavigationService>().NavigateTo_PesquisaJobs(Usuario);
            }
            catch (Exception ex)
            {
                MostrarMensagem.Mostrar(ex.Message);
            }
        }
        public void SetJobsFinalizados(ObservableCollection<Job> finalizados)
        {
            if (finalizados != null)
            {
                Finalizados = finalizados;
            }
        }

        public HomePrestadorViewModel()
        {
            SelectedItemChanged = new Command(GoTo_JobDetailPrestador);
            GoTo_PesquisaJobsCommand = new Command(GoTo_PesquisaJobs);
            GoTo_FinalizadosCommand = new Command(GoTo_Finalizados);
            ForwardPageCommand = new Command(ForwardPage);
            RefreshCommand = new Command(RefreshPage);
            BackPageCommand = new Command(BackPage);
            LogoutCommand = new Command(Logout);
            SetaEsquerda_Visibility = false;
            SetaDireita_Visibility = true;
            TotalDePaginas = 1;
            PaginaAtual = 1;
        }
    }
}
