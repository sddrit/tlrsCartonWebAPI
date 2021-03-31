using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
  
    public partial class CustomerSubAccountList
    {
        [Key]
        [Column("trackingId")]
        public int TrackingId { get; set; }

        [Column("customerId")]
        public int CustomerId { get; set; }
        [Required]
        [Column("name")]
        public string Name { get; set; }

        [Column("address1")]
        [StringLength(50)]
        public string Address1 { get; set; }

    }
}
