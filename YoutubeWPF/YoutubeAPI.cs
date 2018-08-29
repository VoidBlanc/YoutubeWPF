using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
namespace YoutubeWPF
{
    class YoutubeAPI
    {
        private static YouTubeService ytService = Auth();

        public static YouTubeService Auth()
        {
            UserCredential credential;
            using (var stream = new FileStream(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + " \\client_secret.json", FileMode.Open, FileAccess.Read))
            {
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                     GoogleClientSecrets.Load(stream).Secrets,
                     new[]
                     {
                        YouTubeService.Scope.YoutubeReadonly,
                     },
                     "user",
                     CancellationToken.None,
                     new FileDataStore("YoutubeAPI")).Result;
            }

            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "YTDesk"
            });

            return youtubeService;
        }
        //Get User's List Of Private and Public Playlists
        internal static Playlist[] GetPlaylists()
        {
            var request = ytService.Playlists.List("contentDetails,snippet"); //Type of info to request for
            request.Mine = true; //Setting Playlists to come from user
            request.MaxResults = 50;

            var PlaylistResponse = request.Execute();
            //PlaylistResponse.NextPageToken

            Playlist[] playlists = new Playlist[PlaylistResponse.Items.Count]; //To Hold Playlist Info

            int i = 0;
            foreach (var PlaylistItem in PlaylistResponse.Items) {
                //Get Playlist Info
                Playlist pl = new Playlist();
                pl.thumbnail = PlaylistItem.Snippet.Thumbnails.Medium.Url;
                pl.Id = PlaylistItem.Id;
                pl.amtOfVideos = unchecked((int)PlaylistItem.ContentDetails.ItemCount);
                pl.description = PlaylistItem.Snippet.Description;
                pl.title = PlaylistItem.Snippet.Title;
                pl.channelTitle = PlaylistItem.Snippet.ChannelTitle;
                playlists[i] = pl;
                i++;
                //playlists[i] = Playlist.ContentDetails;
            }
            return playlists;
        }

        //Get Videos From Playlist
        internal static Video[] GetPlaylistVideos(String playlistId)
        {
            //ytService.Videos.List("statistics,contentDetails,snippet");
            var request = ytService.PlaylistItems.List("contentDetails,snippet");
            request.PlaylistId = playlistId;
            request.MaxResults = 50;

            var VideosResponse = request.Execute();
            Video[] videos = new Video[VideosResponse.Items.Count];

            int i = 0;
            foreach (var videoItem in VideosResponse.Items)
            {
                Video vid = new Video();
                vid.Id = videoItem.Id;
                vid.Title = videoItem.Snippet.Title;
                vid.Description = videoItem.Snippet.Description;

                if (videoItem.Snippet.Thumbnails == null)
                {
                    //Video is private
                    vid.Thumbnail = "http://s.ytimg.com/yts/img/no_thumbnail-vfl4t3-4R.jpg";
                    vid.ChannelId = "";
                    vid.ChannelTitle = "";
                }
                else
                {
                    vid.ChannelTitle = videoItem.Snippet.ChannelTitle;
                    vid.ChannelId = videoItem.Snippet.ChannelId;
                    vid.Thumbnail = videoItem.Snippet.Thumbnails.Medium.Url.ToString();
                }
                videos[i] = vid;
                i++;
            }
            return videos;
        }

    }
}
