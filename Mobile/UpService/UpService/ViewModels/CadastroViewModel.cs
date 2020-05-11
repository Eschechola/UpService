using System;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Schema;
using UpService.Models;
using UpService.Services;
using UpService.Validator;
using Xamarin.Forms;

namespace UpService.ViewModels
{
    public class CadastroViewModel : BaseViewModel
    {
        private string _Nome;
        private string _Sobrenome;
        private string _Email;
        private string _CPF;
        private string _Endereco;
        private string _Cidade;
        private string _Estado;
        private string _CEP;
        private string _Numero;
        private string _Telefone;
        private string _Senha;
        private bool _IsPrestador;
        private bool _IsPassword;
        private bool _ActInd_IsRunning;
        private Color _ActInd_Color;

        #region Propertys
        public string Nome { 
            get => this._Nome; 
            set => SetProperty<string>(ref _Nome, value, nameof(Nome)); 
        }
        public string Sobrenome { 
            get => this._Sobrenome;
            set => SetProperty<string>(ref _Sobrenome, value, nameof(Sobrenome));
        }
        public string Email { 
            get => this._Email;
            set => SetProperty<string>(ref _Email, value, nameof(Email));
        }
        public string CPF{ 
            get => this._CPF;
            set => SetProperty<string>(ref _CPF, value, nameof(CPF));
        }
        public string Endereco
        {
            get => this._Endereco;
            set => SetProperty<string>(ref _Endereco, value, nameof(Endereco));
        }
        public string Cidade { 
            get => this._Cidade;
            set => SetProperty<string>(ref _Cidade, value, nameof(Cidade));
        }
        public string Estado { 
            get => this._Estado;
            set => SetProperty<string>(ref _Estado, value, nameof(Estado));
        }
        public string CEP { 
            get => this._CEP;
            set
            {
                SetProperty<string>(ref _CEP, value, nameof(CEP));
                string v = value;
                if(v.Length == 8)
                {
                    if (Validates.IsCEP(v))
                    {
                        void ts() { ResolveCEP(v); }
                        Thread t = new Thread(ts);
                        t.Priority = ThreadPriority.BelowNormal;
                        t.Start();
                    }
                }
            }
        }
        public string Numero { 
            get => this._Numero;
            set => SetProperty<string>(ref _Numero, value, nameof(Numero));
        }
        public string Telefone
        {
            get => this._Telefone;
            set => SetProperty<string>(ref _Telefone, value, nameof(Telefone));
        }
        public string Senha { 
            get => this._Senha;
            set => SetProperty<string>(ref _Senha, value, nameof(Senha));
        }
        public bool IsPrestador
        {
            get => this._IsPrestador;
            set
            {
                SetProperty<bool>(ref _IsPrestador, value, nameof(IsPrestador));
            }
        }
        public bool IsPassword
        {
            get => this._IsPassword;
            set => SetProperty<bool>(ref _IsPassword, value, nameof(IsPassword));
        }
        public Color ActInd_Color
        {
            get => this._ActInd_Color;
            set => SetProperty<Color>(ref _ActInd_Color, value, nameof(ActInd_Color));
        }
        public bool ActInd_IsRunning
        {
            get => this._ActInd_IsRunning;
            set
            {
                SetProperty<bool>(ref _ActInd_IsRunning, value, nameof(ActInd_IsRunning));
                if (value)
                {
                    ActInd_Color = Color.FromHex("#002E6C");
                }
                else
                {
                    ActInd_Color = Color.Transparent;
                }
            }
        }
        #endregion
        private void ResolveCEP(string cep)
        {
            ActInd_IsRunning = true;
            CEP_Consulta consulta = new CEP_Consulta(cep);
            CEP c;
            c = consulta.GetCEPAndMore();
            SetInternalValues(c);
            ActInd_IsRunning = false;
        }
        private void SetInternalValues(CEP cep)
        {
            ActInd_IsRunning = false;
            try
            {
                if (cep != null)
                {
                    Endereco = cep.Endereco;
                    Estado = cep.Estado.ToUpper();
                    Cidade = cep.Cidade.ToUpper();

                }
            }
            catch (Exception)
            {
                Endereco = "";
                Estado = "";
                Cidade = "";
                return;
            }
        }



        public ICommand SetIsPassword { private set; get; }
        public ICommand CadastrarCommand { private set; get; }

        public CadastroViewModel()
        {
            SetIsPassword = new Command((e) => {
                IsPassword = !IsPassword;
            });
            CadastrarCommand =new Command(CadastrarClient);
            IsPassword = true;
            ActInd_IsRunning = false;
            IsPrestador = false;
        }

        private async void CadastrarClient()
        {
            if(string.IsNullOrEmpty(Nome) || string.IsNullOrWhiteSpace(Nome) || Nome.Trim().Length < 5)
            {
                MostrarMensagem.Mostrar("O preencha o campo Nome...");
                return;
            }
            if (string.IsNullOrEmpty(Sobrenome) || string.IsNullOrWhiteSpace(Sobrenome) || Nome.Trim().Length < 5)
            {
                MostrarMensagem.Mostrar("O preencha o campo Sobrenome...");
                return;
            }
            if (!Validates.IsEmail(Email))
            {
                MostrarMensagem.Mostrar("O Email informado é inválido...");
                return;
            }
            if (!Validates.IsCPF(CPF))
            {
                MostrarMensagem.Mostrar("O CPF informado é inválido...");
                return;
            }
            if (!Validates.IsCEP(CEP))
            {
                MostrarMensagem.Mostrar("O CEP informado é inválido...");
                return;
            }
            if (string.IsNullOrEmpty(Endereco) || string.IsNullOrWhiteSpace(Endereco))
            {
                MostrarMensagem.Mostrar("Preencha seu Endereço...");
                return;
            }
            if (string.IsNullOrEmpty(Cidade) || string.IsNullOrWhiteSpace(Cidade))
            {
                MostrarMensagem.Mostrar("Preencha o campo Cidade...");
                return;
            }
            if (string.IsNullOrEmpty(Estado) || string.IsNullOrWhiteSpace(Estado) || Estado.Trim().Length <2)
            {
                MostrarMensagem.Mostrar("Preencha o campo Estado...");
                return;
            }
            if (string.IsNullOrEmpty(Numero) || string.IsNullOrWhiteSpace(Numero))
            {
                MostrarMensagem.Mostrar("Preencha o número de sua casa ou apartamento...");
                return;
            }
            if (!Validates.IsTelephone(Telefone))
            {
                MostrarMensagem.Mostrar("O Telefone informado é inválido...");
                return;
            }
            if (string.IsNullOrEmpty(Senha) || string.IsNullOrWhiteSpace(Senha) || Senha.Trim().Length < 6)
            {
                MostrarMensagem.Mostrar("Digite uma Senha com pelo menos 6 caracteres...");
                return;
            }

            Client c = new Client
            {
                Name = Nome + " " + Sobrenome,
                Email = Email,
                Country = "BRASIL",
                City = Cidade.ToUpper(),
                State = Estado.ToUpper(),
                Street = Endereco,
                Id = 0,
                HomeNumber = 0,
                Password = Senha,
                Telephone = Telefone,
                ZipCode = CEP,
                CPF = CPF,
                Type = IsPrestador ? "PS" : "SS"
            };

            try
            {
                await ConnectionAPI.Connection.AddClient(c);
            }catch(Exception ex)
            {
                MostrarMensagem.Mostrar(ex.Message);
            }
        }
    }
}
