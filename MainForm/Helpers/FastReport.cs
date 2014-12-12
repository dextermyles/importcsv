using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharepointListImport.Helpers
{
    [Serializable]
    public class FastReport
    {
        public int nid { get; set; }
        public int vid { get; set; }
        public int report_date { get; set; }
        public int report_volume { get; set; }
        public int report_number { get; set; }
        public string whats_inside { get; set; }
        public string report_events { get; set; }
        public string footer { get; set; }
    }
}
