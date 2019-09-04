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
        public IEnumerable<Tweet> GET(string startDate, string endDate)
        {
            DateTime newstartdate = DateTimeOffset.Parse(startDate).UtcDateTime;

            using (TweetsDBEntities1 entities = new TweetsDBEntities1())
            {
                var batch = (from t in entities.Tweets
                             where (t.stamp >= newstartdate)
                             orderby t.stamp
                             select t).Take(50).ToList();

                return batch;
            }
        }
    }
}
