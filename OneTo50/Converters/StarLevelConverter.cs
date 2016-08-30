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

namespace OneTo50.Converters
{
    public class StarLevelConverter : System.Windows.Data.IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            decimal val = System.Convert.ToDecimal(value);
            if (val <= 10)
                return 5;
            else if (val <= 30)
                return 4;
            else if (val <= 45)
                return 3;
            else if (val <= 60)
                return 2;
            else if (val <= 80)
                return 1;
            else
                return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
