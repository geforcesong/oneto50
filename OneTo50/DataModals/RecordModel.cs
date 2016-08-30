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
using System.Data.Linq.Mapping;

namespace OneTo50.DataModals
{
    [Table]
    public class RecordModel
    {
        [Column(IsPrimaryKey = true, DbType = "INT NOT NULL Identity", CanBeNull = false, IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]
        public int ID { get; set; }
        public string CountryCode { get; set; }
        [Column]
        public decimal Score { get; set; }
        [Column]
        public string UserName { get; set; }
        [Column]
        public DateTime CreatedTime { get; set; }
        public string IPAddress { get; set; }
        public int SortOrder { get; set; }

        public static void Insert(RecordModel rm)
        {
            App.RecordDatabaseContext.Records.InsertOnSubmit(rm);
            App.RecordDatabaseContext.SubmitChanges();
        }
    }
}
