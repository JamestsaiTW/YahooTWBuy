using System;

using Windows.UI.ViewManagement;
using Xamarin.Forms;
using YahooTWBuy.UWP.Helpers;
using System.Diagnostics;

using Windows.Graphics.Display;


namespace YahooTWBuy.UWP
{

    public sealed partial class MainPage
    {
        private static YahooTWBuy.App _mainApp;
        public static YahooTWBuy.App MainApp => _mainApp ?? (_mainApp = new YahooTWBuy.App() );
        private bool IsSystemVirtualButtonBarShow => InitialViewHeight >= _currentViewHeight && DisplayInformation.GetForCurrentView().CurrentOrientation == DisplayOrientations.Portrait;

        public static readonly double InitialViewHeight = ApplicationView.GetForCurrentView().VisibleBounds.Height;
        private double _currentViewHeight;
        public MainPage()
        {
            this.InitializeComponent();
            ShowStatusBar();

            if (DeviceTypeHelper.GetDeviceFromFactorType() == DeviceFromFactorType.Phone)
            {

                ApplicationView.GetForCurrentView().VisibleBoundsChanged += (sender, e) =>
                {
                    _currentViewHeight = ApplicationView.GetForCurrentView().VisibleBounds.Height;

                    Debug.WriteLine("current:" + _currentViewHeight + "; initial:" + InitialViewHeight + ";  BarStatus:" + IsSystemVirtualButtonBarShow);

                    MessagingCenter.Send<YahooTWBuy.Pages.MainPage, bool>((YahooTWBuy.Pages.MainPage)MainApp.MainPage,
                                                                                                                                    "UWP_SystemVirtualButtonBarStatus", IsSystemVirtualButtonBarShow);

                };
            }

            LoadApplication(MainApp);
        }



        private async void HideStatusBar()
        {
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            {
                var statusbar = Windows.UI.ViewManagement.StatusBar.GetForCurrentView();
                await statusbar.HideAsync();
            }
        }

        private async void ShowStatusBar()
        {
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            {
                var statusbar = Windows.UI.ViewManagement.StatusBar.GetForCurrentView();
                await statusbar.ShowAsync();
                statusbar.BackgroundOpacity = 1;
            }
        }
    }
}
