using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMv2.Models
{
   public class vwTaskReport
    {
        public string CustomerName { get; set; }
        public string UserName { get; set; }
        public string Description { get; set; }
        public string CreationTime { get; set; }
        public string DeadlineTime { get; set; }
        public bool isFinished { get; set; }
        public string FinishedTime { get; set; }
    }
}
