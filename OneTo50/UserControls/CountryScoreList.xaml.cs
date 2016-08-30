using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Runtime.Serialization.Json;
using System.IO;
using OneTo50.DataModals;
using System.Text;

namespace OneTo50.UserControls
{
    public partial class CountryScoreList : UserControl
    {
        bool _isLoadedFirstTime = true;

        public CountryScoreList()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded_1(object sender, RoutedEventArgs e)
        {
            if (_isLoadedFirstTime)
            {
                _isLoadedFirstTime = false;
                LoadData();
            }
        }

        void LoadData()
        {
            ppb.Visibility = System.Windows.Visibility.Visible;
            ViewModals.ViewModelManager.CountryScoreViewModel.Items.Clear();
            OneTo50ServiceReference.OneTo50SoapClient client = new OneTo50ServiceReference.OneTo50SoapClient();
            client.GetCountryScoreCompleted += client_GetCountryScoreCompleted;
            client.GetCountryScoreAsync();
        }

        void client_GetCountryScoreCompleted(object sender, OneTo50ServiceReference.GetCountryScoreCompletedEventArgs e)
        {
            ppb.Visibility = System.Windows.Visibility.Collapsed;
            if (e.Error == null && !string.IsNullOrEmpty(e.Result))
            {
                MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(e.Result));
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(List<CountryScore>));
                List<CountryScore> results = ser.ReadObject(ms) as List<CountryScore>;
                if (results != null)
                {
                    int order = 1;
                    foreach (var cs in results.Where(o => !string.IsNullOrEmpty(o.CountryCode)).OrderBy(o => o.Score))
                    {
                        cs.SortOrder = order++;
                        ViewModals.ViewModelManager.CountryScoreViewModel.Items.Add(cs);
                        if (cs.SortOrder == 1)
                        {
                            string scoreString = string.Format("{0:N2}", cs.Score);
                            OneTo50.Utility.LiveTileManager.UpdateLive(scoreString, cs.CountryCode == null ? "" : cs.CountryCode.ToUpper());
                        }
                    }
                }
                gdCountryScore.Visibility = System.Windows.Visibility.Visible;
                gdError.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                gdCountryScore.Visibility = System.Windows.Visibility.Collapsed;
                gdError.Visibility = System.Windows.Visibility.Visible;
            }
        }
    }
}
