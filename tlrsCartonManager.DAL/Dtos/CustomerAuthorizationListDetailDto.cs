using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Dtos
{
    public class CustomerAuthorizationListDetailDto
    {      
        public int TrackingId { get; set; }       
        public int? AuthorizationId { get; set; }      
        public int? Level { get; set; }
    }
    public class CustomerAuthorizationListDetailUdtDto
    {
        [Key]
        public int AutoId { get; set; }
        public int TrackingId { get; set; }
        public int? AuthorizationId { get; set; }
        public int? Level { get; set; }
    }
}
