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
    public partial class ViewCustomerSummary: IGenericReportDataItem
    {
        [Required]
        [StringLength(20)]
        public string CustomerCode { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(50)]
        public string Address1 { get; set; }
        [StringLength(50)]
        public string Address2 { get; set; }
        [StringLength(50)]
        public string Address3 { get; set; }
        [StringLength(11)]
        public string Telephone1 { get; set; }
        [StringLength(11)]
        public string Telephone2 { get; set; }
        [StringLength(11)]
        public string Fax { get; set; }
        [StringLength(50)]
        public string ZipCode { get; set; }
        [StringLength(50)]
        public string Email { get; set; }
        [StringLength(50)]
        public string ContractNo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ContractStartDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ContractEndDate { get; set; }
        [StringLength(50)]
        public string DeliveryName { get; set; }
        [StringLength(50)]
        public string DeliveryAddress1 { get; set; }
        [StringLength(50)]
        public string DeliveryAddress2 { get; set; }
        [StringLength(50)]
        public string DeliveryAddress3 { get; set; }
        [StringLength(50)]
        public string DeliveryTelephone1 { get; set; }
        [StringLength(50)]
        public string DeliveryTelephone2 { get; set; }
        [StringLength(50)]
        public string DeliveryFax { get; set; }
        [StringLength(50)]
        public string PickUpName { get; set; }
        [StringLength(50)]
        public string PickUpAddress1 { get; set; }
        [StringLength(50)]
        public string PickUpAddress2 { get; set; }
        [StringLength(50)]
        public string PickUpAddress3 { get; set; }
        [StringLength(50)]
        public string PickUpTelephone1 { get; set; }
        [StringLength(50)]
        public string PickUpTelephone2 { get; set; }
        [StringLength(50)]
        public string PickUpFax { get; set; }
        [StringLength(50)]
        public string City { get; set; }
        [StringLength(50)]
        public string ContactName { get; set; }
        [StringLength(50)]
        public string ContactAddress1 { get; set; }
        [StringLength(50)]
        public string ContactAddress2 { get; set; }
        [StringLength(50)]
        public string ContactAddress3 { get; set; }
        [StringLength(50)]
        public string ContactTelephone1 { get; set; }
        [StringLength(50)]
        public string ContactTelephone2 { get; set; }
        [StringLength(50)]
        public string ContactFax { get; set; }
        [StringLength(50)]
        public string PoNo { get; set; }
        [StringLength(50)]
        public string VatNo { get; set; }
        [StringLength(50)]
        public string SvatNo { get; set; }
        [StringLength(50)]
        public string BillingCycle { get; set; }
        [StringLength(50)]
        public string Route { get; set; }
        public bool IsSeparateInvoice { get; set; }
        [StringLength(500)]
        public string ContactPersonInv { get; set; }
        public bool IsSubInvoice { get; set; }
        [StringLength(50)]
        public string ServiceProvided { get; set; }
        [Required]
        [StringLength(2)]
        public string AccountType { get; set; }
        [Required]
        [StringLength(20)]
        public string MainCustomerCode { get; set; }
        public bool Active { get; set; }
        public int? LastInvoiceGeneratedDate { get; set; }
        [StringLength(100)]
        public string CreatedUser { get; set; }
        [StringLength(4000)]
        public string CreatedDate { get; set; }
        [Required]
        [StringLength(100)]
        public string LuUser { get; set; }
        [Column("luDate")]
        [StringLength(4000)]
        public string LuDate { get; set; }
        [Column("deleted")]
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
