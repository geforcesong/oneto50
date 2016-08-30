using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace OneTo50.UserControls
{
    public partial class BackgroundTileGrid : UserControl
    {
        public BackgroundTileGrid()
        {
            InitializeComponent();
        }

        public string WorldRecord
        {
            get { return tbWR.Text; }
            set
            {
                tbWR.Text = value;  
            }
        }

        public string RecordCountry
        {
            get { return tbCountry.Text.Replace("Country:", ""); }
            set
            {
                tbCountry.Text = string.Format("Country:{0}", value);
            }
        }
    }
}
