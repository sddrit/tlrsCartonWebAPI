using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Dtos
{
    public class RequestHeaderDto
    {        
        public string RequestNo { get; set; }       
        public long TrackingId { get; set; }       
        public int? CustomerId { get; set; }     
        public int? Priority { get; set; }       
        public int? DeliveryDate { get; set; }      
        public int? OrderReceivedBy { get; set; }
        public string Remarks { get; set; }      
        public string CustomerReference { get; set; }       
        public int AuthorizedOfficerId { get; set; }      
        public int? CartonCount { get; set; }  
        public string RequestType { get; set; }       
        public int? Status { get; set; }       
        public int? CartonType { get; set; }        
        public string WorkOrderType { get; set; }      
        public string DeviceId { get; set; }       
        public string MobileRequestNo { get; set; }       
        public string ContactPersonName { get; set; }     
        public string DeliveryLocation { get; set; }      
        public string DeliveryRoute { get; set; }       
        public string Reminder1 { get; set; }       
        public string Reminder2 { get; set; }       
        public string Reminder3 { get; set; }               
        public int? DeliveryRouteId { get; set; }     
        public int ServiceTypeId { get; set; }     
        public string DocketNo { get; set; }
        public int? UserId { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public virtual ICollection<RequestDetailDto> RequestDetails { get; set; }
        public virtual ICollection<CustomerAuthorizationHeaderDto> AuthorizedOfficers { get; set; }
    }
    public class RequestSearchDto
    {
        public string RequestNo { get; set; }
        public string  CustomerCode { get; set; }
        public string  CustomerName { get; set; }
        public int CartonCount { get; set; }
        public int DeliveryDate { get; set; }
    }

    public class OriginalDocketSearchDto
    {
        public string RequestNo { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string WoType { get; set; }
        public int NoOfCartons { get; set; }
        public int DeliveryDate { get; set; }
        public string DocketNo { get; set; }
    }
    public class RequestValidationModel
    {
        public string RequestNo { get; set; }       
        public string CustomerCode { get; set; }
        public string RequestType { get; set; }
        public string TransactionType { get; set; }
        public List<CartonValidationModel> CartonList { get; set; }

    }
    public class CartonValidationModel
    {
        public string CartonNo { get; set; }
    }
}
