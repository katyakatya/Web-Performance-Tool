using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WebPerfTool.Domain.Entities;
using WebPerfTool.WebUI.Models;

namespace WebPerfTool.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private static StatsViewModel statsViewModel;
        private  static StatsViewModel StatsViewModel
        {
            get
            {
                if (statsViewModel == null) statsViewModel = new StatsViewModel();
                return statsViewModel;
            }
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SendRequest(LinkStat model)
        {
            string testedLink = model.Url;
            List<LinkStat> list = new List<LinkStat>();
            LinkStat stat;
            HttpWebRequest req;
            Stopwatch timer = new Stopwatch();
            var reqCounts = new[] { 1, 5, 10, 15, 20, 25, 30, 35, 40};
            foreach (var count in reqCounts)
            {
                timer.Start();
                for (int i = 0; i < count; i++)
                {
                    req = (HttpWebRequest)HttpWebRequest.Create(testedLink);
                    using (var resp = req.GetResponse())
                    {
                        var html = new StreamReader(resp.GetResponseStream()).ReadToEnd();
                    }
                }
                timer.Stop();
                stat = StatsViewModel.Stats.FirstOrDefault(x => x.Url == testedLink);
                if (stat == null)
                {
                    stat = new LinkStat();
                    stat.Url = testedLink;
                    stat.StatsResults.Add(count, timer.Elapsed.TotalSeconds);
                    foreach (var item in stat.StatsResults)
                    {
                        stat.AverageResponseTime += item.Value;
                        stat.AverageResponseTime /= reqCounts.Length;
                    }
                    StatsViewModel.Stats.Add(stat);
                    StatsViewModel.Stats = StatsViewModel.Stats.OrderBy(x => x.AverageResponseTime).ToList();
                }
                else
                {
                    stat.StatsResults.Add(count, timer.Elapsed.TotalSeconds);
                }
            }
            return View(StatsViewModel);
        }
    }
}