using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
    [Keyless]
    public partial class ViewDisposalDatesOfCustomer
    {
        [Column("cartonNo")]
        public int CartonNo { get; set; }
        [Required]
        [Column("customerCode")]
        [StringLength(20)]
        public string CustomerCode { get; set; }
        [Required]
        [Column("name")]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(50)]
        public string Status { get; set; }
        [Required]
        [StringLength(50)]
        public string StatusConfirmed { get; set; }
        [Column("disposalDate")]
        public int? DisposalDate { get; set; }
        [Column("disposalTimeFrame")]
        public int? DisposalTimeFrame { get; set; }
        [Column("firstInDate")]
        public int FirstInDate { get; set; }
        [Column("lastTransactionDate")]
        public int LastTransactionDate { get; set; }
    }
}
