using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
    [Table("CartonStorage")]
    [Index(nameof(AlternativeCartonNo), Name = "indx_alternativeNocartonSearch")]
    [Index(nameof(CartonType), Name = "indx_cartonSearch")]
    [Index(nameof(Status), Name = "indx_cartonStatusCartonSearch")]
    [Index(nameof(CustomerId), Name = "indx_cutomerCartonSearch")]
    public partial class CartonStorage
    {
        public CartonStorage()
        {
            CartonLocations = new HashSet<CartonLocation>();
        }

        [Key]
        [Column("cartonNo")]
        public int CartonNo { get; set; }
        [Column("status")]
        [StringLength(50)]
        public string Status { get; set; }
        [Column("lastRequestNo")]
        [StringLength(20)]
        public string LastRequestNo { get; set; }
        [Column("customerId")]
        public int? CustomerId { get; set; }
        [Column("locationCode")]
        [StringLength(20)]
        public string LocationCode { get; set; }
        [Column("lastTransactionDate")]
        public int? LastTransactionDate { get; set; }
        [Column("disposalDate")]
        public int? DisposalDate { get; set; }
        [Column("disposalTimeFrame")]
        public int? DisposalTimeFrame { get; set; }
        [Column("actualDisposalDate")]
        public int? ActualDisposalDate { get; set; }
        [Column("statusInOut")]
        public int? StatusInOut { get; set; }
        [Column("statusInOutDate", TypeName = "datetime")]
        public DateTime? StatusInOutDate { get; set; }
        [Column("alternativeCartonNo")]
        [StringLength(50)]
        public string AlternativeCartonNo { get; set; }
        [Column("cartonType")]
        public int? CartonType { get; set; }
        [Column("lastUpdateDate")]
        public int? LastUpdateDate { get; set; }
        [Column("firstInDate")]
        public int? FirstInDate { get; set; }
        [Column("lastConfirmedStatus")]
        [StringLength(50)]
        public string LastConfirmedStatus { get; set; }
        [Column("lastConfirmeedRequestNo")]
        [StringLength(20)]
        public string LastConfirmeedRequestNo { get; set; }
        [Column("lastOwnershipChangedDate", TypeName = "datetime")]
        public DateTime? LastOwnershipChangedDate { get; set; }
        [Column("lastDeliveryRoute")]
        public int? LastDeliveryRoute { get; set; }
        [Column("createdUser")]
        [StringLength(50)]
        public string CreatedUser { get; set; }
        [Column("createdDate", TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column("luUser")]
        [StringLength(50)]
        public string LuUser { get; set; }
        [Column("luDate", TypeName = "datetime")]
        public DateTime? LuDate { get; set; }

        [ForeignKey(nameof(CartonType))]
        [InverseProperty("CartonStorages")]
        public virtual CartonType CartonTypeNavigation { get; set; }
        [ForeignKey(nameof(DisposalTimeFrame))]
        [InverseProperty("CartonStorages")]
        public virtual DisposalTimeFrame DisposalTimeFrameNavigation { get; set; }
        [ForeignKey(nameof(LastDeliveryRoute))]
        [InverseProperty(nameof(Route.CartonStorages))]
        public virtual Route LastDeliveryRouteNavigation { get; set; }
        [InverseProperty(nameof(CartonLocation.CartonNoNavigation))]
        public virtual ICollection<CartonLocation> CartonLocations { get; set; }
    }
}
