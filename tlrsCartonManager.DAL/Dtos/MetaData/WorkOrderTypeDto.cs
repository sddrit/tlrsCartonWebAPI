using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Dtos.MetaData
{
    public class WorkOrderTypeDto
    {
        [Key]
        public int Id { get; set; }
        public string TypeCode { get; set; }       
        public string RequestTypeCode { get; set; }
        public string Description { get; set; }
        public bool? Active { get; set; }
        public bool? ShowInventoryReport { get; set; }

    }
}
