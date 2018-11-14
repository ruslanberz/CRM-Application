using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMv2.Models
{
    class vwTask
    {
        public int id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }
        public string Description { get; set; }
    }
}
