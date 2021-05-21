using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
    [Table("UserRole")]
    public partial class UserRole
    {
        [Key]
        [Column("userId")]
        public int UserId { get; set; }
        [Key]
        [Column("roleId")]
        public int RoleId { get; set; }
    }
    
}
