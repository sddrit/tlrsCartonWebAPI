using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
    [Table("ServiceCategory")]
    public partial class ServiceCategory
    {
        public ServiceCategory()
        {
            Customers = new HashSet<Customer>();
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
        public bool? Deleted { get; set; }

        [InverseProperty(nameof(Customer.ServiceProvidedNavigation))]
        public virtual ICollection<Customer> Customers { get; set; }
    }
}
