using auto_shop.Pages;
using auto_shop.Helpers;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace auto_shop
{
    public partial class App : Application
    {
        public App()
        {
            // Initialize Live Reload.
            InitializeComponent();
            MainPage = new NavigationPage(new LoginPage()) {
                //BarBackgroundColor = Color.Black
                BarBackgroundColor = Color.FromHex("#0C67C2")
            };
        }

        protected override void OnStart()
        {
            AppCenter.Start("ios=7c3e9eff-c3ff-4b84-a45f-1a580221c3e6;" + "android=b0b4f3b7-7720-495d-b771-b947dd05e42c;", typeof(Analytics), typeof(Crashes));
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
