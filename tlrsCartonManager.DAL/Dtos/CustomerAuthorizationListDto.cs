using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Dtos
{
   public class CustomerAuthorizationListDto
    {
        public int TrackingId { get; set; } = 0;
        public int CustomerId { get; set; } = 0;     
        public string Name { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string Designation { get; set; } = string.Empty;
        public int LevelOfAuthority { get; set; } = 0; 
        public string Email { get; set; } = string.Empty;
        public int Status { get; set; } = 0;

    }
    public class CustomerAuthorizationListDisplayDto : CustomerAuthorizationListDto
    {
      
        public int CreatedUser { get; set; }
        public DateTime  CreatedDate{ get; set; }
        public int? LuUser { get; set; }
        public DateTime LuDate { get; set; }
    }
    
   
 
}
