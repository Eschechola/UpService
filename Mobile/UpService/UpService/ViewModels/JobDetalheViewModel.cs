using System;
using System.Windows.Input;
using UpService.Models;
using UpService.Services;
using Xamarin.Forms;

namespace UpService.ViewModels
{
    public class JobDetalheViewModel : BaseViewModel
    {
        private Job _job;
        private float _rating;
        private bool _ratingBarVisiblility;
        private bool _mainButtonVisiblility;
        private string _textoMainButton;

        private bool _ActInd_isRunning;

        public Job JobInDetail
        {
            get => _job;
            set
            {
                SetProperty<Job>(ref _job, value, nameof(JobInDetail));
                if(value.State == "PB")
                {
                    RatingBarVisibility = false;
                    MainButtonVisibility = false;
                }
                else if (value.State == "AC")
                {
                    RatingBarVisibility = false;
                    MainButtonVisibility = true;
                    TextoMainButton = "FINALIZAR SERVIÇO";
                }
                else
                {
                    RatingBarVisibility = true;
                    MainButtonVisibility = true;
                    TextoMainButton = "ENVIAR AVALIAÇÃO";
                }
            }
        }
        public bool RatingBarVisibility
        {
            get => _ratingBarVisiblility;
            set => SetProperty<bool>(ref _ratingBarVisiblility, value, nameof(RatingBarVisibility));
        }
        public bool MainButtonVisibility
        {
            get => _mainButtonVisiblility;
            set => SetProperty<bool>(ref _mainButtonVisiblility, value, nameof(MainButtonVisibility));
        }
        public float Rating
        {
            get => _rating;
            set => SetProperty<float>(ref _rating, value, nameof(Rating));
        }
        public string TextoMainButton
        {
            get => _textoMainButton;
            set => SetProperty<string>(ref _textoMainButton, value, nameof(TextoMainButton));
        }
        public bool ActInd_IsRunning
        {
            get => _ActInd_isRunning;
            set => SetProperty<bool>(ref _ActInd_isRunning, value, nameof(ActInd_IsRunning));
        }
        private async void MainButtonBehavior()
        {
            ActInd_IsRunning = true;
            try
            {
                if (JobInDetail.State == "AC")
                {
                    bool b = await App.Current.MainPage.DisplayAlert("Finalizar serviço", "Deseja realmente finalizar esse serviço? A mudança é irreversível", "Sim", "Não");
                    if (b)
                    {
                        bool res = await ConnectionAPI.Connection.FinishJob(JobInDetail.Id);
                        if (res)
                        {
                            MostrarMensagem.Mostrar("Serviço finalizado com sucesso");
                            await DependencyService.Get<INavigationService>().GoBack();
                        }
                        ActInd_IsRunning = false;
                    }
                    else
                    {
                        ActInd_IsRunning = false;
                        return;
                    }
                }
                else
                {
                    try
                    {
                        bool res = await ConnectionAPI.Connection.SendFeedback(JobInDetail, Rating);
                        if (res)
                        {
                            MostrarMensagem.Mostrar("Avaliação enviada com sucesso");
                            await DependencyService.Get<INavigationService>().GoBack();
                        }
                    }
                    catch (Exception ex)
                    {
                        ActInd_IsRunning = false;
                        MostrarMensagem.Mostrar(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarMensagem.Mostrar(ex.Message);
            }
        }
        public ICommand MainButtonCommand { get; private set; }

        public JobDetalheViewModel()
        {
            MainButtonCommand = new Command(MainButtonBehavior);
        }
        
    }
}
