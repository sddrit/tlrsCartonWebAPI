using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
    [Table("MenuRightAttachedUser")]
    public partial class MenuRightAttachedUser
    {
        public MenuRightAttachedUser()
        {
            MenuRightFormUsers = new HashSet<MenuRightFormUser>();
        }

        [Column("menuId")]
        public int MenuId { get; set; }
        [Required]
        [Column("menuName")]
        [StringLength(50)]
        public string MenuName { get; set; }
        [Required]
        [Column("menuDisplayString")]
        [StringLength(50)]
        public string MenuDisplayString { get; set; }
        [Column("menuRoute")]
        [StringLength(100)]
        public string MenuRoute { get; set; }
        [Column("menuLevel")]
        public int MenuLevel { get; set; }
        [Column("parantId")]
        public int ParantId { get; set; }
        [Column("divisionId")]
        public int DivisionId { get; set; }
        [Column("finalMenu")]
        public int? FinalMenu { get; set; }
        [Key]
        [Column("UserMenuID")]
        public int UserMenuId { get; set; }
        [Column("userRoleID")]
        public int? UserRoleId { get; set; }

        [InverseProperty(nameof(MenuRightFormUser.UserMenuTracking))]
        public virtual ICollection<MenuRightFormUser> MenuRightFormUsers { get; set; }
    }
}
