using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Dtos
{
    public class InvoiceHeaderDto
    {

        [Key]       
        public string InvoiceId { get; set; }       
        public long TrackingId { get; set; }      
        public int? CustomerId { get; set; }
        public string Name { get; set; }
        public int? FromDate { get; set; }      
        public int? ToDate { get; set; }      
        public decimal? InvoiceValue { get; set; }       
        public int? InvoiceProfielId { get; set; }        
        public int? CreatedUserId { get; set; }      
        public DateTime? CreatedDate { get; set; }       
        public int? LuUserId { get; set; }       
        public DateTime? LuDate { get; set; }      
        public string CustomerAddress { get; set; }       
        public string ContactPerson { get; set; }       
        public string PoNo { get; set; }       
        public string VatNo { get; set; }
        

        public virtual ICollection<InvoiceDetailDto> InvoiceDetails { get; set; }
    }
    public class InvoiceSearchDto
    {
        public string InvoiceId { get; set; }
        public string CustomerCode { get; set; }

        public string CustomerName { get; set; }
        public string InvoicePeriod { get; set; }
        public decimal InvoiceValue { get; set; }

    }
    public class InvoicePrintModel
    {
       
        public string InvoiceId { get; set; }      
        public string CustomerCode { get; set; }      
        public string Name { get; set; }
        public int? FromDate { get; set; }
        public int? ToDate { get; set; }     
        public decimal? InvoiceValue { get; set; }            
        public string CustomerAddress { get; set; }        
        public string ContactPerson { get; set; }       
        public string PoNo { get; set; }               
        public string VatNo { get; set; }

        public  ICollection<InvoiceDetailDto> InvoiceDetails { get; set; }
    }
}
