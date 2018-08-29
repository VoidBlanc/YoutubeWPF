using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
namespace YoutubeWPF
{
    class Searches
    {
        private static YouTubeService ytService = YoutubeAPI.Auth();


        public static Video[] SearchVideo(string keyword)
        {
            var request = ytService.Search.List("snippet");
            request.Q = keyword;    
            request.MaxResults = 50;
            var response = request.Execute();

            Video[] videos = new Video[response.Items.Count];

            int i = 0;
            foreach (var item in response.Items)
            {

                switch (item.Id.Kind)
                {
                    case "youtube#video":
                        Video vid = new Video();
                        vid.Id = item.Id.VideoId;
                        vid.ViewCount = GetViewCount(item.Id.VideoId);
                        videos[i] = vid;
                        break;

                    case "youtube#channel":
                        Video vidchan = new Video();
                        vidchan.Id = item.Id.ChannelId;
                        videos[i] = vidchan;
                        break;

                    case "youtube#playlist":
                       
                        break;
                }

                videos[i].Thumbnail = item.Snippet.Thumbnails.Default__.Url;

                i = i++;
            }


            return videos;
        }

        internal static long GetViewCount(string vidid)
        {
            long result = 0;
            var viewCountRequest = ytService.Videos.List("statistics");
            viewCountRequest.Id = vidid;
            var viewCountResponse = viewCountRequest.Execute();
            int[] videos = new int[viewCountResponse.Items.Count];
            //Below error
            int i = 0;
            foreach (var item in viewCountResponse.Items)
            {
                result = (long)item.Statistics.ViewCount;
                i = i++;
            }
            return result;
        }

    }
}
