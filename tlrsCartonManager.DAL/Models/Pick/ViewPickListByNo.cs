using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
    [Keyless]
    public partial class ViewPickListByNo
    {
        [Column("trackingId")]
        public long TrackingId { get; set; }

        [Required]
        [Column("pickListNo")]
        [StringLength(50)]
        public string PickListNo { get; set; }

        [Column("cartonNo")]
        public int CartonNo { get; set; }

        [Required]
        [Column("barcode")]
        [StringLength(50)]
        public string Barcode { get; set; }

        [Column("locationCode")]
        [StringLength(20)]
        public string LocationCode { get; set; }

        [Column("wareHouseCode")]
        [StringLength(20)]
        public string WareHouseCode { get; set; }

        [Column("lastSentDeviceId")]
        [StringLength(50)]
        public string LastSentDeviceId { get; set; }

        [Column("assignedUserId")]
        public int? AssignedUserId { get; set; }

        [Column("requestNo")]
        [StringLength(20)]
        public string RequestNo { get; set; }

        [Column("pickedUserId")]
        public int PickedUserId { get; set; }

        [Column("isPicked")]
        public bool IsPicked { get; set; }

        [Column("pickDate")]
        public long? PickDate { get; set; }
        [Column("status")]
        public string Status { get; set; }

        [Column("deleted")]
        public bool? Deleted { get; set; }

        [Column("createdUserId")]
        public int CreatedUserId { get; set; }

        [Column("createdDate", TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }

        [Column("luUserId")]
        public int? LuUserId { get; set; }

        [Column("luDate", TypeName = "datetime")]
        public DateTime? LuDate { get; set; }

        [Column("pickedUserName")]
        [StringLength(100)]
        public string PickedUserName { get; set; }

        [Column("assignedUserName")]
        [StringLength(100)]
        public string AssignedUserName { get; set; }

        [Column("lastScannedDate")]
        public int? LastScannedDate { get; set; }

        [Column("requestType")]
        public string RequestType { get; set; }

        [Column("printed")]
        public bool Printed { get; set; }

    }
}
