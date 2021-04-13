using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
    [Table("InvoiceProfile")]
   

        public partial class InvoiceProfile
        {
            public InvoiceProfile()
            {
                InvoiceSlabTypeHeaders = new HashSet<InvoiceSlabTypeHeader>();
            }

            [Key]
            [Column("profileId")]
            public int ProfileId { get; set; }
            [Required]
            [Column("profileDesc")]
            [StringLength(50)]
            public string ProfileDesc { get; set; }
            [Column("profileType")]
            public int ProfileType { get; set; }
            [Column("active")]
            public int Active { get; set; }
            [Column("deleted")]
            public int Deleted { get; set; }

            [InverseProperty(nameof(InvoiceSlabTypeHeader.InvoiceProfile))]
            public virtual ICollection<InvoiceSlabTypeHeader> InvoiceSlabTypeHeaders { get; set; }
        }
    }

