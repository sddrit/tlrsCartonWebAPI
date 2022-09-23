using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
    [Keyless]
    public partial class ViewCustomerLoyality
    {
        [Required]
        [Column("customerCode")]
        [StringLength(20)]
        public string CustomerCode { get; set; }
        [Required]
        [Column("name")]
        [StringLength(50)]
        public string Name { get; set; }
        [Column("monthYear")]
        [StringLength(61)]
        public string MonthYear { get; set; }
        [Column("rentalQty", TypeName = "decimal(18, 2)")]
        public decimal RentalQty { get; set; }
        [Column("rentalAmount", TypeName = "decimal(18, 2)")]
        public decimal RentalAmount { get; set; }
        [Column("fromdate")]       
        public int FromDate { get; set; }
    }
}
