using System;
using System.Windows.Input;
using UpService.Models;
using UpService.Services;
using Xamarin.Forms;

namespace UpService.ViewModels
{
    public class AddJobViewModel : BaseViewModel
    {
        private Client _Usuario;
        private string _Titulo;
        private string _Descricao;
        private string _TextoQuant;
        private double _MaxValue;
        private bool _IsRunning;

        public Client Usuario
        {
            get => _Usuario;
            set => SetProperty<Client>(ref _Usuario, value, nameof(Usuario));
        }

        public string Titulo
        {
            get => _Titulo;
            set => SetProperty<string>(ref _Titulo, value, nameof(Titulo));
        }
        public string Descricao
        {
            get => _Descricao;
            set
            {
                SetProperty<string>(ref _Descricao, value, nameof(Descricao));
                TextoQuant = (3000 - Descricao.Length).ToString();
            }
        }
        public string TextoQuant
        {
            get => _TextoQuant;
            set => SetProperty<string>(ref _TextoQuant, value, nameof(TextoQuant));
        }
        public double MaxValue
        {
            get => _MaxValue;
            set => SetProperty<double>(ref _MaxValue, value, nameof(MaxValue));
        }
        public bool IsRunning
        {
            get => _IsRunning;
            set => SetProperty<bool>(ref _IsRunning, value, nameof(IsRunning));
        }
        public ICommand EnviarNewJob { get; private set; }
        public AddJobViewModel()
        {
            MaxValue = 30;
            TextoQuant = 3000.ToString();
            EnviarNewJob = new Command(AdicionarNewJob);
        }

        private async void AdicionarNewJob()
        {
            IsRunning = true;
            
            try
            {
                if (string.IsNullOrEmpty(Titulo) || string.IsNullOrWhiteSpace(Titulo) || Titulo.Length < 10)
                {
                    MostrarMensagem.Mostrar("Preencha o campo Titulo. Mínimo de 10 caracteres");
                    IsRunning = false;
                    return;
                }
                if (string.IsNullOrEmpty(Descricao) || string.IsNullOrWhiteSpace(Descricao) || Descricao.Length < 45)
                {
                    MostrarMensagem.Mostrar("É necessário uma descrição do serviço. Mínimo de 45 caracteres");
                    IsRunning = false;
                    return;
                }
                if (MaxValue <= 30)
                {
                    MostrarMensagem.Mostrar("Valor mínimo de R$ 30");
                    IsRunning = false;
                    return;
                }

                bool result = await ConnectionAPI.Connection.AddJob(new Job
                {
                    Id = 0,
                    PublicationDate = DateTime.Now,
                    JobMaxValue = this.MaxValue,
                    Description = Descricao,
                    Title = Titulo,
                    State = "PB",
                    FkIdClientJobRequester = Usuario.Id
                });
                if (result)
                {
                    MostrarMensagem.Mostrar("Serviço adicionado \ncom sucesso");
                    IsRunning = false;
                    await DependencyService.Get<INavigationService>().GoBack();
                }
            }
            catch (NullReferenceException ex)
            {
                MostrarMensagem.Mostrar(ex.Message);
                IsRunning = false;
                return;
            }
            catch (Exception ex)
            {
                MostrarMensagem.Mostrar(ex.Message);
                IsRunning = false;
                return;
            }
        }
    }
}
