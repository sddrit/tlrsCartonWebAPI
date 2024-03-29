﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using tlrsCartonManager.DAL.Models.Base;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
    [Table("Department")]
    public partial class Department : ISoftDelete
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("description")]
        [StringLength(100)]
        public string Description { get; set; }
        [Column("active")]
        public bool Active { get; set; }
        [Column("deleted")]
        public bool Deleted { get; set; }
        [Column("createdUserd")]
        public int CreatedUserd { get; set; }
       
    }
}
