﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using tlrsCartonManager.DAL.Models.Base;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
    [Table("Module")]
    public partial class Module:ISoftDelete
    {
        public Module()
        {
            ModuleSubs = new HashSet<ModuleSub>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("name")]
        [StringLength(100)]
        public string Name { get; set; }
        [Column("createdUserId")]
        public int CreatedUserId { get; set; }      

        //[InverseProperty(nameof(ModuleSub.Module))]
        public virtual ICollection<ModuleSub> ModuleSubs { get; set; }
        public bool Deleted { get; set; }
        public bool Active { get; set; }
    }
}
