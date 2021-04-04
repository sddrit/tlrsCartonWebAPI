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
        public string ContactNo { get; set; } = string.Empty;
        public virtual ICollection<CustomerAuthorizationListDetailDto> CustomerAuthorizationListDetails { get; set; }
    }

    public class CustomerAuthorizationListUtdDto
    {


        public int AutoId { get; set; }

        public int TrackingId { get; set; } = 0;

        public int CustomerId { get; set; } = 0;

        public string Name { get; set; } = string.Empty;

        public string Department { get; set; } = string.Empty;

        public string Designation { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public bool Active { get; set; } = false;
        public string ContactNo { get; set; } = string.Empty;

    }



}
