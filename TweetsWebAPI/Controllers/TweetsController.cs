using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Batch;
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

        public HttpResponseMessage GetHttpMsg(string startDate, int pageno = 1, int pagesize = 100)
        {
            startDate = startDate.Replace("T", " ");
            startDate = startDate.Replace("%3A", ":");
            startDate = startDate.Replace('.', ',');
            startDate = startDate.Replace("Z", "");
            DateTime newstartdate = DateTime.ParseExact(startDate, "yyyy-MM-dd HH:mm:ss,fffffff", System.Globalization.CultureInfo.InvariantCulture);
            List<Tweet> batch = new List<Tweet>();

            int skip = (pageno - 1) * pagesize;
            int total = 0;

            using (TweetsDBEntities1 entities = new TweetsDBEntities1())
            {
                total = entities.Tweets.Count();

                batch = entities.Tweets
                        .Where(t => t.stamp > newstartdate)
                        .OrderBy(t => t.stamp)
                        .Skip(skip)
                        .Take(pagesize)
                        .ToList();
            }

            int pagecount = total > 0
                ? (int)Math.Ceiling(total / (double)pagesize)
                : 0;

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, batch); //1) now returning HttpResponseMessage
            response.Headers.Add("X-Paging-PageNo", pageno.ToString());                      //2) using Headers to contain meta data
            response.Headers.Add("X-Paging-PageSize", pagesize.ToString());
            response.Headers.Add("X-Paging-PageCount", pagecount.ToString());
            response.Headers.Add("X-Paging-TotalRecordCount", total.ToString());

            return response;
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
