using System;
using System.Diagnostics;
using UpService.DB;
using UpService.Models;
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
            try
            {
                using (ManagerLocalDB db = new ManagerLocalDB())
                {
                    if (db.ClientTableIsNull())
                    {
                        MainPage = new NavigationPage(new Login());
                    }
                    else
                    {
                        Client c = db.GetClient();
                        if (c != null)
                        {
                            if (c.Type == "SS")
                            {
                                DependencyService.Get<INavigationService>().NavigateTo_Home(c);
                            }
                            else
                            {
                                DependencyService.Get<INavigationService>().NavigateTo_HomePrestador(c);
                            }
                        }
                        else
                        {
                            MainPage = new NavigationPage(new Login());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("In App: " + ex.Message);
            }
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
