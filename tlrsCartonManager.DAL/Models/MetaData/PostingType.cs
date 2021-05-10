using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
    [Table("PostingType")]
    public partial class PostingType
    {
        [Key]
        [Column("code")]
        [StringLength(20)]
        public string Code { get; set; }
        [Required]
        [Column("description")]
        [StringLength(50)]
        public string Description { get; set; }
        [Required]
        [Column("active")]
        public bool? Active { get; set; }
        [Column("deleted")]
        public bool Deleted { get; set; }
    }
}
