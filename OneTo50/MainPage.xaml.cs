using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using OneTo50.DataModals;
using Microsoft.Phone.Tasks;

namespace OneTo50
{
    public partial class MainPage : PhoneApplicationPage
    {
        private bool loaded = false;
        // 构造函数
        public MainPage()
        {
            InitializeComponent();

            SystemSetting ss = App.Current.Resources["SystemSetting"] as SystemSetting;
            if (ss.IsSoundEnabled)
                btnSound.Content = "Sound Off";
            else
                btnSound.Content = "Sound On";

            if (ss.IsVibrationEnabled)
                btnViberate.Content = "Vibration Off";
            else
                btnViberate.Content = "Vibration On";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/GamePage.xaml", UriKind.Relative));
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (!loaded)
            {
                loaded = true;
            }
            OneTo50.Utility.SmartMadADManager.AddAD("90030382", System.Windows.VerticalAlignment.Bottom, gdADV);

            //if (ss.IsInTrialModal)
            //    btnBuyit.Visibility = System.Windows.Visibility.Visible;
            //else
            //    btnBuyit.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void btnScores_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Scores.xaml", UriKind.Relative));
        }

        private void btnRate_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Phone.Tasks.MarketplaceReviewTask mt = new Microsoft.Phone.Tasks.MarketplaceReviewTask();
            mt.Show();
        }

        private void btnAbout_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/About.xaml", UriKind.Relative));
        }

        private void btnSound_Click(object sender, RoutedEventArgs e)
        {
            SystemSetting ss = App.Current.Resources["SystemSetting"] as SystemSetting;
            if (btnSound.Content.ToString().ToLower() == "sound off")
            {
                btnSound.Content = "sound on";
                ss.IsSoundEnabled = false;
            }
            else
            {
                btnSound.Content = "sound off";
                ss.IsSoundEnabled = true;
            }
        }

        private void btnBuyit_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Phone.Tasks.MarketplaceDetailTask marketPlaceDetailTask = new Microsoft.Phone.Tasks.MarketplaceDetailTask();
            marketPlaceDetailTask.Show();
        }

        private void btnVibrate_Click(object sender, RoutedEventArgs e)
        {
            SystemSetting ss = App.Current.Resources["SystemSetting"] as SystemSetting;
            if (btnViberate.Content.ToString().ToLower() == "vibration off")
            {
                btnViberate.Content = "Vibration on";
                ss.IsVibrationEnabled = false;
            }
            else
            {
                btnViberate.Content = "Vibration off";
                ss.IsVibrationEnabled = true;
            }
        }

        private void btnContact_Click(object sender, RoutedEventArgs e)
        {
            EmailComposeTask ect = new EmailComposeTask();
            ect.To = "georgeguo@outlook.com";
            ect.Subject = "1 TO 50 USER FEEDBACK FOR v5.0";
            ect.Body = "if you have new reqirements, please let me know. I will try my best to meet your requirements. Thank you in advance.";
            ect.Show();
        }
    }
}