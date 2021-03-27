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
        [Column("trackingId")]
        public int TrackingId { get; set; }
        [Column("serviceDescription")]
        [StringLength(50)]
        public string ServiceDescription { get; set; }
        [Column("status")]
        public int? Status { get; set; }

        [InverseProperty(nameof(Customer.ServiceProvidedNavigation))]
        public virtual ICollection<Customer> Customers { get; set; }
    }
}
