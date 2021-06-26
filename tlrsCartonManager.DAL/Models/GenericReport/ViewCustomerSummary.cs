using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using tlrsCartonManager.DAL.Models.GenericReport;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
    [Keyless]
    public partial class ViewCustomerSummary : IGenericReportDataItem
    {
        [Required]
        [StringLength(20)]
        [Column("Customer Code")]
        public string CustomerCode { get; set; }

        [Required]
        [StringLength(50)]
        [Column("Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        [Column("Address 1")]
        public string Address1 { get; set; }

        [StringLength(50)]
        [Column("Address 2")]
        public string Address2 { get; set; }

        [StringLength(50)]
        [Column("Address 3")]
        public string Address3 { get; set; }

        [StringLength(11)]
        [Column("Telephone 1")]
        public string Telephone1 { get; set; }

        [StringLength(11)]
        [Column("Telephone 2")]
        public string Telephone2 { get; set; }

        [StringLength(11)]
        [Column("Fax")]
        public string Fax { get; set; }

        [StringLength(50)]
        [Column("Zip Code")]
        public string ZipCode { get; set; }

        [StringLength(50)]
        [Column("Email")]
        public string Email { get; set; }

        [StringLength(50)]
        [Column("Contract No")]
        public string ContractNo { get; set; }

        [Column("Contract Start Date", TypeName = "datetime")]
        public DateTime? ContractStartDate { get; set; }

        [Column("Contract End Date", TypeName = "datetime")]
        public DateTime? ContractEndDate { get; set; }

        [StringLength(50)]
        [Column("Delivery Name")]
        public string DeliveryName { get; set; }

        [StringLength(50)]
        [Column("Delivery Address 1")]
        public string DeliveryAddress1 { get; set; }

        [StringLength(50)]
        [Column("Delivery Address 2")]
        public string DeliveryAddress2 { get; set; }

        [StringLength(50)]
        [Column("Delivery Address 3")]
        public string DeliveryAddress3 { get; set; }

        [StringLength(50)]
        [Column("Delivery Telephone 1")]
        public string DeliveryTelephone1 { get; set; }

        [StringLength(50)]
        [Column("Delivery Telephone 2")]
        public string DeliveryTelephone2 { get; set; }

        [StringLength(50)]
        [Column("Delivery Fax")]
        public string DeliveryFax { get; set; }

        [StringLength(50)]
        [Column("PickUp Name")]
        public string PickUpName { get; set; }

        [StringLength(50)]
        [Column("PickUp Address 1")]
        public string PickUpAddress1 { get; set; }

        [StringLength(50)]
        [Column("PickUp Address 2")]
        public string PickUpAddress2 { get; set; }

        [StringLength(50)]
        [Column("PickUp Address 3")]
        public string PickUpAddress3 { get; set; }

        [StringLength(50)]
        [Column("PickUp Telephone 1")]
        public string PickUpTelephone1 { get; set; }

        [StringLength(50)]
        [Column("PickUp Telephone 2")]
        public string PickUpTelephone2 { get; set; }

        [StringLength(50)]
        [Column("PickUp Fax")]
        public string PickUpFax { get; set; }

        [StringLength(50)]
        [Column("City")]
        public string City { get; set; }

        [StringLength(50)]
        [Column("Contact Name")]
        public string ContactName { get; set; }

        [StringLength(50)]
        [Column("Contact Address 1")]
        public string ContactAddress1 { get; set; }

        [StringLength(50)]
        [Column("Contact Address 2")]
        public string ContactAddress2 { get; set; }

        [StringLength(50)]
        [Column("Contact Address 3")]
        public string ContactAddress3 { get; set; }

        [StringLength(50)]
        [Column("Contact Telephone 1")]
        public string ContactTelephone1 { get; set; }

        [StringLength(50)]
        [Column("Contact Telephone 2")]
        public string ContactTelephone2 { get; set; }

        [StringLength(50)]
        [Column("Contact Fax")]
        public string ContactFax { get; set; }

        [StringLength(50)]
        [Column("Po No")]
        public string PoNo { get; set; }

        [StringLength(50)]
        [Column("Vat No")]
        public string VatNo { get; set; }

        [StringLength(50)]
        [Column("Svat No")]
        public string SvatNo { get; set; }

        [StringLength(50)]
        [Column("Billing Cycle")]
        public string BillingCycle { get; set; }

        [StringLength(50)]
        [Column("Route")]
        public string Route { get; set; }

        [Column("Is Separate Invoice")]
        public bool IsSeparateInvoice { get; set; }

        [StringLength(500)]
        [Column("Contact PersonInv")]
        public string ContactPersonInv { get; set; }

        [Column("Is Sub Invoice")]
        public bool IsSubInvoice { get; set; }

        [StringLength(50)]
        [Column("Service Provided")]
        public string ServiceProvided { get; set; }

        [Required]
        [StringLength(2)]
        [Column("Account Type")]
        public string AccountType { get; set; }

        [Required]
        [StringLength(20)]
        [Column("Main Customer Code")]
        public string MainCustomerCode { get; set; }

        public bool Active { get; set; }

        [Column("Last Invoice Generated Date")]
        public int? LastInvoiceGeneratedDate { get; set; }

        [StringLength(100)]
        [Column("Created User")]
        public string CreatedUser { get; set; }

        [StringLength(4000)]
        [Column("Created Date")]
        public string CreatedDate { get; set; }

        [Required]
        [StringLength(100)]
        [Column("Lu User")]
        public string LuUser { get; set; }

        [Column("Lu Date")]
        [StringLength(4000)]
        public string LuDate { get; set; }

        [Column("Deleted")]
        public bool Deleted { get; set; }

        public IList<KeyValuePair<string, string>> GetValues()
        {
            return new List<KeyValuePair<string, string>>()
            {
                    new KeyValuePair<string, string>("Customer Code", CustomerCode),
                    new KeyValuePair<string, string>("Name", Name),
                    new KeyValuePair<string, string>("Address 1", Address1),
                    new KeyValuePair<string, string>("Address 2", Address2),
                    new KeyValuePair<string, string>("Address 3", Address3),
                    new KeyValuePair<string, string>("Telephone 1", Telephone1),
                     new KeyValuePair<string, string>("Telephone2",Telephone2),
                     new KeyValuePair<string, string>("Fax",Fax),
                     new KeyValuePair<string, string>("Zip Code",ZipCode),
                     new KeyValuePair<string, string>("Email",Email),
                     new KeyValuePair<string, string>("Contract No",ContractNo),
                     new KeyValuePair<string, string>("Contract Start Date",ContractStartDate.ToString()),
                     new KeyValuePair<string, string>("Contract End Date",ContractEndDate.ToString()),
                     new KeyValuePair<string, string>("Delivery Name",DeliveryName),
                     new KeyValuePair<string, string>("Delivery Address 1",DeliveryAddress1),
                     new KeyValuePair<string, string>("Delivery Address 2",DeliveryAddress2),
                     new KeyValuePair<string, string>("Delivery Address 3",DeliveryAddress3),
                     new KeyValuePair<string, string>("Delivery Telephone 1",DeliveryTelephone1),
                     new KeyValuePair<string, string>("DeliveryTelephone 2",DeliveryTelephone2),
                     new KeyValuePair<string, string>("Delivery Fax",DeliveryFax),
                     new KeyValuePair<string, string>("PickUp Name",PickUpName),
                     new KeyValuePair<string, string>("PickUp Address 1",PickUpAddress1),
                     new KeyValuePair<string, string>("PickUp Address 2",PickUpAddress2),
                     new KeyValuePair<string, string>("PickUp Address 3",PickUpAddress3),
                     new KeyValuePair<string, string>("PickUp Telephone 1",PickUpTelephone1),
                     new KeyValuePair<string, string>("PickUp Telephone 2",PickUpTelephone2),
                     new KeyValuePair<string, string>("PickUp Fax",PickUpFax),
                     new KeyValuePair<string, string>("City",City),
                     new KeyValuePair<string, string>("Contact Name",ContactName),
                     new KeyValuePair<string, string>("Contact Address 1",ContactAddress1),
                     new KeyValuePair<string, string>("Contact Address 2",ContactAddress2),
                     new KeyValuePair<string, string>("Contact Address 3",ContactAddress3),
                     new KeyValuePair<string, string>("Contact Telephone 1",ContactTelephone1),
                     new KeyValuePair<string, string>("Contact Telephone 2",ContactTelephone2),
                     new KeyValuePair<string, string>("ContactFax",ContactFax),
                     new KeyValuePair<string, string>("Po No",PoNo),
                     new KeyValuePair<string, string>("Vat No",VatNo),
                     new KeyValuePair<string, string>("Svat No",SvatNo),
                     new KeyValuePair<string, string>("Billing Cycle",BillingCycle),
                     new KeyValuePair<string, string>("Route",Route),
                     new KeyValuePair<string, string>("Is Separate Invoice",IsSeparateInvoice.ToString()),
                     new KeyValuePair<string, string>("Contact Person Inv",ContactPersonInv),
                     new KeyValuePair<string, string>("Is Sub Invoice",IsSubInvoice.ToString()),
                     new KeyValuePair<string, string>("Service Provided",ServiceProvided),
                     new KeyValuePair<string, string>("Account Type",AccountType),
                     new KeyValuePair<string, string>("Main Customer Code",MainCustomerCode),
                     new KeyValuePair<string, string>("Active",Active.ToString()),
                     new KeyValuePair<string, string>("Last InvoiceGenerated Date",LastInvoiceGeneratedDate.ToString()),
                     new KeyValuePair<string, string>("Created User",CreatedUser),
                     new KeyValuePair<string, string>("Create dDate",CreatedDate),
                     new KeyValuePair<string, string>("Last Updated User",LuUser),
                     new KeyValuePair<string, string>("Last Updated Date",LuDate),
                     new KeyValuePair<string, string>("Deleted",Deleted.ToString())
            };
        }
    }
}
