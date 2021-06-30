using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using tlrsCartonManager.DAL.Models.Base;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
    [Table("BillingCycle")]
    public partial class BillingCycle: ISoftDelete
    {
        public BillingCycle()
        {
            Customers = new HashSet<Customer>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        
        [Required]
        [Column("description")]
        [StringLength(50)]
        public string Description { get; set; }
        
        [Column("active")]
        public bool? Active { get; set; }
        
        [Column("deleted")]
        public bool Deleted { get; set; }

        [InverseProperty(nameof(Customer.BillingCycleNavigation))]
        public virtual ICollection<Customer> Customers { get; set; }
    }
}
