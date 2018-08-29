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
            //Clear Panel For Displaying
            UCPanel.Children.Clear();

            //Get Playlist
            Playlist pl = playlists[(int)((Button)sender).Tag]; 
            String playlistId = (String)pl.Id; //Playlist id for getting videos of playlist

            //Display Playlist
            PlaylistHeaderDisplayUC plUC = new PlaylistHeaderDisplayUC();
            plUC.playlistThumbnail.Source = new BitmapImage(new Uri(pl.thumbnail, UriKind.Absolute));
            plUC.playlistTitle.Text = pl.title;
            UCPanel.Children.Add(plUC);

            Video[] videos = YoutubeAPI.GetPlaylistVideos(playlistId); //Retrieve Videos
            //Video[] videos = YoutubeAPI.GetPlaylistVideos("PL8DN6BZgVqw7F0ovJKx_AXAGyKnG6suW4"); //Testing Purposes

            ScrollViewer sv = new ScrollViewer();
            sv.CanContentScroll = true;

            StackPanel sp = new StackPanel();
            sv.Content = sp;
            foreach (Video vid in videos)
            {
                //Display Videos In User Control PlaylistVideoDisplayUC
                
                PlaylistVideoDisplayUC plvUC = new PlaylistVideoDisplayUC();
                plvUC.title.Text = vid.Title;
                plvUC.channel.Text = vid.ChannelTitle;
                plvUC.thumbnail.Source = new BitmapImage(new Uri(vid.Thumbnail, UriKind.Absolute));
                plvUC.description.Text = vid.Description;

                sp.Children.Add(plvUC);      
            }
            UCPanel.Children.Add(sv); //Add To Panel
        }

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key != System.Windows.Input.Key.Enter) return;

            // your event handler here
            e.Handled = true;
            MessageBox.Show("Enter pressed");
            Video[] searchvid = Searches.SearchVideo(searchField.Text);
            Console.WriteLine("Search Video ID: " + searchvid[0].Id);
        }
    }
}
