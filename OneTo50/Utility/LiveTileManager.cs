using System;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Phone.Shell;
using OneTo50.UserControls;

namespace OneTo50.Utility
{
    public class LiveTileManager
    {
        const string ImageFolder = @"\Shared\ShellContent";
        const string BackTileImage = "shareImage.jpg";
        const string BackBackTileImage = "BackBackTileImage.jpg";

        public static void UpdateLive(string worldRecord, string country)
        {
            CreateBackTileImage();
            CreateBackBackTileImage(worldRecord, country);

            ShellTile find = ShellTile.ActiveTiles.First();
            if(find != null)
            {
                StandardTileData data = new StandardTileData();
                data.Title = "1 TO 50";
                string imgPath = string.Format(@"isostore:/Shared/ShellContent/{0}", BackTileImage);
                string imgPathBack = string.Format(@"isostore:/Shared/ShellContent/{0}", BackBackTileImage);
                data.BackgroundImage = new Uri(imgPath, UriKind.Absolute);
                data.BackBackgroundImage = new Uri(imgPathBack, UriKind.Absolute);
                find.Update(data);
            }
        }

        private static void CreateBackTileImage()
        {
            using (IsolatedStorageFile myIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (!myIsolatedStorage.DirectoryExists(ImageFolder))
                {
                    myIsolatedStorage.CreateDirectory(ImageFolder);
                }

                string filePath = System.IO.Path.Combine(ImageFolder, BackTileImage);
                if (myIsolatedStorage.FileExists(filePath))
                {
                    myIsolatedStorage.DeleteFile(filePath);
                }

                IsolatedStorageFileStream fileStream = myIsolatedStorage.CreateFile(filePath);
                BitmapImage bitmap = new BitmapImage();
                bitmap.CreateOptions = BitmapCreateOptions.None;
                var imageUri = new Uri("/OneTo50;component/Images/LiveTileImages/ApplicationIcon173.png", UriKind.Relative);
                System.Windows.Resources.StreamResourceInfo s = Application.GetResourceStream(imageUri);
                bitmap.SetSource(s.Stream);
                WriteableBitmap wb =new WriteableBitmap(bitmap);
                wb.Invalidate();
                Extensions.SaveJpeg(wb, fileStream, 173, 173, 0, 100);
                fileStream.Close();
            }
        }

        private static void CreateBackBackTileImage(string worldRecord, string country)
        {
            using (IsolatedStorageFile myIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (!myIsolatedStorage.DirectoryExists(ImageFolder))
                {
                    myIsolatedStorage.CreateDirectory(ImageFolder);
                }

                string filePath = System.IO.Path.Combine(ImageFolder, BackBackTileImage);
                if (myIsolatedStorage.FileExists(filePath))
                {
                    myIsolatedStorage.DeleteFile(filePath);
                }

                IsolatedStorageFileStream fileStream = myIsolatedStorage.CreateFile(filePath);
                BitmapImage bitmap = new BitmapImage();
                bitmap.CreateOptions = BitmapCreateOptions.None;
                var imageUri = new Uri("/OneTo50;component/Images/LiveTileImages/TileBackground.png", UriKind.Relative);
                System.Windows.Resources.StreamResourceInfo s = Application.GetResourceStream(imageUri);
                bitmap.SetSource(s.Stream);
                WriteableBitmap wb = new WriteableBitmap(bitmap);
                wb.Render(CreateBackBackTileContent(worldRecord, country), null);
                wb.Invalidate();
                Extensions.SaveJpeg(wb, fileStream, 173, 173, 0, 100);
                fileStream.Close();
            }
        }

        public static FrameworkElement CreateBackBackTileContent(string wr, string country)
        {
            var canvas = new Grid();
            BackgroundTileGrid bt = new BackgroundTileGrid();
            bt.Width = 173;
            bt.Height=173;
            bt.WorldRecord = wr;
            bt.RecordCountry = country;
            canvas.Children.Add(bt);
            return canvas;
        }
    }
}
