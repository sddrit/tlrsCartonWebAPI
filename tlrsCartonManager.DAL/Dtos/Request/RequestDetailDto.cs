using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Dtos
{
    public class RequestDetailDto
    {
        [Key]
        public long TrackingId { get; set; }       
      
        public string RequestNo { get; set; }
      
        public int CartonNo { get; set; }
      
        public int? DisposalDate { get; set; }
      
        public int? DisposalTimeFrame { get; set; }
       
        public bool? FromMobile { get; set; }
      
        public bool? Picked { get; set; }
     
        public string PickListNo { get; set; }
       
        public int? Status { get; set; }     
      
        public string FeedBack { get; set; }
      
        public DateTime? StatusInDate { get; set; }

        public int? User { get; set; }

        public int? ToCartonNo { get; set; }
    }


    public class CustomerPortalRequestDetailDto
    {
       
        public int CartonNo { get; set; }       
       
    }

}
