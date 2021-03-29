using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

namespace Web_Development.Utils.YoutubeApi
{
    public static class YoutubeApi
    {
        private static string api_key = "";
        
        
        public static async Task<YoutubeVideo> GetVideo(string keyword)
        {
            string url =  $"https://www.googleapis.com/youtube/v3/search?part=snippet&maxResults=20&q={keyword}&type=video&key={api_key}";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
     
            WebResponse webResponse;
            try
            {
                webResponse = await request.GetResponseAsync();
            }
            catch
            {
                return null;
            }
            
         
            var webStream = webResponse.GetResponseStream();
            
            var responseReader = new StreamReader(webStream);
            
            var response = await responseReader.ReadToEndAsync();
            
            response = HttpUtility.HtmlDecode(response);
            
            YtApiResponseClasses.YtApiResponse result = JsonConvert.DeserializeObject <YtApiResponseClasses.YtApiResponse>(response);
            responseReader.Close();
            

            int searched_index = 0;
            
            YtApiResponseClasses.Item first = result.items[searched_index];
            

            while (first.id.kind != "youtube#video")
            {
                searched_index += 1;
                first = result.items[searched_index];
            }
            
            return new YoutubeVideo(first.snippet.title, first.snippet.thumbnails.medium.url, first.id.videoId, first.snippet.description);
        }
                
    }
}