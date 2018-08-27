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
            using (var stream = new FileStream(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + " //client_secret.json", FileMode.Open, FileAccess.Read))
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
                ApplicationName = "YoutubeAPI"
            });

            return youtubeService;
        }
        //Get User's List Of Private and Public Playlists
        internal static string[][] GetPlaylists()
        {
            var request = ytService.Playlists.List("contentDetails,snippet"); //Type of info to request for
            request.Mine = true; //Setting Playlists to come from user
            var PlaylistResponse = request.Execute(); 

            string[][] playlists = new string[PlaylistResponse.Items.Count][]; //To Hold Playlist Info

            int i = 0;
            foreach (var Playlist in PlaylistResponse.Items) {
                //Get Playlist Info
                Console.Write("Playlist: " + Playlist.Snippet.Title.ToString());
                Console.Write("| Amt Of Videos:"  + Playlist.ContentDetails.ItemCount);
                Console.WriteLine("| Video Thumbnail:" + Playlist.Snippet.Thumbnails.Standard.Url.ToString()); //Get Video Thumbnail
                //playlists[i] = Playlist.ContentDetails;
                GetPlaylistVideos(Playlist.Id);
            }
            return playlists;
        }

        //Get Videos From Playlist
        internal static string[] GetPlaylistVideos(String playlistId)
        {
            var request = ytService.PlaylistItems.List("contentDetails,snippet");
            request.PlaylistId = playlistId;
            var VideosResponse = request.Execute();
            string[] videos = new string[VideosResponse.Items.Count];
            foreach (var video in VideosResponse.Items)
            {
                Console.WriteLine("Video Title: " + video.Snippet.Title.ToString());
            }
            return videos;
        }

    }
}
