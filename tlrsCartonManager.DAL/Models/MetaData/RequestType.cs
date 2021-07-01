using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using tlrsCartonManager.DAL.Models.Base;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
    [Table("RequestType")]
    public partial class RequestType:ISoftDelete
    {
        //public RequestType()
        //{
        //    WorkOrderRequestTypes = new HashSet<WorkOrderRequestType>();
        //}

       
        [Column("typeCode")]
        [StringLength(50)]
        public string TypeCode { get; set; }

        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("active")]
        public bool? Active { get; set; }

        [Column("deleted")]
        public bool Deleted { get; set; }

        [Column("createdUserId")]
        public int CreatedUserId { get; set; }

        [Column("luUserId")]
        public int LuUserId { get; set; }

        //[InverseProperty(nameof(WorkOrderRequestType.RequestTypeCodeNavigation))]
        //public virtual ICollection<WorkOrderRequestType> WorkOrderRequestTypes { get; set; }
    }
}
