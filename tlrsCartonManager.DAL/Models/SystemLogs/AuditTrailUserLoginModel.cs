using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Models.SystemLogs
{
    public class AuditTrailUserLoginModel
    {
        public int TrackingId { get; set; }
        public string HostName { get; set; }       
        public DateTime LoginDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string UserFullName { get; set; }
        public int UserId { get; set; }

    }
}
