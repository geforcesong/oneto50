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
using Microsoft.Phone.Marketplace;
using OneTo50.Utility;

namespace OneTo50.DataModals
{
    public class SystemSetting
    {
        private bool _isInTrialModal;
        private bool _isSoundEnabled;
        private bool _isViberationEnabled;

        public SystemSetting()
        {
            //LicenseInformation li = new LicenseInformation();
            //_isInTrialModal = li.IsTrial();

            _isInTrialModal = false;

            // sound
            if (!IsolatedStorageManager.Contains(IsolatedStorageKeys.SoundEnabledKey))
                _isSoundEnabled = true;
            else
                _isSoundEnabled = IsolatedStorageManager.GetBooleanValue(IsolatedStorageKeys.SoundEnabledKey);

            // vibrate
            if (!IsolatedStorageManager.Contains(IsolatedStorageKeys.VibrationEnabledKey))
                _isViberationEnabled = true;
            else
                _isViberationEnabled = IsolatedStorageManager.GetBooleanValue(IsolatedStorageKeys.VibrationEnabledKey);
        }
        
        public bool IsInTrialModal
        {
            get
            {
                return _isInTrialModal;
            }
            set
            {
                _isInTrialModal = value;
            }
        }

        public bool IsSoundEnabled
        {
            get { return _isSoundEnabled; }
            set
            {
                _isSoundEnabled = value;
                IsolatedStorageManager.Save(IsolatedStorageKeys.SoundEnabledKey, value);
            }
        }

        public bool IsVibrationEnabled
        {
            get { return _isViberationEnabled; }
            set
            {
                _isViberationEnabled = value;
                IsolatedStorageManager.Save(IsolatedStorageKeys.VibrationEnabledKey, value);
            }
        }
    }
}
