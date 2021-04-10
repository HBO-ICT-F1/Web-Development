using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

namespace Web_Development.Utils
{
    public static class Youtube
    {
        /// <summary>
        ///     Google APIs api key for youtube data v3
        /// </summary>
        private const string ApiKey = "";

        /// <summary>
        ///     Endpoint used for requesting videos from the Youtube api
        /// </summary>
        private static readonly string BaseUrl =
            $"https://www.googleapis.com/youtube/v3/search?part=snippet&type=video&maxResults=1&key={ApiKey}";

        /// <summary>
        ///     Gets the first youtube video's id by searching for keywords
        /// </summary>
        /// <param name="keywords">The keywords to search for</param>
        /// <returns>The youtube video ID, or null if it failed</returns>
        public static async Task<string> GetVideoId(string keywords)
        {
            // Encode keywords for request
            var encoded = HttpUtility.UrlEncode(keywords);
            if (encoded == null) return null;

            // Absolute url to send as request
            var url = $"{BaseUrl}&q={encoded}";

            try
            {
                // Create HTTP request
                var request = WebRequest.CreateHttp(url);

                // Gets the response from the api
                using var response = request.GetResponse();
                var stream = response.GetResponseStream();
                if (stream == null) return null;

                // Create StreamReader to read contents from response
                using var reader = new StreamReader(stream);
                var contents = await reader.ReadToEndAsync();

                // Deserialize to response object and get VideoId
                var deserialized = JsonConvert.DeserializeObject<Response>(contents);
                return deserialized?.Items[0].Id.VideoId;
            }
            catch (Exception e)
            {
                // Print error to console
                Console.WriteLine(e.StackTrace);
            }

            return null;
        }
    }

    internal class Response
    {
        public List<Video> Items { get; set; }
    }

    internal class Video
    {
        public Id Id { get; set; }
    }

    internal class Id
    {
        public string VideoId { get; set; }
    }
}