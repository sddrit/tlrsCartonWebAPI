using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using tlrsCartonManager.DAL.Models.Base;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
    [Table("PriorityLevel")]
    public partial class PriorityLevel :  ISoftDelete
    {       

        [Key]
        [Column("id")]
        public int Id { get; set; }     
        [Column("description")]
        [StringLength(100)]
        public string Description { get; set; }
        
        [Column("active")]
        public bool? Active { get; set; }

        [Column("deleted")]
        [DefaultValue(false)]
        public bool Deleted { get; set; } 

        [Column("createdUserId")]
        public int CreatedUserId { get; set; }
      
       
    }
}
