using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
    [Table("MenuRight")]
    public partial class MenuRight
    {
        public MenuRight()
        {
            MenuRightForms = new HashSet<MenuRightForm>();
            MenuRightUsers = new HashSet<MenuRightUser>();
        }

        [Key]
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

        [InverseProperty(nameof(MenuRightForm.Menu))]
        public virtual ICollection<MenuRightForm> MenuRightForms { get; set; }
        [InverseProperty(nameof(MenuRightUser.Menu))]
        public virtual ICollection<MenuRightUser> MenuRightUsers { get; set; }
    }
}
