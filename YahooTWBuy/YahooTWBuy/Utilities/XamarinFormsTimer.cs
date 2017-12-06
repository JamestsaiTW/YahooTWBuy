using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace YahooTWBuy.Utilities
{
    public class XamarinFormsTimer
    {
        private readonly TimeSpan timespan;
        private readonly Action callback;

        private CancellationTokenSource cancellation;

        public XamarinFormsTimer(TimeSpan timespan, Action callback)
        {
            this.timespan = timespan;
            this.callback = callback;
            this.cancellation = new CancellationTokenSource();
        }

        public void Start()
        {
            CancellationTokenSource cts = this.cancellation; // safe copy
            Device.StartTimer(this.timespan,
                () => {
                    if (cts.IsCancellationRequested)
                        return false;

                    this.callback.Invoke();
                    return false; // or true for periodic behavior
                });
        }

        public void Stop()
        {
            Interlocked.Exchange(ref this.cancellation, new CancellationTokenSource()).Cancel();
        }
    }
}
