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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace OneTo50.UserControls
{
    public partial class TimerDisplay : UserControl
    {
        public TimerDisplay()
        {
            InitializeComponent();
        }

        public TimeSpan RTime
        {
            set
            {
                //string timeString = string.Format("{0}.{1}", ((int)value.TotalSeconds).ToString().PadLeft(3, '0'), (value.Milliseconds / 10).ToString().PadLeft(2, '0'));
                tb1.Text = ((int)value.TotalSeconds).ToString().PadLeft(3, '0');
                tb2.Text = (value.Milliseconds / 10).ToString().PadLeft(2, '0');
            }
        }

        public string GetTimeText()
        {
            return string.Format("{0}.{1}", tb1.Text, tb2.Text);
        }
    }
}