using System.Windows.Input;
using UpService.Models;
using Xamarin.Forms;

namespace UpService.ViewModels
{
    public class AddJobViewModel : BaseViewModel
    {
        private Client _Usuario;
        private string _Titulo;
        private string _Descricao;
        private string _TextoQuant;
        private int _MaxValue;
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
                TextoQuant = (600 - Descricao.Length).ToString();
            }
        }
        public string TextoQuant
        {
            get => _TextoQuant;
            set => SetProperty<string>(ref _TextoQuant, value, nameof(TextoQuant));
        }
        public int MaxValue
        {
            get => _MaxValue;
            set => SetProperty<int>(ref _MaxValue, value, nameof(MaxValue));
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
            TextoQuant = 600.ToString();
            EnviarNewJob = new Command(() => { });
        }
    }
}
