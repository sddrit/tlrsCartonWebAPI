using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
    [Table("Menu")]
    public partial class MenuModel
    {
        public MenuModel()
        {
            MenuModelOptions = new HashSet<MenuModelOption>();
            MenuModelUserRoles = new HashSet<MenuModelUserRole>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [InverseProperty(nameof(MenuModelOption.Model))]
        public virtual ICollection<MenuModelOption> MenuModelOptions { get; set; }
        [InverseProperty(nameof(MenuModelUserRole.Model))]
        public virtual ICollection<MenuModelUserRole> MenuModelUserRoles { get; set; }
    }
}
