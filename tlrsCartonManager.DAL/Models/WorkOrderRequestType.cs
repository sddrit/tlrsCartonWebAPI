using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
    [Table("WorkOrderRequestType")]
    public partial class WorkOrderRequestType
    {
        [Key]
        [Column("typeCode")]
        [StringLength(50)]
        public string TypeCode { get; set; }
        [Column("requestTypeCode")]
        [StringLength(50)]
        public string RequestTypeCode { get; set; }
        [Column("active")]
        public bool? Active { get; set; }
        [Column("deleted")]
        public bool? Deleted { get; set; }
        [Column("createdUserId")]
        public int? CreatedUserId { get; set; }
        [Column("createdDate", TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column("luUserId")]
        public int? LuUserId { get; set; }
        [Column("luDate", TypeName = "datetime")]
        public DateTime? LuDate { get; set; }

        [ForeignKey(nameof(RequestTypeCode))]
        [InverseProperty(nameof(RequestType.WorkOrderRequestTypes))]
        public virtual RequestType RequestTypeCodeNavigation { get; set; }
    }
}
