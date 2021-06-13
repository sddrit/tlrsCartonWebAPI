using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Dtos
{
    public class CustomerSubAccountListDto
    {
        
        public int TrackingId { get; set; }      
        public string CustomerCode { get; set; }      
        public string Name { get; set; }       
        public string Address1 { get; set; }
    }
}
