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
       
        public string RequestNo { get; set; }
        [Required]
        [StringLength(20)]
        public string CustomerCode { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(50)]
        public string Address { get; set; }
        public int? DeliveryDate { get; set; }
        [StringLength(50)]
        public string OrderReceivedBy { get; set; }
        [StringLength(50)]
        public string CustomerReference { get; set; }
        [Required]
        [StringLength(50)]
        public string AuthorizedOfficer { get; set; }
        public int? NoOfCartons { get; set; }
        [StringLength(10)]
        public string RequestType { get; set; }
        [StringLength(100)]
        public string Status { get; set; }
        [StringLength(100)]
        public string StorageType { get; set; }
        [StringLength(20)]
        public string WoType { get; set; }
        [Column("createdUser")]
        [StringLength(100)]
        public string CreatedUser { get; set; }
        [Column("createdDate")]
        [StringLength(4000)]
        public string CreatedDate { get; set; }
        [Column("createdDateTime")]
        [StringLength(4000)]
        public string CreatedDateTime { get; set; }
        [Required]
        [Column("luUser")]
        [StringLength(100)]
        public string LuUser { get; set; }
        [Column("luDate")]
        [StringLength(4000)]
        public string LuDate { get; set; }
        [Column("luDateTime")]
        [StringLength(4000)]
        public string LuDateTime { get; set; }
        [StringLength(50)]
        public string DeliveryRoute { get; set; }
        [StringLength(50)]
        public string ServiceType { get; set; }
        [Column("docketNo")]
        [StringLength(200)]
        public string DocketNo { get; set; }

        [Column("Department")]        
        public string Department { get; set; }

        [Column("PoNo")]     
        public string PoNo { get; set; }

        [Column("ContactPerson")]    
        public string ContactPerson { get; set; }

        [Column("ContactNo")]
        public string ContactNo { get; set; }

        [Column("DocketType")]
        public string DocketType { get; set; }

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
                 new KeyValuePair<string, string>("Storage Type",StorageType),
                 new KeyValuePair<string, string>("Created User",CreatedUser),
                 new KeyValuePair<string, string>("Created Date",CreatedDate),
                 new KeyValuePair<string, string>("Created Date Time",CreatedDateTime),
                 new KeyValuePair<string, string>("Last Updated User",LuUser),
                 new KeyValuePair<string, string>("Last Updated Date",LuDate),
                 new KeyValuePair<string, string>("Last Updated Date Time",LuDateTime),
                 new KeyValuePair<string, string>("Delivery Route",DeliveryRoute),
                 new KeyValuePair<string, string>("Docket No",DocketNo)
            };
        }
    }

   
}
