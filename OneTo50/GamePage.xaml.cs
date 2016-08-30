using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Microsoft.Phone.Controls;
using OneTo50.UserControls;
using OneTo50.Utility;

namespace OneTo50
{
    public partial class GamePage : PhoneApplicationPage
    {
        private bool loaded = false;
        List<int> smallNumList = new List<int>();
        List<int> bigNumList = new List<int>();
        static Random _random = new Random(DateTime.Now.Millisecond);
        private int _currentClickNumber = 0;
        DateTime _startTime;
        DateTime _lastFlipTime;
        DispatcherTimer _timer = new DispatcherTimer();
        DispatcherTimer _timerForTip = new DispatcherTimer();

        public GamePage()
        {
            InitializeComponent();
            _timer.Tick += _timer_Tick;
            _timerForTip.Tick += new EventHandler(_timerForTip_Tick);
        }

        void _timerForTip_Tick(object sender, EventArgs e)
        {
            TimeSpan elapse = DateTime.Now - _lastFlipTime;
            if(elapse.TotalMilliseconds >=3000)
            {
                _timerForTip.Stop();
                var flip = Search(_currentClickNumber + 1);
                if(flip !=null)
                    flip.ShowTipAnimation();
            }
        }

        void RegisterAllPopup()
        {
            PopupManager.Clear();
            //
            FinalScore f = new FinalScore();
            f.Height = LayoutRoot.ActualHeight;
            f.Width = LayoutRoot.ActualWidth;
            f.OnClosed += (o, e) =>
            {
                if (NavigationService.CanGoBack)
                    NavigationService.GoBack();
                else
                    NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
            };
            PopupManager.RegisterPopup(typeof(FinalScore).ToString(), f);
        }


        void _timer_Tick(object sender, object e)
        {
            TimeSpan elapse = DateTime.Now - _startTime;
            display.RTime = elapse;
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (!loaded)
            {
                SmartMadADManager.AddAD("90030381", System.Windows.VerticalAlignment.Bottom,gdADV);
                RegisterAllPopup();
                LoadGame();
            }
            loaded = true;
        }

        private void LoadGame()
        {
            smallNumList.Clear();
            bigNumList.Clear();
            for (int i = 0; i < 25; i++)
            {
                smallNumList.Add(i + 1);
                bigNumList.Add((i + 1) + 25);
            }

            for(int i=0;i<25;i++)
            {
                int smallIndex = _random.Next(0, smallNumList.Count);
                int bigIndex = _random.Next(0, bigNumList.Count);
                Flipper f = new Flipper(smallNumList[smallIndex], bigNumList[bigIndex]);
                f.OnPlayEventHandler += new EventHandler<OnPlayEventHandlerArg>(f_OnPlayEventHandler);
                f.Height = gdGameContainer.ActualWidth / 5;
                int rowIndex = i/5;
                if (rowIndex >= 5)
                    rowIndex = 4;
                int columnIndex = i%5;
                f.SetValue(Grid.RowProperty, rowIndex);
                f.SetValue(Grid.ColumnProperty, columnIndex++);
                gdGameContainer.Children.Add(f);
                smallNumList.RemoveAt(smallIndex);
                bigNumList.RemoveAt(bigIndex);
            }

            downCounter.OnReadCompleted += new EventHandler(downCounter_OnReadCompleted);
            downCounter.Start();
        }

        private Flipper Search(int num)
        {
            foreach (var flip in gdGameContainer.Children)
            {
                Flipper f = flip as Flipper;
                if (f != null && (f.SmallValue == num || f.BigValue == num))
                    return f;
            }
            return null;
        }

        void f_OnPlayEventHandler(object sender, OnPlayEventHandlerArg e)
        {
            if (e.CurrentValue == _currentClickNumber + 1)
            {
                SoundManager.PlaySound(GameSounds.Shot);
                Flipper f = sender as Flipper;
                if (f != null)
                {
                    f.PlayNext();
                    nextTip.PlayNext();
                    _lastFlipTime = DateTime.Now;
                    _timerForTip.Start();
                }
                _currentClickNumber++;

                if (_currentClickNumber == 50)
                {
                    // game over
                    _timer.Stop();
                    _timerForTip.Stop();
                    downCounter.Visibility = Visibility.Visible;
                    string popupName = typeof(FinalScore).ToString();
                    FinalScore fs = PopupManager.GetPopup(popupName).Child as FinalScore;
                    if (fs != null)
                        fs.Score = display.GetTimeText();
                    PopupManager.ShowPopup(popupName);
                }
            }
            else
            {
                SoundManager.PlaySound(GameSounds.WrongTap);
            }
        }

        void downCounter_OnReadCompleted(object sender, EventArgs e)
        {
            _startTime = DateTime.Now;
            _lastFlipTime = DateTime.Now;
            _timer.Start();
            _timerForTip.Start();
            downCounter.Visibility = Visibility.Collapsed;
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            base.OnBackKeyPress(e);
            if (PopupManager.HasOpenedPopup)
            {
                PopupManager.CloseAllPopups();
                e.Cancel = true;
            }
        }
    }
}