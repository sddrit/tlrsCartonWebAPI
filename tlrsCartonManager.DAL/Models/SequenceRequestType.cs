using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
    [Table("SequenceRequestType")]
    public partial class SequenceRequestType
    {
        [Key]
        [Column("typeCode")]
        [StringLength(50)]
        public string TypeCode { get; set; }
        [Column("requestTypeCode")]
        [StringLength(50)]
        public string RequestTypeCode { get; set; }
        [Column("createdUserId")]
        public int? CreatedUserId { get; set; }
        [Column("createdDate", TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
    }
}
