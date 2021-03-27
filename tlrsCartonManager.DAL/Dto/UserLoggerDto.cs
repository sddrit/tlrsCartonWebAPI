using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Dto
{
    public class UserLoggerDto
    {
        [Key]       
        public long TrackingId { get; set; }      
        public int? UserId { get; set; }       
        public DateTime? LoginDate { get; set; }       
        public DateTime? ExpiryDate { get; set; }
    }
}
