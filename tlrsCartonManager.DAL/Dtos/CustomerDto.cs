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
        public int TrackingId { get; set; } = 0;
        public string CustomerCode { get; set; } = string.Empty;    
        public string Name { get; set; } = string.Empty;
        public string Address1 { get; set; } = string.Empty;
        public string Address2 { get; set; } = string.Empty;
        public string Address3 { get; set; } = string.Empty;
        public string Telephone1 { get; set; } = string.Empty;
        public string Telephone2 { get; set; } = string.Empty;
        public string Fax { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
        public decimal? CountryId { get; set; } = 0;      
        public string Email { get; set; } = string.Empty;
        public string ContractNo { get; set; } = string.Empty;
        public DateTime? ContractStartDate { get; set; } = new DateTime(1900, 1, 1);
        public DateTime? ContractEndDate { get; set; } = new DateTime(1900, 1, 1);
        public string DeliveryName { get; set; } = string.Empty;
        public string DeliveryAddress1 { get; set; } = string.Empty;
        public string DeliveryAddress2 { get; set; } = string.Empty;
        public string DeliveryAddress3 { get; set; } = string.Empty;
        public string DeliveryTelephone1 { get; set; } = string.Empty;
        public string DeliveryTelephone2 { get; set; } = string.Empty;
        public string DeliveryFax { get; set; } = string.Empty;
        public string PickUpName { get; set; } = string.Empty;
        public string PickUpAddress1 { get; set; } = string.Empty;
        public string PickUpAddress2 { get; set; } = string.Empty;
        public string PickUpAddress3 { get; set; } = string.Empty;
        public string PickUpTelephone1 { get; set; } = string.Empty;
        public string PickUpTelephone2 { get; set; } = string.Empty;
        public string PickUpFax { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string ContactName { get; set; } = string.Empty;
        public string ContactAddress1 { get; set; } = string.Empty;
        public string ContactAddress2 { get; set; } = string.Empty;
        public string ContactAddress3 { get; set; } = string.Empty;
        public string ContactTelephone1 { get; set; } = string.Empty;
        public string ContactTelephone2 { get; set; } = string.Empty;
        public string ContactFax { get; set; } = string.Empty;
        public string PoNo { get; set; } = string.Empty;
        public string VatNo { get; set; } = string.Empty;
        public string SvatNo { get; set; } = string.Empty;
        public int? BillingCycle { get; set; } = 0;
        public int? Route { get; set; } = 0;
        public bool? IsSeparateInvoice { get; set; } = false;      
        public string ContactPersonInv { get; set; } = string.Empty;
        public bool? SubInvoice { get; set; } = false;
        public int? ServiceProvided { get; set; } = 0;       
        public string AccountType { get; set; } = string.Empty;
        public int? MainCustomerCode { get; set; } = 0;
        public bool Active { get; set; } = false;
        public int? User { get; set; } = 0;
        public virtual ICollection<CustomerAuthorizationListHeaderDto> CustomerAuthorizationListHeaders { get; set; } = new List<CustomerAuthorizationListHeaderDto>();
        public virtual ICollection<CustomerSubAccountListDto> CustomerSubAccountLists { get; set; } = new List<CustomerSubAccountListDto>();

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
