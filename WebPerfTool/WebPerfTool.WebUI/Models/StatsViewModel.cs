using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebPerfTool.Domain.Entities;

namespace WebPerfTool.WebUI.Models
{
    public class StatsViewModel
    {
        public List<LinkStat> Stats { get; set; }
        public LinkStat Last { get { return Stats.LastOrDefault(); } }
        public StatsViewModel()
        {
            Stats = new List<LinkStat>();
        }
    }
}