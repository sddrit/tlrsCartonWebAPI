using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
    [Table("RequestHeader")]
    public partial class RequestHeader
    {
        public RequestHeader()
        {
            RequestDetails = new HashSet<RequestDetail>();
        }

        [Key]
        [Column("requestNo")]
        [StringLength(20)]
        public string RequestNo { get; set; }

        [Column("trackingId")]
        public long TrackingId { get; set; }
        
        [Column("customerId")]
        public int? CustomerId { get; set; }
        [Column("priority")]
        public int? Priority { get; set; }
        [Column("deliveryDate")]
        public int? DeliveryDate { get; set; }
        [Column("orderReceivedBy")]
        public int? OrderReceivedBy { get; set; }
        [Column("remark")]
        [StringLength(200)]
        public string Remarks { get; set; }
        [Column("customerReference")]
        [StringLength(50)]
        public string CustomerReference { get; set; }
        [Column("authorizedOfficer")]
        [StringLength(50)]
        public int AuthorizedOfficerId { get; set; }
        [Column("noOfCartons")]
        public int? CartonCount { get; set; }
        
        [Column("requestType")]
        [StringLength(10)]
        public string RequestType { get; set; }
        [Column("status")]
        public int? Status { get; set; }
        [Column("cartonType")]
        public int? StorageType { get; set; }
        [Column("woType")]
        [StringLength(20)]
        public string WorkOrderType { get; set; }
        [Column("deviceId")]
        [StringLength(50)]
        public string DeviceId { get; set; }
        [Column("mobileRequestNo")]
        [StringLength(20)]
        public string MobileRequestNo { get; set; }
        [Column("contactPersonName")]
        [StringLength(200)]
        public string ContactPersonName { get; set; }
        [Column("deliveryLocation")]
        [StringLength(200)]
        public string DeliveryLocation { get; set; }
        [Column("deliveryRoute")]
        [StringLength(100)]
        public string DeliveryRoute { get; set; }
        [Column("reminder1")]
        public string Reminder1 { get; set; }
        [Column("reminder2")]
        public string Reminder2 { get; set; }
        [Column("reminder3")]
        public string Reminder3 { get; set; }
        [Column("createdUserId")]
        public int? CreatedUserId { get; set; }
        [Column("createdDate", TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column("luUser")]
        public int? LuUser { get; set; }
        [Column("luDate", TypeName = "datetime")]
        public DateTime? LuDate { get; set; }
        [Column("deliveryRouteId")]
        public int? DeliveryRouteId { get; set; }
        [Column("deleted")]
        public bool? Deleted { get; set; }
        [Column("serviceTypeId")]
        [StringLength(20)]
        public int ServiceTypeId { get; set; }
        [Column("docketNo")]
        [StringLength(20)]
        public string DocketNo { get; set; }

        [InverseProperty(nameof(RequestDetail.Request))]
        public virtual ICollection<RequestDetail> RequestDetails { get; set; }
    }
    public class RequestOriginalDocket
    {
        [Key]
        [Required]
        [Column("requestNo")]
        [StringLength(20)]
        public string RequestNo { get; set; }
        [Column("docketNo")]
        [StringLength(20)]
        public string DocketNo { get; set; }
       

    }
    public class OriginalDocketSearch
    {
        public string RequestNo { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string WoType { get; set; }
        public int NoOfCartons { get; set; }
        public int DeliveryDate { get; set; }
        public string DocketNo { get; set; }
    }
}
