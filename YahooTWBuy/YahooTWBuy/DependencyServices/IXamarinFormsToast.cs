using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YahooTWBuy.DependencyServices
{
    public enum XamarinFormsToastLength
    {
        Long,
        Short,
    }
    public interface IXamarinFormsToast
    {
        void Show(string message, XamarinFormsToastLength length);
    }
}
