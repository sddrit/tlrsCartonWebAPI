using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace tlrsCartonManager.DAL.Models.Ownership
{
    [Table("CartonOwnerShip")]
    public partial class CartonOwnerShip
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("cartonNo")]
        public int CartonNo { get; set; }
        [Column("fromCustomerCode")]
        [StringLength(20)]
        public string FromCustomerCode { get; set; }
        [Column("toCustomerCode")]
        [StringLength(20)]
        public string ToCustomerCode { get; set; }
        [Column("ownershipChangedDate", TypeName = "datetime")]
        public DateTime? OwnershipChangedDate { get; set; }
        [Column("ownershipChangedBy")]
        [StringLength(50)]
        public string OwnershipChangedBy { get; set; }
    }
}
