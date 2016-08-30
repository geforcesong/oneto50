using Microsoft.Devices;
using Microsoft.Xna.Framework.Audio;
using OneTo50.DataModals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Resources;

namespace OneTo50.Utility
{
    public class SoundManager
    {
        public static void PlaySound(GameSounds gs)
        {
            SystemSetting ss = App.Current.Resources["SystemSetting"] as SystemSetting;
            if (gs == GameSounds.Shot && ss.IsVibrationEnabled) 
            {
                VibrateController.Default.Start(TimeSpan.FromMilliseconds(100));
            }

            if (ss.IsSoundEnabled)
            {
                StreamResourceInfo streamInfo = Application.GetResourceStream(
                                    new Uri(string.Format("/OneTo50;component/sounds/{0}.wav", gs.ToString()),
                                    UriKind.Relative));
                SoundEffect se = SoundEffect.FromStream(streamInfo.Stream);
                SoundEffectInstance soundInstance = se.CreateInstance();
                soundInstance.Play();
            }
        }
    }
}
