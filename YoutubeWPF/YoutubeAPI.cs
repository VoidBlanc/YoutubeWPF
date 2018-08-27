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



        private static YouTubeService Auth()
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

        internal static string[] GetPlaylist()
        {
            var request = ytService.PlaylistItems.List("contentDetails");
            request.PlaylistId = "RDTJJVNrrCgw0";
            var response = request.Execute();

            string[] videos = new string[response.Items.Count];

            int i = 0;
            foreach (var item in response.Items)
            {
                videos[i++] = "" + item.ContentDetails.VideoId;
            }

            return videos;
        }

        internal static Video[] SearchVideo(string keyword)
        {
            var request = ytService.Search.List("snippet");
            request.Q = keyword;
            request.MaxResults = 50;
            var response = request.Execute();

            Video[] videos = new Video[response.Items.Count];

            int i = 0;
            foreach (var item in response.Items)
            {
                videos[i++] = item.Snippet
            }


            return videos;
        }
    }
}
