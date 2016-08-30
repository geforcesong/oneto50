using OneTo50.DataModals;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace OneTo50.ViewModals
{
    public class CountryScoreViewModel
    {
        ObservableCollection<CountryScore> _items = new ObservableCollection<CountryScore>();
        public ObservableCollection<CountryScore> Items
        {
            get { return _items; }
            set
            {
                _items = value;
            }
        }
    }
}
