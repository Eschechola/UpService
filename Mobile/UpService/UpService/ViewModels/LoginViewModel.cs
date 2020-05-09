using System.Windows.Input;
using UpService.Services;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace UpService.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private bool _LembrarSenhaIsChecked;
        private string _CPF;
        private string _Senha;
        private bool _IsPassword;
        private bool _ActInd_IsRunning;
        private Color _ActInd_Color;

        public string CPF
        {
            get => _CPF;
            set => SetProperty<string>(ref _CPF, value, nameof(CPF));
        }

        public string Senha
        {
            get => _Senha;
            set => SetProperty<string>(ref _Senha, value, nameof(Senha));
        }
        public bool IsPassword
        {
            get => _IsPassword;
            set => SetProperty<bool>(ref _IsPassword, value, nameof(IsPassword));
        }

        public bool LembrarSenha
        {
            get => _LembrarSenhaIsChecked;
            set => SetProperty<bool>(ref _LembrarSenhaIsChecked, value, nameof(LembrarSenha));
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

        public ICommand SetIsPassword { private set; get; }
        public ICommand GoTo_Cadastro { private set; get; }
        public ICommand GoTo_MainPage{ private set; get; }



        public LoginViewModel()
        {
            SetIsPassword = new Command((e) => {
                IsPassword = !IsPassword;
            });
            GoTo_Cadastro = new Command((e) =>
            {
                DependencyService.Get<INavigationService>().NavigateTo_Cadastro();
            });

            IsPassword = true;
        }
    }
}
