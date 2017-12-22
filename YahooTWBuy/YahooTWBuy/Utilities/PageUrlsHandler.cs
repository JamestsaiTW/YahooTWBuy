using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YahooTWBuy.Utilities
{
    public class PageUrlsHandler
    {
        public static bool CheckIsOk(string url) {  return PageUrls.Any(item=> url.StartsWith(item)); }

        private static HashSet<string> PageUrls = new HashSet<string>()
        {
            "https://m.tw.buy",
            "https://m.tw.pay.buy.yahoo.com",
            "https://m.tw.search.buy.yahoo.com/search/shopping/product",
            "https://login.yahoo.com",
            "https://tw.buy.yahoo.com/coupons",
            "https://tw.search.buy.yahoo.com/search/shopping/product",
            "https://tw.buy.yahoo.com/activity",
            "https://tw.buy.yahoo.com/help/helper.asp?p=redeem_01",
            "https://tw.rd.yahoo.com/referurl/buy/act/"
        };
    }
}
