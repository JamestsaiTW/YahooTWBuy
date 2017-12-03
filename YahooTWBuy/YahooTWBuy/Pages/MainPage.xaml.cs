using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using YahooTWBuy.ViewModels;

namespace YahooTWBuy.Pages
{
    public partial class MainPage : ContentPage
    {
        private MainPageViewModel _mainPageViewModel;
        public MainPage()
        {
            InitializeComponent();
            BindingContext = _mainPageViewModel = new MainPageViewModel();
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
                        MessagingCenter.Subscribe<MainPage, bool>(this, "UWP_SystemVirtualButtonBarStatus", (sender, status) =>
                        {
                            MainGrid.Padding = status ? new Thickness(0, 0, 0, 48) : new Thickness(0, 0, 0, 0);
                        });
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
                return true;
            }
            base.OnBackButtonPressed();
            return true;
        }

        private void MainWebView_Navigating(object sender, WebNavigatingEventArgs e)
        {
            _mainPageViewModel.IsBusy = true;
        }

        private void MainWebView_Navigated(object sender, WebNavigatedEventArgs e)
        {
            _mainPageViewModel.IsBusy = false;
        }
    }
}
