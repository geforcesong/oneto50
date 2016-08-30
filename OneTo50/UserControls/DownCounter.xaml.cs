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

namespace OneTo50.UserControls
{
    public partial class DownCounter : UserControl
    {
        public event EventHandler OnReadCompleted;

        public DownCounter()
        {
            InitializeComponent();
        }

        public void Start()
        {
            sbCounterDown.Completed += (s, e) =>
                                           {
                                               if (OnReadCompleted != null)
                                                   OnReadCompleted(this, null);
                                           };
            imgCounter1.Opacity = 0;
            imgCounter2.Opacity = 0;
            imgCounter3.Opacity = 0;
            imgCounterStart.Opacity = 0;
            imgCounter1.Visibility = Visibility.Visible;
            imgCounter2.Visibility = Visibility.Visible;
            imgCounter3.Visibility = Visibility.Visible;
            imgCounterStart.Visibility = Visibility.Visible;
            sbCounterDown.Begin();
        }
    }
}
