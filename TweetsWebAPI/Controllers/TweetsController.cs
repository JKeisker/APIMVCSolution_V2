using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TweetsDataLayer;

namespace TweetsWebAPI.Controllers
{
    public class TweetsController : ApiController
    {
        public IEnumerable<Tweet> GET(string startDate)
        {
            startDate = startDate.Replace("T", " ");
            startDate = startDate.Replace("%3A", ":");
            startDate = startDate.Replace('.', ',');
            startDate = startDate.Replace("Z", "");
            DateTime newstartdate = DateTime.ParseExact(startDate, "yyyy-MM-dd HH:mm:ss,fffffff", System.Globalization.CultureInfo.InvariantCulture);

            using (TweetsDBEntities1 entities = new TweetsDBEntities1())
            {
                var batch = (from t in entities.Tweets
                             where (t.stamp > newstartdate)
                             orderby t.stamp
                             select t).ToList();
                return batch;
            }
        }

        public string GET()
        {
            return "Hello thar !";
        }

        public string GetTimeNow()
        {
            return DateTime.Now.ToString();
        }
    }
}
