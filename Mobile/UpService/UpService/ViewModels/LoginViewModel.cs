using System;
using System.Windows.Input;
using UpService.Models;
using UpService.Services;
using UpService.Validator;
using Xamarin.Forms;

namespace UpService.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private bool _LembrarSenhaIsChecked;
        private string _CPF_ou_Email;
        private string _Senha;
        private bool _IsPassword;
        private bool _ActInd_IsRunning;

        public string CPF_ou_Email
        {
            get => _CPF_ou_Email;
            set => SetProperty<string>(ref _CPF_ou_Email, value, nameof(CPF_ou_Email));
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
        public bool ActInd_IsRunning
        {
            get => this._ActInd_IsRunning;
            set => SetProperty<bool>(ref _ActInd_IsRunning, value, nameof(ActInd_IsRunning));
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
            GoTo_MainPage = new Command(Logar);
            IsPassword = true;
        }

        private async void Logar()
        {
            ActInd_IsRunning = true;
            try
            {
                if (string.IsNullOrEmpty(Senha) || string.IsNullOrWhiteSpace(Senha))
                {
                    MostrarMensagem.Mostrar("A senha é necessária para realizar o login...");
                    ActInd_IsRunning = false;
                    return;
                }
                if (Validates.IsCPF(CPF_ou_Email) || Validates.IsEmail(CPF_ou_Email))
                {
                    Client c = await ConnectionAPI.Connection.GetClient(CPF_ou_Email, Senha);
                    ActInd_IsRunning = false;
                    DependencyService.Get<INavigationService>().NavigateTo_Home(c);
                }
                else
                {
                    MostrarMensagem.Mostrar("CPF ou Email inválidos...");
                    ActInd_IsRunning = false;
                    return;
                }
            }
            catch(Exception ex)
            {
                ActInd_IsRunning = false;
                MostrarMensagem.Mostrar(ex.Message);
            }
            
        }
    }
}
