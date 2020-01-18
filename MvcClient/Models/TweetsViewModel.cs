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
        public Dictionary<string, TweetModel> TweetHashTable = new Dictionary<string, TweetModel>();
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

            int counter;
            string startDate = "2016-01-01T00%3A00%3A00.0000000Z";
            do
            {
                counter = 0;
                HttpResponseMessage Res = await client.GetAsync("api/Tweets?startDate=" + startDate + "&endDate=2017-12-31T23%3A59%3A59.999Z");

                if (Res.IsSuccessStatusCode)
                {
                    var Response = Res.Content.ReadAsStringAsync().Result;
                    List<TweetModel> list = JsonConvert.DeserializeObject<List<TweetModel>>(Response);
                    string laststamp = string.Empty;

                    foreach (TweetModel item in list)
                    {
                        if (!TweetHashTable.ContainsKey(item.id))
                        {
                            TweetHashTable.Add(item.id, new TweetModel { id = item.id, stamp = item.stamp, text = item.text });
                        }
                        laststamp = item.stamp;
                        counter++;
                    }

                    if (laststamp != string.Empty)
                    {
                        DateTime newstartdate = Convert.ToDateTime(laststamp);
                        string milliseconds = String.Format("{0:.0000000}", Convert.ToDecimal(laststamp.Substring(laststamp.IndexOf('.'))));
                        startDate = newstartdate.Year.ToString() + "-" + string.Format("{0:00}", newstartdate.Month) + "-" + string.Format("{0:00}", newstartdate.Day) + "T" + string.Format("{0:00}", newstartdate.Hour) + "%3A" + string.Format("{0:00}", newstartdate.Minute) + "%3A" + string.Format("{0:00}", newstartdate.Second) + milliseconds + "Z";
                    }
                }
            } while (counter == 25);
        }
    }

    public class TweetModel
    {
        public string id { get; set; }
        public string stamp { get; set; }
        public string text { get; set; }
    }
}