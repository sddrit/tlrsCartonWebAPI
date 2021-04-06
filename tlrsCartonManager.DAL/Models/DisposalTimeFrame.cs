using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
    [Table("DisposalTimeFrame")]
    public partial class DisposalTimeFrame
    {
        public DisposalTimeFrame()
        {
            CartonStorages = new HashSet<CartonStorage>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("description")]
        [StringLength(50)]
        public string Description { get; set; }
        [Column("active")]
        public bool? Active { get; set; }
        [Column("deleeted")]
        public bool? Deleeted { get; set; }
        [Column("createdUser")]
        public int? CreatedUser { get; set; }
        [Column("createdDate", TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }

        [InverseProperty(nameof(CartonStorage.DisposalTimeFrameNavigation))]
        public virtual ICollection<CartonStorage> CartonStorages { get; set; }
    }
}
