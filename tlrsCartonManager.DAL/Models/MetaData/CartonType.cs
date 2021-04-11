using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
    [Table("CartonType")]
    public partial class CartonType
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("type")]
        [StringLength(50)]
        public string Type { get; set; }
        [Column("description")]
        [StringLength(100)]
        public string Description { get; set; }
        [Column("size")]
        [StringLength(100)]
        public string Size { get; set; }
        [Column("active")]
        public bool? Active { get; set; }
        [Column("deleted")]
        public bool? Deleted { get; set; }
        [Column("createdUser")]
        public int? CreatedUser { get; set; }
        [Column("createdDate", TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column("luUser")]
        public int? LuUser { get; set; }
        [Column("luDate", TypeName = "datetime")]
        public DateTime? LuDate { get; set; }
    }
}
