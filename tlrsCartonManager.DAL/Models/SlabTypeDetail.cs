using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
    [Table("SlabTypeDetail")]
    public partial class SlabTypeDetail
    {
        [Key]
        [Column("trackingId")]
        public int TrackingId { get; set; }
        [Column("id")]
        public int? Id { get; set; }
        [Column("rowId")]
        public int? RowId { get; set; }
        [Column("fromSlab")]
        public int? FromSlab { get; set; }
        [Column("toSlab")]
        public int? ToSlab { get; set; }
        [Column("rate", TypeName = "decimal(18, 2)")]
        public decimal? Rate { get; set; }

        [ForeignKey(nameof(Id))]
        [InverseProperty(nameof(SlabTypeHeader.SlabTypeDetails))]
        public virtual SlabTypeHeader IdNavigation { get; set; }
    }
}
