using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Net;

namespace MvcClient.Models
{
    public class TweetsViewModel
    {
        public List<TweetModel> TweetList = new List<TweetModel>();
        public HttpResponseHeaders _HttpClientResponseHeaders { get; set; }
        public WebHeaderCollection _WebClientResponseHeaders { get; set; }

        public async Task LoadTweets()
        {
            string Baseurl = "http://localhost:3270/";

            using (HttpClient client = new HttpClient())
            {
                if (client.BaseAddress == null)
                {
                    client.BaseAddress = new Uri(Baseurl);
                }
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string startDate = "2016-01-01T00%3A00%3A00.0000000Z";
                HttpResponseMessage Res = await client.GetAsync("api/Tweets/GetHttpMsg?startDate=" + startDate);

                if (Res.IsSuccessStatusCode)
                {
                    var Response = Res.Content.ReadAsStringAsync().Result;
                    _HttpClientResponseHeaders = Res.Headers;
                    TweetList = JsonConvert.DeserializeObject<List<TweetModel>>(Response);
                }
            }
        }

        public void LoadTweetsViaWebClient()
        {
            using (WebClient client = new WebClient())
            {
                Dictionary<string, string> headers = new Dictionary<string, string>();
                string startDate = "2016-01-01T00%3A00%3A00.0000000Z";
                Uri address = new Uri("http://localhost:3270/api/Tweets/GetHttpMsg?startDate=" + startDate);

                client.Headers.Clear();
                client.Headers.Add(HttpRequestHeader.Accept, "application/json");
                string json = client.DownloadString(address);
                _WebClientResponseHeaders = client.ResponseHeaders;

                //for (int i = 0; i < _WebClientResponseHeaders.Count; i++)
                //{
                //    string name = _WebClientResponseHeaders.GetKey(i);
                //    string v = _WebClientResponseHeaders.Get(i);
                //    if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(v))
                //    {
                //        headers.Add(name, v);
                //    }
                //}

                if (!string.IsNullOrEmpty(json))
                {
                    TweetList = JsonConvert.DeserializeObject<List<TweetModel>>(json);
                }
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