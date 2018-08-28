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
        private Playlist[] playlists;
        public MainWindow()
        {
            InitializeComponent();
            this.playlists = YoutubeAPI.GetPlaylists();        
            for (int i = 0; i < playlists.Length;i++) {
                Playlist pl = playlists[i];
                Button plTitle = new Button();
                plTitle.Content = pl.title;
                plTitle.MinWidth = 300.00;
                plTitle.MaxWidth = 300.00;
                plTitle.Tag = i;
                plTitle.Click += displayInfo;
                playlistViewer.Children.Add(plTitle);
            }
        }

        private void displayInfo(object sender, RoutedEventArgs e)
        {
            Playlist pl = playlists[(int)((Button)sender).Tag];
            String playlistId = (String)pl.Id;

            Video[] videos = YoutubeAPI.GetPlaylistVideos(playlistId);

            Image playlistThumbnail = new Image();
            playlistThumbnail.Source = new BitmapImage(new Uri(pl.thumbnail, UriKind.Absolute));
            UCPanel.Children.Clear();
            foreach (Video vid in videos)
            {
                PlaylistHeaderDisplayUC plUC = new PlaylistHeaderDisplayUC();
                plUC.playlistThumbnail.Source = new BitmapImage(new Uri(vid.Thumbnail, UriKind.Absolute));
                plUC.playlistTitle.Text = vid.Title;
                UCPanel.Children.Add(plUC);
            }
        }

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key != System.Windows.Input.Key.Enter) return;

            // your event handler here
            e.Handled = true;
            MessageBox.Show("Enter pressed");
            Video[] searchvid = Search.SearchVideo(searchField.Text);
            Console.WriteLine("Search Video ID: " + searchvid[0].Id);
        }
    }
}
