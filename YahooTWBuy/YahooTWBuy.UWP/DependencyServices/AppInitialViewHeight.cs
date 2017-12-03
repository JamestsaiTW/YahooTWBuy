using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YahooTWBuy.DependencyServices;
using YahooTWBuy.UWP.DependencyServices;
using Xamarin.Forms;

[assembly: Dependency(typeof(AppInitialViewHeight))]
namespace YahooTWBuy.UWP.DependencyServices
{
    public class AppInitialViewHeight : IAppInitialViewHeight
    {
        public double Get()
        {
            return MainPage.InitialViewHeight;
        }
    }
}
