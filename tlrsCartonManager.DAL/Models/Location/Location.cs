using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
    [Table("Location")]
    public partial class Location
    {
        [Key]
        [Column("code")]
        [StringLength(20)]       
        public string Code { get; set; }

        [Column("id")]
        public int Id { get; set; }


        [Required]
        [Column("name")]
        [StringLength(200)]
        public string Name { get; set; }
        [Required]
        [Column("active")]
        public bool? Active { get; set; }
        [Column("rms1Location")]
        [StringLength(20)]
        public string Rms1Location { get; set; }
        [Column("isVehicle")]
        public bool IsVehicle { get; set; }
        [Column("isRcLocation")]
        public bool? IsRcLocation { get; set; }
        [Column("warehouseCode")]
        [StringLength(20)]
        public string WarehouseCode { get; set; }
        [Column("createdUserId")]
        public int CreatedUserId { get; set; }
        [Column("createdDate", TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }
        [Column("luUserId")]
        public int? LuUserId { get; set; }
        [Column("luDate", TypeName = "datetime")]
        public DateTime? LuDate { get; set; }

        [Column("deleted")]
        public bool Deleted { get; set; }

        [Column("isChamber")]
        public bool? IsChamber { get; set; }

        [Column("capacity")]
        public int? Capacity { get; set; }
    }
}
