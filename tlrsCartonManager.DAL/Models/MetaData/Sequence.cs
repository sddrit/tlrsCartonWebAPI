using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
    [Table("Sequence")]
    public partial class Sequence
    {
        [Key]
        [Column("sequenceType")]
        [StringLength(10)]
        public string SequenceType { get; set; }
        [Column("lastNo")]
        public int? LastNo { get; set; }
        [Column("currentSuffix")]
        [StringLength(20)]
        public string CurrentSuffix { get; set; }
        [Column("luDate", TypeName = "datetime")]
        public DateTime? LuDate { get; set; }
        [Column("requestTypeCode")]
        [StringLength(50)]
        public string RequestTypeCode { get; set; }

        [Column("active")]
        public bool Active { get; set; }
    }
}
