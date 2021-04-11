using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
    [Table("Status")]
    public partial class Status
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("description")]
        [StringLength(100)]
        public string Description { get; set; }
        [Column("active")]
        public bool? Active { get; set; }
        [Column("woType")]
        [StringLength(10)]
        public string WoType { get; set; }
        [Column("isInvoiceConfirmation")]
        public bool? IsInvoiceConfirmation { get; set; }
        [Column("isWorkOrderAdd")]
        public bool? IsWorkOrderAdd { get; set; }
        [Column("isWorkOrderEdit")]
        public bool? IsWorkOrderEdit { get; set; }
        [Column("isWorkOrderDelete")]
        public bool? IsWorkOrderDelete { get; set; }
        [Column("isWorkOrderPrint")]
        public bool? IsWorkOrderPrint { get; set; }
    }
}
