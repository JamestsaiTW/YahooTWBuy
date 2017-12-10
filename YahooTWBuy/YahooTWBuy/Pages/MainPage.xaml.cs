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
        private static WebView _mainWebView;

        private MainPageViewModel _mainPageViewModel;
        private static DateTime? _lastBackKeyDownTime;
        private XamarinFormsTimer _mainPageTimer;
        public MainPage(bool currentNetworkIsConnected)
        {
            InitializeComponent();
            BindingContext = _mainPageViewModel = new MainPageViewModel() {
                 NetworkIsConnected = currentNetworkIsConnected
            };

            BuildMainPageTimer();


        }

        private void BuildMainPageTimer()
        {
            _mainPageTimer = new XamarinFormsTimer(new TimeSpan(0, 0, 0, 2), () =>
            {
                _mainPageViewModel.ToastMessageIsVisible = false;
            });
        }

        internal static void RefreshWebView()
        {
            if(_mainWebView != null)
                _mainWebView.Source = (_mainWebView.Source as UrlWebViewSource).Url;
        }

        protected async override void OnAppearing()
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

            _mainWebView = MainWebView;

            if (! _mainPageViewModel.NetworkIsConnected)
            {
                await DisplayAlert("通知", "目前網路狀態不穩定，無法讀取購物中心資料...", "了解");
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
