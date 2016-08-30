using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Animation;

namespace OneTo50.Utility
{
    public class PopupManager
    {
        private static Dictionary<string, Popup> _allPopups = new Dictionary<string, Popup>();

        public static void RegisterPopup(string poupName, System.Windows.Controls.UserControl uc)
        {
            Popup p = new Popup();
            p.Child = uc;
            p.VerticalOffset = 30;
            p.HorizontalOffset = 400;
            p.IsOpen = false;
            _allPopups.Add(poupName, p);
        }

        public static bool HasOpenedPopup
        {
            get
            {
                foreach (var pop in _allPopups)
                {
                    bool? flag = IsOpen(pop.Key);
                    if (flag.HasValue && flag == true)
                        return true;
                }
                return false;
            }
        }

        public static void ShowPopup(string popupName)
        {
            if (!_allPopups.ContainsKey(popupName))
                return;
            Popup p = _allPopups[popupName];
            if (p.IsOpen)
            {
                return;
            }
            // close all existing popups
            CloseAllPopups();
            p.HorizontalOffset = 480;
            p.IsOpen = true;

            ////
            DoubleAnimation myDoubleAnimation1 = new DoubleAnimation();
            Duration duration = new Duration(TimeSpan.FromMilliseconds(300));
            myDoubleAnimation1.Duration = duration;
            Storyboard.SetTarget(myDoubleAnimation1, p);
            Storyboard.SetTargetProperty(myDoubleAnimation1, new PropertyPath(Popup.HorizontalOffsetProperty));
            var ease = new QuadraticEase { EasingMode = EasingMode.EaseIn };
            //myDoubleAnimation1.From = 480;
            myDoubleAnimation1.To = 0;
            myDoubleAnimation1.EasingFunction = ease;
            Storyboard sb = new Storyboard();
            sb.Children.Add(myDoubleAnimation1);
            sb.Begin();
        }

        public static void ClosePopup(string popupName, bool needTransition=true)
        {
            if (!_allPopups.ContainsKey(popupName))
                return;
            Popup p = _allPopups[popupName];
            if (p.IsOpen)
            {
                if (needTransition)
                {
                    DoubleAnimation myDoubleAnimation1 = new DoubleAnimation();
                    Duration duration = new Duration(TimeSpan.FromMilliseconds(300));
                    myDoubleAnimation1.Duration = duration;
                    Storyboard.SetTarget(myDoubleAnimation1, p);
                    Storyboard.SetTargetProperty(myDoubleAnimation1, new PropertyPath(Popup.HorizontalOffsetProperty));
                    var ease = new QuadraticEase { EasingMode = EasingMode.EaseIn };
                    //myDoubleAnimation1.From = 480;
                    myDoubleAnimation1.To = -500;
                    myDoubleAnimation1.EasingFunction = ease;
                    Storyboard sb = new Storyboard();
                    sb.Children.Add(myDoubleAnimation1);
                    sb.Completed += (o, e) =>
                    {
                        p.IsOpen = false;
                    };
                    sb.Begin();
                }
                else
                {
                    p.IsOpen = false;
                }
            }
        }

        public static void CloseAllPopups()
        {
            foreach (var pop in _allPopups)
            {
                ClosePopup(pop.Key);
            }
        }

        

        public static Popup GetPopup(string popupName)
        {
            if (!_allPopups.ContainsKey(popupName))
                return null;
            return _allPopups[popupName];
        }

        public static void Clear()
        {
            _allPopups.Clear();
            _allPopups = new Dictionary<string, Popup>();
        }

        public static bool? IsOpen(string popupName)
        {
            if (!_allPopups.ContainsKey(popupName))
                return null;
            else
                return _allPopups[popupName].IsOpen;
        }

        public static void TogglePopup(string popupName)
        {
            bool? flag = IsOpen(popupName);
            if (!flag.HasValue)
                return;
            if (flag == false)
                ShowPopup(popupName);
            else
                ClosePopup(popupName);
        }
    }
}
