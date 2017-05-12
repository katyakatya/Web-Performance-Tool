using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Collections.Specialized;

namespace WebPerfTool.Domain.Entities
{
    public class LinkStat
    {
        [Required(ErrorMessage = "Please enter a link to test")]
        public string Url { get; set; }
        public double AverageResponseTime { get; set; }
        public  Dictionary<int, double> StatsResults  {get; set;}
        public LinkStat()
        {
            StatsResults = new Dictionary<int, double>(); // stats collection
        }
        
    }
}
