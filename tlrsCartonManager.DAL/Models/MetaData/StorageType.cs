﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using tlrsCartonManager.DAL.Models.Base;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
    [Table("StorageType")]
    public partial class StorageType :  ISoftDelete
    {
        public StorageType()
        {
            CartonStorages = new HashSet<CartonStorage>();
        }

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
        [DefaultValue(false)]
        public bool Deleted { get; set; } 

        [Column("createdUser")]
        public int CreatedUser { get; set; }
      
        [Column("luUser")]
        public int LuUser { get; set; }

        [Column("category")]
        public int? Category { get; set; }

        [InverseProperty(nameof(CartonStorage.CartonTypeNavigation))]
        public virtual ICollection<CartonStorage> CartonStorages { get; set; }
    }
}
