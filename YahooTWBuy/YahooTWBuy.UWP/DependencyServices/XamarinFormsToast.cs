using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using YahooTWBuy.DependencyServices;
using Xamarin.Forms;

namespace YahooTWBuy.UWP.DependencyServices
{
    public class XamarinFormsToast : IXamarinFormsToast
    {
        public async void Show(string message, XamarinFormsToastLength length)
        {
            MessagingCenter.Send<YahooTWBuy.Pages.MainPage, string>((YahooTWBuy.Pages.MainPage)MainApp.MainPage,
                                                                                                                                    "UWP_Xamarin.FormsToast");

        }
    }
}
