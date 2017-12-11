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
        public App(double uwpInitialViewHeight = 0.0)
        {
            InitializeComponent();

            MainPage = new YahooTWBuy.Pages.MainPage(CrossConnectivity.Current.IsConnected, uwpInitialViewHeight);

            CrossConnectivity.Current.ConnectivityChanged += (sender, args) => {

               // var baseViewModel = MainPage.BindingContext as BaseViewModel;

                //if (baseViewModel.NetworkIsConnected == false && args.IsConnected == true)
                //{
                         YahooTWBuy.Pages.MainPage.RefreshWebView(args.IsConnected);
                //}

                //baseViewModel.NetworkIsConnected = args.IsConnected;
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
