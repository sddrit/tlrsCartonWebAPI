using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
    [Table("BillingCycle")]
    public partial class BillingCycle
    {
        public BillingCycle()
        {
            Customers = new HashSet<Customer>();
        }

        [Key]
        [Column("trackingId")]
        public int TrackingId { get; set; }
        [Required]
        [Column("billingCycleDescription")]
        [StringLength(50)]
        public string BillingCycleDescription { get; set; }
        [Column("status")]
        public int? Status { get; set; }

        [InverseProperty(nameof(Customer.BillingCycleNavigation))]
        public virtual ICollection<Customer> Customers { get; set; }
    }
}
