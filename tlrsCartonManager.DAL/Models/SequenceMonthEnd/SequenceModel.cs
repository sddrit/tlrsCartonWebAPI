using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Models.SequenceMonthEnd
{
    public class SequenceModel
    {

        public string SequenceType { get; set; }
        public int LastNo { get; set; }
        public bool Active { get; set; }
        public string CurrentSuffix { get; set; }

        public string RequestTypeCode { get; set; }

    }
}
