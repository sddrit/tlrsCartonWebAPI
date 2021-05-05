using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
    public partial class MenuModel
    {
        public MenuModel()
        {
            MenuModelOptions = new HashSet<MenuModelOption>();
            MenuModelUserRoles = new HashSet<MenuModelUserRole>();
        }

        [Key]
        public int ModelCode { get; set; }
        [Required]
        [StringLength(50)]
        public string ModelName { get; set; }

        [InverseProperty(nameof(MenuModelOption.Model))]
        public virtual ICollection<MenuModelOption> MenuModelOptions { get; set; }
        [InverseProperty(nameof(MenuModelUserRole.Model))]
        public virtual ICollection<MenuModelUserRole> MenuModelUserRoles { get; set; }
    }
}
