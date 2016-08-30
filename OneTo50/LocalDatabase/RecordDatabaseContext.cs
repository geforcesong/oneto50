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
using OneTo50.DataModals;
using System.Data.Linq;

namespace OneTo50.LocalDatabase
{
    public class RecordDatabaseContext:DataContext
    {
        public static readonly string DBConnectionString = "Data Source=isostore:/Records.sdf";

        public RecordDatabaseContext(string connectionString) : base(connectionString) { }

        public Table<RecordModel> Records;
    }
}
