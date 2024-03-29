﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
    [Table("MenuAction")]
    public partial class MenuRightFormName
    {
        public MenuRightFormName()
        {
            MenuModelOptions = new HashSet<MenuModelOption>();
            MenuModelOptionsUserRoles = new HashSet<MenuModelOptionsUserRole>();
        }

        [Key]
        [Column("id")]
        public int FormRightId { get; set; }
        [Required]
        [Column("name")]
        [StringLength(50)]
        public string FormRightName { get; set; }

        [InverseProperty(nameof(MenuModelOption.FormRight))]
        public virtual ICollection<MenuModelOption> MenuModelOptions { get; set; }
        [InverseProperty(nameof(MenuModelOptionsUserRole.FormRight))]
        public virtual ICollection<MenuModelOptionsUserRole> MenuModelOptionsUserRoles { get; set; }
    }
}
