using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using OneTo50.Utility;
using OneTo50.DataModals;

namespace OneTo50.UserControls
{
    public partial class FinalScore : UserControl
    {
        public event EventHandler OnClosed;

        public FinalScore()
        {
            InitializeComponent();
        }

        private void UpdateSavedUserName()
        {
            string userName = IsolatedStorageManager.GetStringValue(IsolatedStorageKeys.SavedUserName);
            if (string.IsNullOrEmpty(userName))
                userName = "User Name";
            tbUserName.Text = userName;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                // save to local database
                RecordModel rm = new RecordModel();
                rm.Score = decimal.Parse(tbScore.Text);
                rm.UserName = string.IsNullOrWhiteSpace(tbUserName.Text) ? "User Name" : tbUserName.Text.Trim();
                rm.CreatedTime = DateTime.Now;
                RecordModel.Insert(rm);
                // save username to local
                IsolatedStorageManager.Save(IsolatedStorageKeys.SavedUserName, rm.UserName);
                // save to webserver
                if (Microsoft.Phone.Net.NetworkInformation.DeviceNetworkInformation.IsNetworkAvailable && rm.Score <60)
                {
                    OneTo50ServiceReference.OneTo50SoapClient client = new OneTo50ServiceReference.OneTo50SoapClient();
                    client.UpdateScoreAsync(rm.UserName, Convert.ToDouble(rm.Score));
                }
                PopupManager.ClosePopup(typeof(FinalScore).ToString(), false);
                if (OnClosed != null)
                    OnClosed(this, null);
            }
            catch { }
        }

        public string Score
        {
            get
            {
                return tbScore.Text;
            }
            set
            {
                UpdateSavedUserName();
                tbScore.Text = value;
            }
        }

    }
}
