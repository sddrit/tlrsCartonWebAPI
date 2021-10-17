using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
    [Table("Customer")]
    public partial class Customer
    {
        public Customer()
        {
            CustomerAuthorizationListHeaders = new HashSet<CustomerAuthorizationListHeader>();
           
        }

        [Key]
        [Column("trackingId")]
        public int TrackingId { get; set; }
        [Required]
        [Column("customerCode")]
        [StringLength(20)]
        public string CustomerCode { get; set; }
        [Column("name")]
        [StringLength(50)]
        public string Name { get; set; }
        [Column("address1")]
        [StringLength(50)]
        public string Address1 { get; set; }
        [Column("address2")]
        [StringLength(50)]
        public string Address2 { get; set; }
        [Column("address3")]
        [StringLength(50)]
        public string Address3 { get; set; }
        [Column("telephone1")]
        [StringLength(11)]
        public string Telephone1 { get; set; }
        [Column("telephone2")]
        [StringLength(11)]
        public string Telephone2 { get; set; }
        [Column("fax")]
        [StringLength(11)]
        public string Fax { get; set; }
        [Column("zipCode")]
        [StringLength(50)]
        public string ZipCode { get; set; }
        [Column("countryId", TypeName = "numeric(10, 0)")]
        public decimal? CountryId { get; set; }
        [Column("email")]
        [StringLength(50)]
        public string Email { get; set; }
        [Column("contractNo")]
        [StringLength(50)]
        public string ContractNo { get; set; }
        [Column("contractStartDate", TypeName = "datetime")]
        public DateTime? ContractStartDate { get; set; }
        [Column("contractEndDate", TypeName = "datetime")]
        public DateTime? ContractEndDate { get; set; }
        [Column("deliveryName")]
        [StringLength(50)]
        public string DeliveryName { get; set; }
        [Column("deliveryAddress1")]
        [StringLength(50)]
        public string DeliveryAddress1 { get; set; }
        [Column("deliveryAddress2")]
        [StringLength(50)]
        public string DeliveryAddress2 { get; set; }
        [Column("deliveryAddress3")]
        [StringLength(50)]
        public string DeliveryAddress3 { get; set; }
        [Column("deliveryTelephone1")]
        [StringLength(50)]
        public string DeliveryTelephone1 { get; set; }
        [Column("deliveryTelephone2")]
        [StringLength(50)]
        public string DeliveryTelephone2 { get; set; }
        [Column("deliveryFax")]
        [StringLength(50)]
        public string DeliveryFax { get; set; }
        [Column("pickUpName")]
        [StringLength(50)]
        public string PickUpName { get; set; }
        [Column("pickUpAddress1")]
        [StringLength(50)]
        public string PickUpAddress1 { get; set; }
        [Column("pickUpAddress2")]
        [StringLength(50)]
        public string PickUpAddress2 { get; set; }
        [Column("pickUpAddress3")]
        [StringLength(50)]
        public string PickUpAddress3 { get; set; }
        [Column("pickUpTelephone1")]
        [StringLength(50)]
        public string PickUpTelephone1 { get; set; }
        [Column("pickUpTelephone2")]
        [StringLength(50)]
        public string PickUpTelephone2 { get; set; }
        [Column("pickUpFax")]
        [StringLength(50)]
        public string PickUpFax { get; set; }
        [Column("city")]
        [StringLength(50)]
        public string City { get; set; }
        [Column("contactName")]
        [StringLength(50)]
        public string ContactName { get; set; }
        [Column("contactAddress1")]
        [StringLength(50)]
        public string ContactAddress1 { get; set; }
        [Column("contactAddress2")]
        [StringLength(50)]
        public string ContactAddress2 { get; set; }
        [Column("contactAddress3")]
        [StringLength(50)]
        public string ContactAddress3 { get; set; }
        [Column("contactTelephone1")]
        [StringLength(50)]
        public string ContactTelephone1 { get; set; }
        [Column("contactTelephone2")]
        [StringLength(50)]
        public string ContactTelephone2 { get; set; }
        [Column("contactFax")]
        [StringLength(50)]
        public string ContactFax { get; set; }
        [Column("poNo")]
        [StringLength(50)]
        public string PoNo { get; set; }
        [Column("vatNo")]
        [StringLength(50)]
        public string VatNo { get; set; }
        [Column("svatNo")]
        [StringLength(50)]
        public string SvatNo { get; set; }
        [Column("billingCycle")]
        public int? BillingCycle { get; set; }
        [Column("route")]
        public int? Route { get; set; }
        [Column("isSeparateInvoice")]
        public bool? IsSeparateInvoice { get; set; }
        [Column("contactPersonInv")]
        [StringLength(500)]
        public string ContactPersonInv { get; set; }
        [Column("subInvoice")]
        public bool? SubInvoice { get; set; }
        [Column("serviceProvided")]
        public int? ServiceProvided { get; set; }
        [Column("accountType")]
        [StringLength(2)]
        public string AccountType { get; set; }
        [Column("mainCustomerCode")]
        public int? MainCustomerCode { get; set; }
        [Column("active")]
        public bool? Active { get; set; }
        [Column("createdUserId")]
        public int? CreatedUserId { get; set; }
        [Column("createdDate", TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column("luUserId")]
        public int? LuUserId { get; set; }
        [Column("luDate", TypeName = "datetime")]
        public DateTime? LuDate { get; set; }
        [Column("deleted")]
        public bool Deleted { get; set; }

        [ForeignKey(nameof(BillingCycle))]
        [InverseProperty("Customers")]
        public virtual BillingCycle BillingCycleNavigation { get; set; }
        [ForeignKey(nameof(Route))]
        [InverseProperty("Customers")]
        public virtual Route RouteNavigation { get; set; }
        [ForeignKey(nameof(ServiceProvided))]
        [InverseProperty(nameof(ServiceCategory.Customers))]
        public virtual ServiceCategory ServiceProvidedNavigation { get; set; }
        [InverseProperty(nameof(CustomerAuthorizationListHeader.Customer))]
        public virtual ICollection<CustomerAuthorizationListHeader> CustomerAuthorizationListHeaders { get; set; }

        [Column("includeMainAccountAuthorization")]
        public bool? IncludeMainAccountAuthorization { get; set; }

        [Column("isManualInvoice")]
        public bool? IsManualInvoice { get; set; }

        [Column("billingName")]
        public string BillingName { get; set; }


        [NotMapped]       
        public string Address
        {
            get
            {
                return Address1 + ", " + Address2 + ", " + Address3;

            }
            set { }
        }


    }
}
