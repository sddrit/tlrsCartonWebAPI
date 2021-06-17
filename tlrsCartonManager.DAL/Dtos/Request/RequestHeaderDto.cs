using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
    public class RequestAlternateValidationModel
    {
        public string RequestNo { get; set; }
        public string CustomerCode { get; set; }
        public string RequestType { get; set; }
        public string TransactionType { get; set; }
        public List<AlternateValidationModel> AlternateList { get; set; }

    }
    public class CartonValidationModel
    {
        public string CartonNo { get; set; }
    }
    public class AlternateValidationModel
    {
        public string AlternateNo { get; set; }
    }
    public class DocketPrintDetailModel
    {
        public int? Id { get; set; }

        public int? No1 { get; set; }
        public string Col1 { get; set; }

        public int? No2 { get; set; }
        public string Col2 { get; set; }

        public int? No3 { get; set; }
        public string Col3 { get; set; }

        public int? No4 { get; set; }
        public string Col4 { get; set; }


    }
    public class DocketPrintEmptyDetailModel
    {
        public int? No { get; set; }
        public int? FromCarton { get; set; }
        public int? ToCarton { get; set; }

    }
    public class DocketPrintModel
    {
        public string RequestNo { get; set; }
        public string PrintedBy { get; set; }
        public string RequestType { get; set; }
       
    }
    public class DocketRePrintModel
    {
        public int? SerialNo { get; set; }
        public string RequestNo { get; set; }
        public string PrintedBy { get; set; }
        public string RequestType { get; set; }

    }
    public class DocketPrintResultModel
    {
        public string RequestNo { get; set; }
        public string DocketType { get; set; }
        public int SerialNo { get; set; }
        public string CustomerCode { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string ContactPerson { get; set; }
        public string PONo { get; set; }
        public string ContactNo { get; set; }    
        public string Department { get; set; }        
        public List<DocketPrintEmptyDetailModel> EmptyList { get; set; }
        public List<DocketPrintDetailModel> CartonList { get; set; }

    }
  
}
