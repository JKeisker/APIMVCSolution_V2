using System.Web.Mvc;
using MvcClient.Models;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System;

namespace MvcClient.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            return View();
        }

        public async Task<PartialViewResult> GetTweets() //Utilizes HttpClient
        {
            TweetsViewModel tvm = new TweetsViewModel();
            await tvm.LoadTweets();
            return PartialView("_TweetGrid", tvm);
        }

        public PartialViewResult GetTweets2() //Utilizes WebClient
        {
            TweetsViewModel tvm = new TweetsViewModel();
            tvm.LoadTweetsViaWebClient();
            return PartialView("_TweetGrid", tvm);
        }

        public string GetTimeNow()
        {
            DateTime d = DateTime.Now;
            return d.ToString();
        }
    }
}