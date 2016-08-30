using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using OneTo50.DataModals;

namespace OneTo50.Utility
{
    public class SmartMadADManager
    {
        public static void AddAD(string positionID, VerticalAlignment va, Grid parent)
        {
            //SystemSetting ss = App.Current.Resources["SystemSetting"] as SystemSetting;
            SmartMad.Ads.WindowsPhone7.WPF.AdView adview = new SmartMad.Ads.WindowsPhone7.WPF.AdView();
            adview.VerticalAlignment = va;
            adview.AdPositionID = positionID;
            adview.AdInterval = 30;
            adview.AdAnimation = SmartMad.Ads.WindowsPhone7.WPF.AdAnimationType.AdAnimationFlipFromLeft;
            adview.IsDebugMode = false;
            parent.Children.Add(adview);
        }
    }
}
