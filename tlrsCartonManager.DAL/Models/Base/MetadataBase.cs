using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Models.Base
{
    public class  MetadataBase
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string TypeCode { get; set; }
        public string RequestTypeCode { get; set; }
        public string Code { get; set; }
        public bool? Active { get; set; }
        public string Size { get; set; }
        public bool Deleted { get; set; }
    }
}
