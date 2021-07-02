using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using tlrsCartonManager.DAL.Models.Base;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
    [Table("DisposalTimeFrame")]
    public partial class DisposalTimeFrame : ISoftDelete
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

        [Column("deleted")]
        public bool Deleted { get; set; }
        
        [Column("createdUserId")]
        public int CreatedUserId { get; set; }

        [InverseProperty(nameof(CartonStorage.DisposalTimeFrameNavigation))]
        public virtual ICollection<CartonStorage> CartonStorages { get; set; }
    }
}
