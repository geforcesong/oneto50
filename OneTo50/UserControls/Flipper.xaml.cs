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
    public partial class Flipper : UserControl
    {
        public event EventHandler<OnPlayEventHandlerArg> OnPlayEventHandler;
        public readonly int _smallValue;
        public readonly int _bigValue;

        public int SmallValue
        {
            get { return _smallValue; }
        }

        public int BigValue
        {
            get { return _bigValue; }
        }

        public Flipper(int smallInt, int bigInt)
        {
            InitializeComponent();
            _smallValue = smallInt;
            _bigValue = bigInt;
            tbSmall.Text = smallInt.ToString();
            tbBig.Text = bigInt.ToString();
        }

        private int _StatusCode;

        private void blockMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //_StatusCode = 3;
            //GotoState(_StatusCode);
            if (OnPlayEventHandler != null)
            {
                OnPlayEventHandlerArg arg = new OnPlayEventHandlerArg();
                if (_StatusCode == 1)
                    arg.CurrentValue = int.Parse(tbSmall.Text);
                else if (_StatusCode == 2)
                    arg.CurrentValue = int.Parse(tbBig.Text);
                else
                    arg.CurrentValue = - 1;
                OnPlayEventHandler(this, arg);
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            _StatusCode = 1;
            GotoState(_StatusCode);
        }

        public bool CanFlip
        {
            get { return _StatusCode >= 3; }
        }

        private void GotoState(int statusCode)
        {
            string status = string.Format("Status{0}", statusCode);
            VisualStateManager.GoToState(this, status, true);
        }

        public void PlayNext()
        {
            sbBackground1.Stop();
            sbBackground2.Stop();
            r1Start.Color = Color.FromArgb(255, 128, 183, 0);
            r1End.Color = Color.FromArgb(255, 109, 159, 0);
            r2Start.Color = Color.FromArgb(255, 128, 183, 0);
            r2End.Color = Color.FromArgb(255, 109, 159, 0);
            GotoState(++_StatusCode);
        }

        public void ShowTipAnimation()
        {
            sbBackground1.Begin();
            sbBackground2.Begin();
        }

    }

    public class OnPlayEventHandlerArg: EventArgs
    {
        public int CurrentValue { get; set; }
    }
}
