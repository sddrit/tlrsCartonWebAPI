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
        [Column("userRoleID")]
        public int UserRoleId { get; set; }
        [Required]
        [Column("userRoleName")]
        [StringLength(20)]
        public string UserRoleName { get; set; }
        [Column("userRoleDivision")]
        public int UserRoleDivision { get; set; }
    }
}
