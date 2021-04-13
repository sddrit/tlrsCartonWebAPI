using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
    [Table("InvoiceSlabCalculationProcessType")]
    public partial class InvoiceSlabCalculationProcessType
    {
        [Key]
        [Column("CakID")]
        public int CakId { get; set; }
        [Required]
        [StringLength(50)]
        public string CalcDesc { get; set; }
    }
}
