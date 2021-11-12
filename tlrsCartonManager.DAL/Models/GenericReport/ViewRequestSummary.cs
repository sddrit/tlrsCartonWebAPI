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
    public partial class ViewRequestSummary : IGenericReportDataItem
    {
        [Required]
        [StringLength(20)]
        [Column("Request No")]
        public string RequestNo { get; set; }

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
        public string Address { get; set; }

        [Column("Delivery Date")]
        public int? DeliveryDate { get; set; }

        [StringLength(50)]
        [Column("Order Received By")]
        public string OrderReceivedBy { get; set; }

        [StringLength(50)]
        [Column("Customer Reference")]
        public string CustomerReference { get; set; }

        [Required]
        [StringLength(50)]
        [Column("Authorized Officer")]
        public string AuthorizedOfficer { get; set; }

        [Column("No Of Cartons")]
        public int? NoOfCartons { get; set; }

        [StringLength(10)]
        [Column("Request Type")]
        public string RequestType { get; set; }

        [StringLength(100)]
        public string Status { get; set; }

        [StringLength(100)]
        [Column("Storage Type")]
        public string StorageType { get; set; }

        [StringLength(20)]
        [Column("Wo Type")]
        public string WoType { get; set; }

        [Column("Created User")]
        [StringLength(100)]
        public string CreatedUser { get; set; }

        [Column("Created Date")]
        [StringLength(4000)]
        public string CreatedDate { get; set; }

        [Column("Created Date Time")]
        [StringLength(4000)]
        public string CreatedDateTime { get; set; }

        [Required]
        [Column("Lu User")]
        [StringLength(100)]
        public string LuUser { get; set; }

        [Column("Lu Date")]
        [StringLength(4000)]
        public string LuDate { get; set; }

        [Column("Lu Date Time")]
        [StringLength(4000)]
        public string LuDateTime { get; set; }

        [StringLength(50)]
        [Column("Delivery Route")]
        public string DeliveryRoute { get; set; }

        [StringLength(50)]
        [Column("Service Type")]
        public string ServiceType { get; set; }

        [Column("Docket No")]
        [StringLength(200)]
        public string DocketNo { get; set; }

        [Column("Department")]
        public string Department { get; set; }

        [Column("Po No")]
        public string PoNo { get; set; }

        [Column("Contact Person")]
        public string ContactPerson { get; set; }

        [Column("Contact No")]
        public string ContactNo { get; set; }

        [Column("Docket Type")]
        public string DocketType { get; set; }

        [Column("Is PrintAlternative No")]
        public bool IsPrintAlternativeNo { get; set; }

        [Column("Route")]
        public string Route { get; set; }

        [Column("Last Confirmed Date")]
        public int? LastConfirmedDate { get; set; }

        public IList<KeyValuePair<string, string>> GetValues()
        {
            return new List<KeyValuePair<string, string>>()
            {
                 new KeyValuePair<string, string>("Request No", RequestNo.ToString()),
                 new KeyValuePair<string, string>("Customer Code",CustomerCode),
                 new KeyValuePair<string, string>("Name", Name),
                 new KeyValuePair<string, string>("Address",Address),
                 new KeyValuePair<string, string>("Delivery Date", DeliveryDate.ToString()),
                 new KeyValuePair<string, string>("Order Received By",OrderReceivedBy),
                 new KeyValuePair<string, string>("Customer Reference",CustomerReference),
                 new KeyValuePair<string, string>("Authorized Officer",AuthorizedOfficer),
                 new KeyValuePair<string, string>("No Of Cartons",NoOfCartons.ToString()),
                 new KeyValuePair<string, string>("Request Type",RequestType),
                 new KeyValuePair<string, string>("Status",Status),
                 new KeyValuePair<string, string>("Storage Type",StorageType),
                 new KeyValuePair<string, string>("Wo Type",WoType),               
                 new KeyValuePair<string, string>("Created User",CreatedUser),
                 new KeyValuePair<string, string>("Created Date",CreatedDate),
                 new KeyValuePair<string, string>("Created Date Time",CreatedDateTime),
                 new KeyValuePair<string, string>("Last Updated User",LuUser),
                 new KeyValuePair<string, string>("Last Updated Date",LuDate),
                 new KeyValuePair<string, string>("Last Updated Date Time",LuDateTime),
                 new KeyValuePair<string, string>("Delivery Route",DeliveryRoute),
                 new KeyValuePair<string, string>("Docket No",DocketNo),
                 new KeyValuePair<string, string>("Delivery Location",Department)
            };
        }
    }


}
