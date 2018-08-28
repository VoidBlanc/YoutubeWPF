using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;

using Google.Apis.Discovery.v1.Data;
using Google.Apis.Services;
using Google.Apis.Tasks.v1;
using Google.Apis.Upload;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;


namespace YoutubeWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            Playlist[] playlists = YoutubeAPI.GetPlaylists();
            //Console.WriteLine(str[0]);
            /*while (playlists.Length > 0)
            {
                Playlist pl = playlists.Dequeue();
                //Image playlistThumbnail = new Image();
                //playlistThumbnail.Source = new BitmapImage(new Uri(pl.thumbnail,UriKind.Absolute));
                TextBlock plTitle = new TextBlock();
                plTitle.Text = pl.title;
                playlistViewer.Children.Add(plTitle);
            }    */

            foreach (Playlist pl in playlists) {
                //Image playlistThumbnail = new Image();
                //playlistThumbnail.Source = new BitmapImage(new Uri(pl.thumbnail,UriKind.Absolute));
                TextBlock plTitle = new TextBlock();
                plTitle.Text = pl.title;
                playlistViewer.Children.Add(plTitle);
            }
        }

        public static void displayInfo(object sender, MouseButtonEventArgs e)
        {
            
        }


    }
}
