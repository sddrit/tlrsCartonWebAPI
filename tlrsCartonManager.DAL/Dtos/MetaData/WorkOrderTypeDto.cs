using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Dtos.MetaData
{
    public class WorkOrderTypeDto
    {
       
        public string TypeCode { get; set; }       
        public string RequestTypeCode { get; set; }       
        public bool? Active { get; set; }      

    }
}
