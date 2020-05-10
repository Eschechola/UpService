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
        private string _Senha;
        private bool _IsPrestador;
        private bool _IsPassword;
        private bool _ActInd_IsRunning;
        private Color _ActInd_Color;
        private string _Title;

        const string TITLE_IS_CHECKED = "CADASTRO\nPRESTADOR";
        const string TITLE_IS_NO_CHECKED = "CADASTRO\n ";
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
                        ThreadStart ts = () => { ResolveCEP(v); };
                        Thread t = new Thread(ts);
                        t.Priority = ThreadPriority.BelowNormal;
                        t.Start();
                    }
                }
            }
        }
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
            if (cep != null)
            {
                Endereco = cep.Endereco;
                Estado = cep.Estado;
                Cidade = cep.Cidade;
            }
        }


        public string Numero { 
            get => this._Numero;
            set => SetProperty<string>(ref _Numero, value, nameof(Numero));
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
                if (value)
                {
                    Title = TITLE_IS_CHECKED;
                }
                else
                {
                    Title = TITLE_IS_NO_CHECKED;
                }
            }
        }
        public bool IsPassword
        {
            get => this._IsPassword;
            set => SetProperty<bool>(ref _IsPassword, value, nameof(IsPassword));
        }
        public string Title
        {
            get => this._Title;
            set => SetProperty(ref _Title, value, nameof(Title));
        }
        public ICommand SetIsPassword { private set; get; }
        
        public CadastroViewModel()
        {
            SetIsPassword = new Command((e) => {
                IsPassword = !IsPassword;
            });
            IsPassword = true;
            ActInd_IsRunning = false;
            IsPrestador = false;
        }
    }
}
