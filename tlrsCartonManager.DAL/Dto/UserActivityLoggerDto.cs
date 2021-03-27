using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Dto
{
    public class UserActivityLoggerDto
    {
        public int TrackingId { get; set; }       
        public int? UserId { get; set; }      
        public DateTime? ActivityDate { get; set; }       
        public int? ActivityId { get; set; }        
        public string ActivityLog { get; set; }       
        public int? ActivityType { get; set; }

    }
}
