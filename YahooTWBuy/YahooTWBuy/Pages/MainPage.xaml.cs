using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using YahooTWBuy.DependencyServices;
using YahooTWBuy.Utilities;
using YahooTWBuy.ViewModels;

namespace YahooTWBuy.Pages
{
    public partial class MainPage : ContentPage
    {
        private MainPageViewModel _mainPageViewModel;
        private static DateTime? _lastBackKeyDownTime;
        private XamarinFormsTimer _mainPageTimer;
        public MainPage()
        {
            InitializeComponent();
            BindingContext = _mainPageViewModel = new MainPageViewModel();

            BuildMainPageTimer();
        }

        private void BuildMainPageTimer()
        {
            _mainPageTimer = new XamarinFormsTimer(new TimeSpan(0, 0, 0, 2), () =>
            {
                _mainPageViewModel.ToastMessageIsVisible = false;
            });
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                case Device.Android:
                    break;
                case Device.UWP:
                case Device.WinPhone:
                        MainGrid.Padding = new Thickness(0, 0, 0, 48);
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
            base.OnBackButtonPressed();

            if (MainWebView.CanGoBack)
            {
                MainWebView.GoBack();
                return true;
            }

            if (!_lastBackKeyDownTime.HasValue || DateTime.Now - _lastBackKeyDownTime.Value > new TimeSpan(0, 0, 0, 1))
            {
                _mainPageTimer?.Start();
                _mainPageViewModel.ToastMessageIsVisible = true;
                _mainPageViewModel.ToastMessage = "再按一次返回鍵 離開此程式...";
                _lastBackKeyDownTime = DateTime.Now;
                return true;
            }

            return false;

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
