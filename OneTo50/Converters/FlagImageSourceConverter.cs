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
using System.Windows.Media.Imaging;

namespace OneTo50.Converters
{
    public class FlagImageSourceConverter : System.Windows.Data.IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string code ="";
            if(value == null)
                code ="cn";
            code = value.ToString().ToLower();
            string sURL = string.Format( "../Images/Nations/{0}.png",code);
            Uri imgURI = new Uri(sURL, UriKind.Relative);
            return new System.Windows.Media.Imaging.BitmapImage(imgURI);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
