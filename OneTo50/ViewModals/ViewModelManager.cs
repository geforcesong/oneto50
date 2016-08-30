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

namespace OneTo50.ViewModals
{
    public class ViewModelManager
    {
        public static RecordViewModel RecordViewModel
        {
            get
            {
                return App.Current.Resources["RecordViewModel"] as RecordViewModel;
            }
        }

        public static RecordViewModel WorldRecordsViewModel
        {
            get
            {
                return App.Current.Resources["WorldRecordsViewModel"] as RecordViewModel;
            }
        }

        public static RecordViewModel TodayWorldRecordsViewModel
        {
            get
            {
                return App.Current.Resources["TodayWorldRecordsViewModel"] as RecordViewModel;
            }
        }

        public static CountryScoreViewModel CountryScoreViewModel
        {
            get
            {
                return App.Current.Resources["CountryScoreViewModel"] as CountryScoreViewModel;
            }
        }
    }
}
