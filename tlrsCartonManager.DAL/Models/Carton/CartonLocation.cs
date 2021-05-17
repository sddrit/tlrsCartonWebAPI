using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
    [Table("CartonLocation")]
    public partial class CartonLocation
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("cartonNo")]
        public int? CartonNo { get; set; }
        [Column("barCode")]
        [StringLength(50)]
        public string BarCode { get; set; }
        [Column("locationCode")]
        [StringLength(20)]
        public string LocationCode { get; set; }
        [Column("storageType")]
        [StringLength(50)]
        public string StorageType { get; set; }
        [Column("isFromMobile")]
        public bool? IsFromMobile { get; set; }
        [Column("scannedDate")]
        public long? ScannedDate { get; set; }
        [Column("customerId")]
        public int? CustomerId { get; set; }
        [Column("scanDateTime")]
        [StringLength(50)]
        public DateTime ScanDateTime { get; set; }
        [Column("remark")]
        [StringLength(500)]
        public string Remark { get; set; }
        [Column("correctedBarCode")]
        [StringLength(20)]
        public string CorrectedBarCode { get; set; }
        [Column("isVerified")]
        public bool? IsVerified { get; set; }
        [Column("createdUserId")]
        public string CreatedUserId { get; set; }
        [Column("createdDate", TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column("luUserId")]
        public int? LuUserId { get; set; }
        [Column("luDate", TypeName = "datetime")]
        public DateTime? LuDate { get; set; }

        [ForeignKey(nameof(CartonNo))]
        [InverseProperty(nameof(CartonStorage.CartonLocations))]
        public virtual CartonStorage CartonNoNavigation { get; set; }
    }
}
