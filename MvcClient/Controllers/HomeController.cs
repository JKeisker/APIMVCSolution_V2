using System.Web.Mvc;
using MvcClient.Models;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MvcClient.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            return View();
        }

        public async Task<PartialViewResult> GetTweets()
        {
            TweetsViewModel tvm = new TweetsViewModel();
            await tvm.LoadTweets();
            return PartialView("_TweetGrid", tvm);
        }
    }
}