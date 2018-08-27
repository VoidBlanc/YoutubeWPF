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
        public string channelId { get; set; }
        public string channelTitle { get; set; }
        public string title { get; set; }
        public string description { get; set; } //Keep for now incase user wants to view video(Saves time on retrieving it again)
        public string thumbnail { get; set; }
    }
}
