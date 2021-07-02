using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using tlrsCartonManager.DAL.Models.Base;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
    [Table("Route")]
    public partial class Route : ISoftDelete
    {
        public Route()
        {
            CartonStorages = new HashSet<CartonStorage>();
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

        [InverseProperty(nameof(CartonStorage.LastDeliveryRouteNavigation))]
        public virtual ICollection<CartonStorage> CartonStorages { get; set; }
        [InverseProperty(nameof(Customer.RouteNavigation))]
        public virtual ICollection<Customer> Customers { get; set; }
    }
}
