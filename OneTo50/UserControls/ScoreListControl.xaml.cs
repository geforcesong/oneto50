using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using OneTo50.DataModals;
using OneTo50.ViewModals;
using OneTo50.Utility;
using System.Windows.Data;

namespace OneTo50.UserControls
{
    public partial class ScoreListControl : UserControl
    {
        private WorldRecordType _worldRecordType;
        private static object _syncObject = new object();
        private const int WorldScoreDataPageSize = 20;
        private bool _isLoadingWorldScore = false;
        private int _loadedWorldDataPageSize = 1;
        ScrollViewer _scrollViewer;
        bool _isLoadedFirstTime = true;

        public ScoreListControl()
        {
            InitializeComponent();
        }

        public WorldRecordType WorldRecordType
        {
            get { return _worldRecordType; }
            set
            {
                _worldRecordType = value;
                Binding binding = new Binding();
                binding.Path = new PropertyPath("Items");
                if (_worldRecordType == Utility.WorldRecordType.All)
                {
                    binding.Source = ViewModelManager.WorldRecordsViewModel;
                }
                else
                {
                    binding.Source = ViewModelManager.TodayWorldRecordsViewModel;
                }
                lbWorldScores.SetBinding(ListBox.ItemsSourceProperty, binding);
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (_isLoadedFirstTime)
            {
                _isLoadedFirstTime = false;
                if (_worldRecordType == Utility.WorldRecordType.All)
                    ViewModelManager.WorldRecordsViewModel.Items.Clear();
                else
                    ViewModelManager.TodayWorldRecordsViewModel.Items.Clear();

                LoadWorldScoreData(1);
            }
        }

        private void LoadWorldScoreData(int pageIndex)
        {
            if (_isLoadingWorldScore)
                return;
            // get world data
            lock (_syncObject)
            {
                _isLoadingWorldScore = true;
            }
            ppb.Visibility = System.Windows.Visibility.Visible;
            OneTo50ServiceReference.OneTo50SoapClient client = new OneTo50ServiceReference.OneTo50SoapClient();
            if (_worldRecordType == Utility.WorldRecordType.All)
            {
                client.GetRecordsByPageCompleted += client_GetRecordsByPageCompleted;
                client.GetRecordsByPageAsync(WorldScoreDataPageSize, pageIndex);
            }
            else
            {
                client.GetTodayRecordsByPageCompleted += new EventHandler<OneTo50ServiceReference.GetTodayRecordsByPageCompletedEventArgs>(client_GetTodayRecordsByPageCompleted);
                client.GetTodayRecordsByPageAsync(WorldScoreDataPageSize, pageIndex);
            }
        }

        void client_GetTodayRecordsByPageCompleted(object sender, OneTo50ServiceReference.GetTodayRecordsByPageCompletedEventArgs e)
        {
            if (e.Error == null && !string.IsNullOrEmpty(e.Result))
            {
                PopulateViewModel(e.Result);
            }
            else
            {
                if (_loadedWorldDataPageSize == 1)
                {
                    gdScoreList.Visibility = System.Windows.Visibility.Collapsed;
                    gdError.Visibility = System.Windows.Visibility.Visible;
                }
            }
            lock (_syncObject)
            {
                _isLoadingWorldScore = false;
                ppb.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        void PopulateViewModel(string resultFromWebservice)
        {
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(resultFromWebservice));
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(List<RecordModel>));
            List<RecordModel> results = ser.ReadObject(ms) as List<RecordModel>;
            ms.Close();
            if (results != null)
            {
                results = results.OrderBy(r => r.Score).ToList();
                for (int i = 0; i < results.Count; i++)
                {
                    results[i].SortOrder = (_loadedWorldDataPageSize - 1) * WorldScoreDataPageSize + i + 1;
                    if (_worldRecordType == Utility.WorldRecordType.All)
                        ViewModelManager.WorldRecordsViewModel.Items.Add(results[i]);
                    else
                        ViewModelManager.TodayWorldRecordsViewModel.Items.Add(results[i]);
                }
            }
            if (_loadedWorldDataPageSize >= 1)
            {
                InitAutoScrollListbox();
            }
            _loadedWorldDataPageSize++;
        }

        void client_GetRecordsByPageCompleted(object sender, OneTo50ServiceReference.GetRecordsByPageCompletedEventArgs e)
        {
            if (e.Error == null && !string.IsNullOrEmpty(e.Result))
            {
                PopulateViewModel(e.Result);
            }
            else
            {
                if (_loadedWorldDataPageSize == 1)
                {
                    gdScoreList.Visibility = System.Windows.Visibility.Collapsed;
                    gdError.Visibility = System.Windows.Visibility.Visible;
                }
            }
            lock (_syncObject)
            {
                _isLoadingWorldScore = false;
                ppb.Visibility = System.Windows.Visibility.Collapsed;
            }
        }


        void visualStateGroup_CurrentStateChanged(object sender, VisualStateChangedEventArgs e)
        {
            var visualState = e.NewState.Name;
            if (visualState == "NotScrolling")
            {
                var v1 = _scrollViewer.ExtentHeight - _scrollViewer.VerticalOffset;
                var v2 = _scrollViewer.ViewportHeight;

                if (v1 < v2 || Math.Abs(v1 - v2) <= 0.0001)
                {
                    // TODO: 在此处加载新数据
                    LoadWorldScoreData(_loadedWorldDataPageSize);
                }
            }
        }

        private void InitAutoScrollListbox()
        {
            _scrollViewer = OneTo50.Utility.TreeViewHelper.FindVisualChild<ScrollViewer>(lbWorldScores);
            if (_scrollViewer != null)
            {

                FrameworkElement element = VisualTreeHelper.GetChild(_scrollViewer, 0) as FrameworkElement;
                if (element != null)
                {
                    VisualStateGroup visualStateGroup = OneTo50.Utility.TreeViewHelper.FindVisualState(element, "ScrollStates");
                    if (_loadedWorldDataPageSize == 1)
                        visualStateGroup.CurrentStateChanged += new EventHandler<VisualStateChangedEventArgs>(visualStateGroup_CurrentStateChanged);
                    else
                    {
                        visualStateGroup.CurrentStateChanged -= new EventHandler<VisualStateChangedEventArgs>(visualStateGroup_CurrentStateChanged);
                        visualStateGroup.CurrentStateChanged += new EventHandler<VisualStateChangedEventArgs>(visualStateGroup_CurrentStateChanged);
                    }
                }
            }
        }
    }
}
