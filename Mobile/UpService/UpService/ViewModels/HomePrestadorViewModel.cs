using System.Collections.ObjectModel;
using UpService.Models;
using Xamarin.Forms;

namespace UpService.ViewModels
{
    public class HomePrestadorViewModel : BaseViewModel
    {
        private ObservableCollection<Job> _jobsFinalizados;
        private ObservableCollection<Job> _emProgresso;
        private bool _IsRefreshing;
        private Job _ItemSelected;
        private Client _usuario;

        public Command RefreshCommand;
        public Command SelectedItem;
        public Command SelectedItemChanged;

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

        public Client Usuario
        {
            get => this._usuario;
            set => SetProperty<Client>(ref _usuario, value, nameof(Usuario));
        }

        public Job ItemSelected
        {
            get => this._ItemSelected;
            set => SetProperty<Job>(ref _ItemSelected, value, nameof(ItemSelected));
        }
        public bool IsRefreshing
        {
            get => this._IsRefreshing;
            set => SetProperty<bool>(ref _IsRefreshing, value, nameof(IsRefreshing));
        }

        public HomePrestadorViewModel()
        {
            ObservableCollection<Job> lista = new ObservableCollection<Job>();
            lista.Add(new Job() { Title = "Titulo do serviço", State = "Em aberto", Description = "Bacon ipsum dolor amet pork loin beef porchetta bacon turducken. Ham tongue turducken t-bone fatback short ribs pork loin ham hock sausage ball tip strip steak kevin drumstick alcatra. Rump doner ribeye corned beef fatback shankle cow. Corned beef pastrami cow sirloin burgdoggen picanha ham hock t-bone venison salami hamburger." });
            lista.Add(new Job() { Title = "Titulo do serviço", State = "Em aberto", Description = "Bacon ipsum dolor amet pork loin beef porchetta bacon turducken. Ham tongue turducken t-bone fatback short ribs pork loin ham hock sausage ball tip strip steak kevin drumstick alcatra. Rump doner ribeye corned beef fatback shankle cow. Corned beef pastrami cow sirloin burgdoggen picanha ham hock t-bone venison salami hamburger." });
            EmProgresso = lista;
            ObservableCollection<Job> lista2 = new ObservableCollection<Job>();
            lista.Add(new Job() { Title = "Titulo do serviço", State = "Finalizados", Description = "Bacon ipsum dolor amet pork loin beef porchetta bacon turducken. Ham tongue turducken t-bone fatback short ribs pork loin ham hock sausage ball tip strip steak kevin drumstick alcatra. Rump doner ribeye corned beef fatback shankle cow. Corned beef pastrami cow sirloin burgdoggen picanha ham hock t-bone venison salami hamburger." });
            lista.Add(new Job() { Title = "Titulo do serviço", State = "Finalizados", Description = "Bacon ipsum dolor amet pork loin beef porchetta bacon turducken. Ham tongue turducken t-bone fatback short ribs pork loin ham hock sausage ball tip strip steak kevin drumstick alcatra. Rump doner ribeye corned beef fatback shankle cow. Corned beef pastrami cow sirloin burgdoggen picanha ham hock t-bone venison salami hamburger." });
            Finalizados = lista2;
        }
    }
}
