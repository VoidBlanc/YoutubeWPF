using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoutubeWPF
{
    class Video
    {
        public string Id { get; set; }
        public string ChannelId { get; set; }
        public string ChannelTitle { get; set; }
        public string Title { get; set; }
        public string Description { get; set; } //Keep for now incase user wants to view video(Saves time on retrieving it again)
        public string Thumbnail { get; set; } // To get thumbnail URL
        public long ViewCount { get; set; }
    }
}
