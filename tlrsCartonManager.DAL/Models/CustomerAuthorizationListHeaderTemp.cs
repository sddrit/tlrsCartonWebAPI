using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
    [Table("CustomerAuthorizationListHeaderTemp")]
    public partial class CustomerAuthorizationListHeaderTemp
    {
        [Key]
        [Column("trackingId")]
        public int TrackingId { get; set; }
        [Required]
        [Column("customerId")]
        [StringLength(50)]
        public string CustomerId { get; set; }
        [Required]
        [Column("name")]
        [StringLength(50)]
        public string Name { get; set; }
        [Column("department")]
        [StringLength(50)]
        public string Department { get; set; }
        [Column("designation")]
        [StringLength(50)]
        public string Designation { get; set; }
        [Column("email")]
        [StringLength(50)]
        public string Email { get; set; }
        [Column("active")]
        [StringLength(50)]
        public string Active { get; set; }
        [Required]
        [Column("createdUser")]
        [StringLength(50)]
        public string CreatedUser { get; set; }
        [Column("createdDate", TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }
        [Column("luUser")]
        [StringLength(50)]
        public string LuUser { get; set; }
        [Column("luDate", TypeName = "datetime")]
        public DateTime? LuDate { get; set; }
        [Column("deleted")]
        [StringLength(50)]
        public string Deleted { get; set; }
        [Column("lineNo")]
        [StringLength(50)]
        public string LineNo { get; set; }
    }
}
