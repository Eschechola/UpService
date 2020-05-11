using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using UpService.Models;
using UpService.Services;
using Xamarin.Forms;

namespace UpService.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private ObservableCollection<Job> _EmAberto;
        private ObservableCollection<Job> _EmProgresso;
        private ObservableCollection<Job> _Finalizados;
        private Client _usuario;
        private bool _IsRefreshing;
        private Job _SelectedItem;

        public Client Usuario
        {
            get => this._usuario;
            set => SetProperty<Client>(ref _usuario, value, nameof(Usuario));
        }
        public ObservableCollection<Job> EmAberto
        {
            get => this._EmAberto;
            set => SetProperty<ObservableCollection<Job>>(ref _EmAberto, value, nameof(EmAberto));
        }

        public ObservableCollection<Job> EmProgresso
        {
            get => this._EmProgresso;
            set => SetProperty<ObservableCollection<Job>>(ref _EmProgresso, value, nameof(EmProgresso));
        }
        public ObservableCollection<Job> Finalizados
        {
            get => this._Finalizados;
            set => SetProperty<ObservableCollection<Job>>(ref _Finalizados, value, nameof(Finalizados));
        }
        public bool IsRefreshing
        {
            get => _IsRefreshing;
            set => SetProperty<bool>(ref _IsRefreshing, value, nameof(IsRefreshing));
        }
        public Job SelectedItem
        {
            get => _SelectedItem;
            set => SetProperty<Job>(ref _SelectedItem, value, nameof(SelectedItem));
        }


        public ICommand RefreshCommand { get; private set; }
        public ICommand SelectedItemChanged { get; private set; }
        public ICommand AddNewJob { get; private set; }
        public HomeViewModel()
        {
            RefreshCommand = new Command(() =>
            {
                return;
            });

            SelectedItemChanged = new Command(GoTo_JobDetail);
            AddNewJob = new Command(GoTo_AddJob);
            ObservableCollection<Job> lista = new ObservableCollection<Job>();
            lista.Add(new Job() { Title = "Titulo do serviço", State = "Em aberto", Description = "Bacon ipsum dolor amet pork loin beef porchetta bacon turducken. Ham tongue turducken t-bone fatback short ribs pork loin ham hock sausage ball tip strip steak kevin drumstick alcatra. Rump doner ribeye corned beef fatback shankle cow. Corned beef pastrami cow sirloin burgdoggen picanha ham hock t-bone venison salami hamburger." });
            lista.Add(new Job() { Title = "Titulo do serviço", State = "Em aberto", Description = "Bacon ipsum dolor amet pork loin beef porchetta bacon turducken. Ham tongue turducken t-bone fatback short ribs pork loin ham hock sausage ball tip strip steak kevin drumstick alcatra. Rump doner ribeye corned beef fatback shankle cow. Corned beef pastrami cow sirloin burgdoggen picanha ham hock t-bone venison salami hamburger." });
            lista.Add(new Job() { Title = "Titulo do serviço", State = "Em aberto", Description = "Bacon ipsum dolor amet pork loin beef porchetta bacon turducken. Ham tongue turducken t-bone fatback short ribs pork loin ham hock sausage ball tip strip steak kevin drumstick alcatra. Rump doner ribeye corned beef fatback shankle cow. Corned beef pastrami cow sirloin burgdoggen picanha ham hock t-bone venison salami hamburger." });
            lista.Add(new Job() { Title = "Titulo do serviço", State = "Em aberto", Description = "Bacon ipsum dolor amet pork loin beef porchetta bacon turducken. Ham tongue turducken t-bone fatback short ribs pork loin ham hock sausage ball tip strip steak kevin drumstick alcatra. Rump doner ribeye corned beef fatback shankle cow. Corned beef pastrami cow sirloin burgdoggen picanha ham hock t-bone venison salami hamburger." });
            lista.Add(new Job() { Title = "Titulo do serviço", State = "Em aberto", Description = "Bacon ipsum dolor amet pork loin beef porchetta bacon turducken. Ham tongue turducken t-bone fatback short ribs pork loin ham hock sausage ball tip strip steak kevin drumstick alcatra. Rump doner ribeye corned beef fatback shankle cow. Corned beef pastrami cow sirloin burgdoggen picanha ham hock t-bone venison salami hamburger." });
            
            EmAberto = lista;
        }

        private async void GoTo_JobDetail()
        {
            try
            {
                Job j = SelectedItem;
                SelectedItem = null;
                if (j != null)
                {
                    await DependencyService.Get<INavigationService>().NavigateTo_JobDetail(j);
                }
            }
            catch (Exception)
            {
                throw new Exception("GoTo_JobDetail");
            }
        }

        private async void GoTo_AddJob()
        {
            try
            {
                await DependencyService.Get<INavigationService>().NavigateTo_AddJob(Usuario);
            }
            catch (Exception ex)
            {
                throw new Exception("GoTo_AddJob");
            }
        }
    }
}

