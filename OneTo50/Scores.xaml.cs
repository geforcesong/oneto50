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
using Microsoft.Phone.Controls;
using OneTo50.ViewModals;
using System.IO;
using System.Text;
using System.Runtime.Serialization.Json;
using OneTo50.DataModals;

namespace OneTo50
{
    public partial class Scores : PhoneApplicationPage
    {
        

        bool _isLoadedFirstTime = true;

        public Scores()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (_isLoadedFirstTime)
            {
                OneTo50.Utility.SmartMadADManager.AddAD("90030382", System.Windows.VerticalAlignment.Bottom, LayoutRoot);
                _isLoadedFirstTime = false;
                RecordViewModel vm = ViewModelManager.RecordViewModel;
                if (vm != null)
                    vm.Update();
            }
        }

        
    }
}