using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Models.SystemLogs
{
    public class AuditTrailUserActivityModel
    {
        public Int64  Id { get; set; }
        public string Action { get; set; }
        public string Module { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UserFullName { get; set; }
        public int UserId { get; set; }

    }
}
