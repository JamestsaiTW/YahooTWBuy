using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace YahooTWBuy
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                case Device.Android:
                case Device.UWP:
                case Device.WinPhone:
                    break;
                default:
                    break;
            }
        }
        protected override bool OnBackButtonPressed()
        {
            if (MainWebView.CanGoBack)
            {
                MainWebView.GoBack();
                return false;
            }
            return base.OnBackButtonPressed();
        }
    }
}
