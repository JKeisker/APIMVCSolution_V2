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
            using (TweetsDBEntities entities = new TweetsDBEntities())
            {
                return entities.Tweets.ToList();
            }
        }
    }
}
