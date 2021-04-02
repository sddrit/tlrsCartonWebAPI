using System;
using System.Collections.Generic;
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
}
