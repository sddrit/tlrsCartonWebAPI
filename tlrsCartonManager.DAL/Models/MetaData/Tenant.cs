using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using tlrsCartonManager.DAL.Models.Base;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
    [Table("Tenant")]
    public partial class Tenant : ISoftDelete
    {
        public Tenant()
        {
          
        }

        [Key]
        [Column("code")]
        public string Code { get; set; }
        
        [Required]
        [Column("description")]
        [StringLength(50)]
        public string Description { get; set; }
        
        [Column("active")]
        public bool? Active { get; set; }
        
        [Column("deleted")]
        public bool Deleted { get; set; }

       
    }
}
