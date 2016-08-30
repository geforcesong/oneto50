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
using System.Collections.ObjectModel;
using OneTo50.DataModals;
using System.Linq;
namespace OneTo50.ViewModals
{
    public class RecordViewModel
    {
        ObservableCollection<RecordModel> _items = new ObservableCollection<RecordModel>();
        public ObservableCollection<RecordModel> Items
        {
            get { return _items; }
            set
            {
                _items = value;
            }
        }

        public void Update()
        {
            Items.Clear();
            var query = App.RecordDatabaseContext.Records.OrderBy(t => t.Score).ToList();
            if (query != null && query.Count > 0)
            {
                for (int i = 0; i < query.Count; i++)
                {
                    query[i].SortOrder = i + 1;
                    Items.Add(query[i]);
                }
            }
        }
    }
}
