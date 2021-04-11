using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
    [Table("User")]
    public partial class User
    {
        public User()
        {
            MenuRightUsers = new HashSet<MenuRightUser>();
            UserPasswords = new HashSet<UserPassword>();
        }

        [Key]
        [Column("userId")]
        public int UserId { get; set; }
        [Column("userName")]
        [StringLength(100)]
        public string UserName { get; set; }
        [Column("userFullName")]
        [StringLength(100)]
        public string UserFullName { get; set; }
        [Column("empId")]
        [StringLength(50)]
        public string EmpId { get; set; }
        [Column("appId")]
        public int? AppId { get; set; }
        [Column("lastLoginDate", TypeName = "datetime")]
        public DateTime? LastLoginDate { get; set; }
        [Column("passwordLockedDate", TypeName = "datetime")]
        public DateTime? PasswordLockedDate { get; set; }
        [Column("userRoleId")]
        public int UserRoleId { get; set; }
        [Column("active")]
        public bool? Active { get; set; }
        [Column("deleted")]
        public bool? Deleted { get; set; }

        [InverseProperty(nameof(MenuRightUser.User))]
        public virtual ICollection<MenuRightUser> MenuRightUsers { get; set; }
        [InverseProperty(nameof(UserPassword.User))]
        public virtual ICollection<UserPassword> UserPasswords { get; set; }
    }
}
