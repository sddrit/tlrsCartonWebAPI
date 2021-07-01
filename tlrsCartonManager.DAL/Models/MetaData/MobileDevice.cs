using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using tlrsCartonManager.DAL.Models.Base;

#nullable disable

namespace tlrsCartonManager.DAL.Models.MetaData
{
    [Table("MobileDevice")]
    public partial class MobileDevice:ISoftDelete
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("code")]
        [StringLength(20)]
        public string Code { get; set; }
        
        [Column("description")]
        [StringLength(50)]
        public string Description { get; set; }
        
        [Column("active")]
        public bool? Active { get; set; }
        
        [Column("deleted")]
        public bool Deleted { get; set; }
        
    }
}
