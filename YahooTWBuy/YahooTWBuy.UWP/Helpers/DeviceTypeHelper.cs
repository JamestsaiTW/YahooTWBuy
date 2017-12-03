using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System.Profile;
using Windows.UI.ViewManagement;

namespace YahooTWBuy.UWP.Helpers
{
    public static class DeviceTypeHelper
    {
        public static DeviceFromFactorType GetDeviceFromFactorType()
        {
            switch (AnalyticsInfo.VersionInfo.DeviceFamily)
            {
                case "Windows.Mobile":
                    return DeviceFromFactorType.Phone;
                case "Windows.Desktop":
                    return UIViewSettings.GetForCurrentView().UserInteractionMode == UserInteractionMode.Mouse
                        ? DeviceFromFactorType.Desktop
                        : DeviceFromFactorType.Tablet;
                case "Windows.Universal":
                    return DeviceFromFactorType.IoT;
                case "Windows.Team":
                    return DeviceFromFactorType.SurfaceHub;
                default:
                    return DeviceFromFactorType.Other;
            }
        }
    }

    public enum DeviceFromFactorType
    {
        Phone,
        Desktop,
        Tablet,
        IoT,
        SurfaceHub,
        Other
    }
}
