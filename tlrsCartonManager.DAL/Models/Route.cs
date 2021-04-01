using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
    [Table("Route")]
    public partial class Route
    {
        public Route()
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
        [Column("status")]
        public int? Status { get; set; }

        [InverseProperty(nameof(Customer.RouteNavigation))]
        public virtual ICollection<Customer> Customers { get; set; }
    }
}
