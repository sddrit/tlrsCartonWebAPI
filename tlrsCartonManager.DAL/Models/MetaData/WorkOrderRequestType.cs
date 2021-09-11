using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using tlrsCartonManager.DAL.Models.Base;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
    [Table("WorkOrderRequestType")]
    public partial class WorkOrderRequestType:ISoftDelete
    {
        [Key]
        public int Id { get; set; }

        [Column("typeCode")]
        [StringLength(50)]
        public string TypeCode { get; set; }
        
        [Column("requestTypeCode")]
        [StringLength(50)]
        public string RequestTypeCode { get; set; }

        [Column("description")]
        [StringLength(50)]
        public string Description { get; set; }

        [Column("active")]
        public bool? Active { get; set; }
        
        [Column("deleted")]
        public bool Deleted { get; set; }
       
        [Column("createdUserId")]
        public int CreatedUserId { get; set; }
      
        [Column("luUserId")]
        public int LuUserId { get; set; }

        [Column("showInventoryReport")]
        public bool? ShowInventoryReport { get; set; }
        //[ForeignKey(nameof(RequestTypeCode))]
        //[InverseProperty(nameof(RequestType.WorkOrderRequestTypes))]
        //public virtual RequestType RequestTypeCodeNavigation { get; set; }
    }
}
