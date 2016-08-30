﻿using System;
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
    public partial class NextTip : UserControl
    {
        public NextTip()
        {
            InitializeComponent();
        }

        public void PlayNext()
        {
            int val = int.Parse(tbCurrent.Text);
            if (val < 50)
            {
                val++;
                tbCurrent.Text = val.ToString();
            }
        }

    }
}
