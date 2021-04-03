using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace tlrsCartonManager.DAL.Dtos
{
    public class CustomerDto
    {
        [Key]
       
        public int TrackingId { get; set; }       
        public string CustomerCode { get; set; }       
        public string Name { get; set; }        
        public string Address1 { get; set; }        
        public string Address2 { get; set; }       
        public string Address3 { get; set; }       
        public string Telephone1 { get; set; }       
        public string Telephone2 { get; set; }       
        public string Fax { get; set; }        
        public string ZipCode { get; set; }      
        public decimal? CountryId { get; set; }        
        public string Email { get; set; }       
        public string ContractNo { get; set; }       
        public DateTime? ContractStartDate { get; set; }      
        public DateTime? ContractEndDate { get; set; }       
        public string DeliveryName { get; set; }       
        public string DeliveryAddress1 { get; set; }      
        public string DeliveryAddress2 { get; set; }       
        public string DeliveryAddress3 { get; set; }       
        public string DeliveryTelephone1 { get; set; }       
        public string DeliveryTelephone2 { get; set; }       
        public string DeliveryFax { get; set; }       
        public string PickUpName { get; set; }     
        public string PickUpAddress1 { get; set; }      
        public string PickUpAddress2 { get; set; }        
        public string PickUpAddress3 { get; set; }      
        public string PickUpTelephone1 { get; set; }      
        public string PickUpTelephone2 { get; set; }      
        public string PickUpFax { get; set; }      
        public string City { get; set; }       
        public string ContactName { get; set; }       
        public string ContactAddress1 { get; set; }        
        public string ContactAddress2 { get; set; }        
        public string ContactAddress3 { get; set; }        
        public string ContactTelephone1 { get; set; }        
        public string ContactTelephone2 { get; set; }       
        public string ContactFax { get; set; }      
        public string PoNo { get; set; }       
        public string VatNo { get; set; }       
        public string SvatNo { get; set; }       
        public int? BillingCycle { get; set; }      
        public int? Route { get; set; }       
        public bool? IsSeparateInvoice { get; set; }        
        public string ContactPersonInv { get; set; }        
        public bool? SubInvoice { get; set; }        
        public int? ServiceProvided { get; set; }        
        public string AccountType { get; set; }        
        public int? MainCustomerCode { get; set; }      
        public bool Active { get; set; }     
        public int? User { get; set; }            
        public virtual ICollection<CustomerAuthorizationListHeaderDto> CustomerAuthorizationListHeaders { get; set; }
        public virtual ICollection<CustomerSubAccountListDto> CustomerSubAccountLists { get; set; }

    }
    //public class CustomerDisplayDto:CustomerDto
    //{

    //}
    public class CustomerInsertUpdateDto : CustomerDto    
    {
        public int TrackingId { get; set; }
        public virtual ICollection<CustomerAuthorizationListHeaderDto> CustomerAuthorizationLists { get; set; } = new List<CustomerAuthorizationListHeaderDto>();
    }
    public class CustomerDeleteDto 
    {
        public int TrackingId { get; set; }       
    }
    public class CustomerInsertDto : CustomerDto
    {
        public virtual ICollection<CustomerAuthorizationListInsertDto> CustomerAuthorizationLists { get; set; } = new List<CustomerAuthorizationListInsertDto>();

    }
    public class CustomerSearchDto 
    {
        public int TrackingId { get; set; }
        public string CustomerCode { get; set; }
        public string Name { get; set; }
        public string Address1 { get; set; }
        public string AccountType { get; set; }
    }
    public class CustomerMainCodeSearchDto
    {
        [Key]
        public int TrackingId { get; set; }
        public string CustomerCode { get; set; }
        public string Name { get; set; }
    }

}
