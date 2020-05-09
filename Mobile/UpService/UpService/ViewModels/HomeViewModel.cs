using System.Collections.Generic;
using System.Collections.ObjectModel;
using UpService.Models;
using Xamarin.Forms;

namespace UpService.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private ObservableCollection<Job> _myJobs;
        private Client _usuario;
        private bool _IsRefreshing;
        public Command RefreshCommand;

        public Client Usuario
        {
            get => this._usuario;
            set => SetProperty<Client>(ref _usuario, value, nameof(Usuario));
        }
        public ObservableCollection<Job> MyJobs
        {
            get => this._myJobs;
            set => SetProperty<ObservableCollection<Job>>(ref _myJobs, value, nameof(MyJobs));
        }
        public bool IsRefreshing
        {
            get => _IsRefreshing;
            set => SetProperty<bool>(ref _IsRefreshing, value, nameof(IsRefreshing));
        }


        public HomeViewModel()
        {
            RefreshCommand = new Command(() => {

            });
            ObservableCollection<Job> lista = new ObservableCollection<Job>();
            lista.Add(new Job() {Title = "teste", State="Finalizado" });
            lista.Add(new Job() {Title = "teste", State="Finalizado" });
            lista.Add(new Job() {Title = "teste", State="Finalizado" });
            lista.Add(new Job() {Title = "teste", State="Em andamento" });
            lista.Add(new Job() {Title = "teste", State="Finalizado" });
            lista.Add(new Job() {Title = "teste", State="Finalizado" });
            lista.Add(new Job() {Title = "teste", State="Finalizado" });
            lista.Add(new Job() {Title = "teste", State = "Em andamento" });
            lista.Add(new Job() {Title = "teste", State = "Em andamento" });
            lista.Add(new Job() {Title = "teste", State = "Finalizado" });
            lista.Add(new Job() {Title = "teste", State = "Em andamento" });
            lista.Add(new Job() {Title = "teste", State="Finalizado" });
            lista.Add(new Job() {Title = "teste", State="Finalizado" });
            lista.Add(new Job() {Title = "teste", State="Finalizado" });
            lista.Add(new Job() {Title = "teste", State="Finalizado" });
            MyJobs = lista;

        }
    }
}
