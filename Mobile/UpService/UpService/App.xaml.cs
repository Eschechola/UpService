using UpService.Services;
using UpService.Views;
using Xamarin.Forms;

namespace UpService
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            DependencyService.Register<INavigationService, NavigationService>();
            MainPage = new NavigationPage(new Login());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
