using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Dtos
{
    public class CustomerAuthorizationListHeaderDto
    {
        public int TrackingId { get; set; } = 0;
        public int CustomerId { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string Designation { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool Active { get; set; } = false;
        public bool? Deleted { get; set; } = false;
        public virtual ICollection<CustomerAuthorizationListDetailDto> CustomerAuthorizationListDetails { get; set; }
    }

    public class CustomerAuthorizationListInsertDto
    {    
        public int CustomerId { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string Designation { get; set; } = string.Empty;
        public int LevelOfAuthority { get; set; } = 0;
        public string Email { get; set; } = string.Empty;
        public int Status { get; set; } = 0;

    }
  
    public class CustomerAuthorizationListDisplayDto : CustomerAuthorizationListHeaderDto
    {
      
        public int CreatedUser { get; set; }
        public DateTime  CreatedDate{ get; set; }
        public int? LuUser { get; set; }
        public DateTime LuDate { get; set; }
    }



}
