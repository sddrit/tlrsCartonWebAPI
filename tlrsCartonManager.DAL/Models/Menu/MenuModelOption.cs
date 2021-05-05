using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
    [Index(nameof(FormRightId), Name = "IX_MenuModelOptions")]
    public partial class MenuModelOption
    {
        [Key]
        [Column("ModelID")]
        public int ModelId { get; set; }
        [Key]
        [Column("FormRightID")]
        public int FormRightId { get; set; }

        [ForeignKey(nameof(FormRightId))]
        [InverseProperty(nameof(MenuRightFormName.MenuModelOptions))]
        public virtual MenuRightFormName FormRight { get; set; }
        [ForeignKey(nameof(ModelId))]
        [InverseProperty(nameof(MenuModel.MenuModelOptions))]
        public virtual MenuModel Model { get; set; }
    }
}
