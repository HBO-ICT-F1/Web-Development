using System.Collections.Generic;

namespace Web_Development.Utils.YoutubeApi
{
    public class YoutubeVideo
    {
        public YoutubeVideo(string Title, string Thumbnail_url, string VideoId, string description)
        {
            this.Title = Title;
            this.Thumbnail_url = Thumbnail_url;
            this.VideoId = VideoId;
            this.Description = description;
        }

        public YoutubeVideo(YtApiResponseClasses.Item i)
        {
            this.Title = i.snippet.title;
            this.Thumbnail_url = i.snippet.thumbnails.medium.url;
            this.VideoId = i.id.videoId;
            this.Description = i.snippet.description;
        }
        
        
        public string Title { get; set; }
        public string Thumbnail_url { get; set; }
        public string VideoId { get; set; }
        
        public string Description { get; set; }
        public string getFullUrl()
        {
            return "https://www.youtube.com/watch?v=" + VideoId;
        }

        public List<string> GetImages()
        {
            return new List<string>()
            {
                $"https://img.youtube.com/vi/{VideoId}/0.jpg",
                $"https://img.youtube.com/vi/{VideoId}/1.jpg",
                $"https://img.youtube.com/vi/{VideoId}/2.jpg",
                $"https://img.youtube.com/vi/{VideoId}/3.jpg"
            };
        }
        
        
    }
    
}