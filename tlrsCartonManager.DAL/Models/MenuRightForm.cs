using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
    [Table("MenuRightForm")]
    public partial class MenuRightForm
    {
        [Key]
        [Column("trackingId")]
        public int TrackingId { get; set; }
        [Column("menuId")]
        public int MenuId { get; set; }
        [Column("formRight")]
        public int FormRight { get; set; }

        [ForeignKey(nameof(MenuId))]
        [InverseProperty(nameof(MenuRight.MenuRightForms))]
        public virtual MenuRight Menu { get; set; }
    }
}
