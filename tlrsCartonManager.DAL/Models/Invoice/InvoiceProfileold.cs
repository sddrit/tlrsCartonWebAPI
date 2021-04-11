using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
    [Table("InvoiceProfileold")]
    public partial class InvoiceProfileold
    {
        public InvoiceProfileold()
        {
            CutomerInvoiceProfiles = new HashSet<CutomerInvoiceProfile>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("description")]
        [StringLength(50)]
        public string Description { get; set; }
        [Column("active")]
        public int? Active { get; set; }
        [Column("deleted")]
        public int? Deleted { get; set; }

        [InverseProperty(nameof(CutomerInvoiceProfile.InvoiceProfile))]
        public virtual ICollection<CutomerInvoiceProfile> CutomerInvoiceProfiles { get; set; }
    }
}
