using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
    [Keyless]
    public partial class ViewCartonsInLocation
    {
        public int CartonNo { get; set; }
        [StringLength(50)]
        public string AlternativeCartonNo { get; set; }
        [Required]
        [StringLength(50)]
        public string Status { get; set; }
        [Required]
        [StringLength(20)]
        public string LastRequestNo { get; set; }
        [StringLength(4000)]
        public string LastTransactionDate { get; set; }
        [Required]
        [StringLength(20)]
        public string CustomerCode { get; set; }
        [Column("customerId")]
        public int CustomerId { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(20)]
        public string LocationCode { get; set; }
        [StringLength(4000)]
        public string DisposalDate { get; set; }
    }
}
