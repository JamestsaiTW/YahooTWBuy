using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using YahooTWBuy.ViewModels;

namespace YahooTWBuy
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new YahooTWBuy.Pages.MainPage(CrossConnectivity.Current.IsConnected);

            CrossConnectivity.Current.ConnectivityChanged += (sender, args) => {

                var currentNetworkConnectedStatus = (MainPage.BindingContext as BaseViewModel).NetworkIsConnected;

                if (currentNetworkConnectedStatus == false && args.IsConnected == true)
                         YahooTWBuy.Pages.MainPage.RefreshWebView();

                (MainPage.BindingContext as BaseViewModel).NetworkIsConnected = args.IsConnected;

            };
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
