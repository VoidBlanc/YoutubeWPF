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
    class Search
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
                videos[i].ChannelId = item.Snippet.ChannelId;
                videos[i].Id = ""+item.Id;
                videos[i].Thumbnail = item.Snippet.Thumbnails.Default__.Url;
                var viewCountRequest = ytService.Videos.List("statistics");
                viewCountRequest.Id = "" + item.Id;
                var viewCountResponse = viewCountRequest.Execute();
                videos[i].ViewCount = (long)viewCountResponse.Items[0].Statistics.ViewCount;
                i = i++;
            }


            return videos;
        }

    }
}
