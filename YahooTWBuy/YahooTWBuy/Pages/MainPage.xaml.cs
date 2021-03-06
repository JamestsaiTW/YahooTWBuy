﻿using System;
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

        private static MainPageViewModel _mainPageViewModel;
        private static DateTime? _lastBackKeyDownTime;
        private XamarinFormsTimer _mainPageTimer;
        private XamarinFormsTimer _webPageLoadingTimer;
        private double _uwpInitialViewHeight;
        private bool _functionNotWork;

        public MainPage(bool currentNetworkIsConnected, double uwpInitialViewHeight = 0.0)
        {
            InitializeComponent();

            BindingContext = _mainPageViewModel = new MainPageViewModel() {
                 NetworkIsConnected = currentNetworkIsConnected
            };

            BuildMainPageTimer();

            _uwpInitialViewHeight = uwpInitialViewHeight;
        }

        private void BuildMainPageTimer()
        {
            _mainPageTimer = new XamarinFormsTimer(new TimeSpan(0, 0, 0, 2), () =>
            {
                _mainPageViewModel.ToastMessageIsVisible = false;
            });

            _webPageLoadingTimer = new XamarinFormsTimer(new TimeSpan(0, 0, 0, 10), () =>
            {
                _mainPageViewModel.StopLoadingButtonIsVisible = true;
            });
        }

        internal static void RefreshWebView(bool networkIsConnected=true)
        {
            _mainPageViewModel.NetworkIsConnected = networkIsConnected;

            if (_mainWebView != null)
            {
                _mainPageViewModel.StopLoadingButtonIsVisible = false;

                if (_mainPageViewModel.NetworkIsConnected)
                {
                    _mainWebView.Source = (_mainWebView.Source as UrlWebViewSource).Url;
                    _mainPageViewModel.ToastMessageIsVisible = false;
                    _mainPageViewModel.IsBusy = false;
                    return;
                }

                _mainPageViewModel.IsBusy = true;
                _mainPageViewModel.ToastMessageIsVisible = true;
                _mainPageViewModel.ToastMessage = "網路狀態不穩定...";

            }
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
                        System.Diagnostics.Debug.WriteLine($"Xamarin.Forms ViewHeight : {Height}");
                        MainGrid.Padding = Math.Abs(_uwpInitialViewHeight - Height) > 1 ? new Thickness(0, 0, 0, 48)  : new Thickness(0, 0, 0, 0);
                        MessagingCenter.Subscribe<MainPage, bool>(this, "UWP_SystemVirtualButtonBarStatus", (sender, status) =>
                        {
                            MainGrid.Padding = status ? new Thickness(0, 0, 0, 48) : new Thickness(0, 0, 0, 0);
                        });
                    break;
                default:
                    break;
            }

            _mainWebView = MainWebView;
             _mainPageViewModel.IsBusy = true;

            if (! _mainPageViewModel.NetworkIsConnected)
            {
                await DisplayAlert("通知", "目前網路狀態不穩定，無法瀏覽 Yahoo! 購物中心 資料" +
                    "\r\n\r\n請等待網路連線恢復穩定...", "了解");
                RefreshWebView(_mainPageViewModel.NetworkIsConnected);
                return;
            }

            RefreshWebView();

        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                case Device.Android:
                    break;
                case Device.UWP:
                case Device.WinPhone:
                    MessagingCenter.Unsubscribe<MainPage, bool>(this, "UWP_SystemVirtualButtonBarStatus");
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
            if (e.Url.StartsWith("https://tw.rd.yahoo.com/referurl/buy/act/") || e.Url.StartsWith("https://yho.com/shpandroid"))
            {
                _mainWebView.Source = new UrlWebViewSource() { Url = "https://m.tw.buy.yahoo.com/" };
                return;
            }

            if (PageUrlsHandler.CheckIsOk(e.Url)) 
            {
                _webPageLoadingTimer.Start();
                _mainPageViewModel.IsBusy = true;
                _functionNotWork = false;
                return;
            }

           _functionNotWork = true;
        }

        private async void MainWebView_Navigated(object sender, WebNavigatedEventArgs e)
        {
            _webPageLoadingTimer.Stop();
            _mainPageViewModel.StopLoadingButtonIsVisible = false;

            _mainPageViewModel.IsBusy = false;

            if(_functionNotWork)
            {
                await DisplayAlert("通知", "抱歉 Yahoo! 購物中心 此選項的畫面，在本 App 中會無法正確呈現。\r\n\r\nApp 將會回到上一個選項畫面...", "了解");
                _functionNotWork = false;

                if (e.Url == "https://tw.mobi.yahoo.com/")
                { 
                    _mainWebView.Source = new UrlWebViewSource() { Url = "https://m.tw.buy.yahoo.com/" };
                    return;
                }

                _mainWebView.GoBack();
            }

            _mainWebView.Eval(@"(function()
                {
                    var hyperlinks = document.getElementsByTagName('a');
                    for(var i = 0; i < hyperlinks.length; i++)
                    {
                            hyperlinks[i].setAttribute('target', '_self');
                    }
                })()");

        }

        

        private void StopLoadingButton_Clicked(object sender, EventArgs e)
        {
            _mainPageViewModel.StopLoadingButtonIsVisible = false;
            _mainPageViewModel.IsBusy = false;
        }
    }
}
