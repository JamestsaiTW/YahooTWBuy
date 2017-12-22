using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YahooTWBuy.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private string _toastMessage;
        public string ToastMessage
        {
            get { return _toastMessage; }
            set {
                if(_toastMessage != value)
                    _toastMessage = value;
                OnPropertyChanged();
            }
        }

        private bool _toastMessageIsVisible;
        public bool ToastMessageIsVisible
        {
            get { return _toastMessageIsVisible; }
            set
            {
                if (_toastMessageIsVisible != value)
                    _toastMessageIsVisible = value;
                OnPropertyChanged();
            }
        }

        private bool _stopLoadingButtonIsVisible;
        public bool StopLoadingButtonIsVisible
        {
            get { return _stopLoadingButtonIsVisible; }
            set
            {
                if (_stopLoadingButtonIsVisible != value)
                    _stopLoadingButtonIsVisible = value;
                OnPropertyChanged();
            }
        }

    }
}
