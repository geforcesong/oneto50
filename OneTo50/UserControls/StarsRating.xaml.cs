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
    public partial class StarsRating : UserControl
    {
        public StarsRating()
        {
            InitializeComponent();
        }

        public StackPanel Container
        {
            get { return spContainer; }
        }

        public readonly static DependencyProperty StarsProperty = DependencyProperty.Register("Stars",
        typeof(int),
        typeof(StarsRating), new PropertyMetadata(new PropertyChangedCallback(StarsPropertyChangedCallback)));

        static void StarsPropertyChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            StarsRating ptb = (sender as StarsRating);
            if (ptb != null)
            {
                ptb.Container.Children.Clear();
                int count = (int)e.NewValue;
                for (int i = 0; i < count; i++)
                {
                    string sURL = "../Images/other/star.png";
                    Uri imgURI = new Uri(sURL, UriKind.Relative);
                    Image im = new Image();
                    im.Source = new System.Windows.Media.Imaging.BitmapImage(imgURI);
                    im.Width = 16;
                    im.Height = 16;
                    ptb.Container.Children.Add(im);
                }
            }
        }


        public int Stars
        {
            get { return (int)GetValue(StarsProperty); }
            set
            {
                SetValue(StarsProperty, value);
            }
        }
    }
}
