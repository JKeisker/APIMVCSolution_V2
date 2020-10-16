using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace MvcClient.Models
{
    public class TweetsViewModel
    {
        public List<TweetModel> TweetList = new List<TweetModel>();
        private static HttpClient client = new HttpClient();

        public async Task LoadTweets()
        {
            string Baseurl = "http://localhost:3270/";

            if (client.BaseAddress == null)
            {
                client.BaseAddress = new Uri(Baseurl);
            }
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string startDate = "2016-01-01T00%3A00%3A00.0000000Z";
            HttpResponseMessage Res = await client.GetAsync("api/Tweets?startDate=" + startDate);

            if (Res.IsSuccessStatusCode)
            {
                var Response = Res.Content.ReadAsStringAsync().Result;
                TweetList = JsonConvert.DeserializeObject<List<TweetModel>>(Response);
            }
        }
    }

    public class TweetModel
    {
        public string id { get; set; }
        public string stamp { get; set; }
        public string text { get; set; }
    }
}