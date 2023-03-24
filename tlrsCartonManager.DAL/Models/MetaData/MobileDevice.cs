using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace tlrsCartonManager.DAL.Models.MetaData
{
    [Table("MobileDevice")]
    public partial class MobileDevice
    {
        [Key]
        [Column("deviceCode")]
        [StringLength(20)]
        public string DeviceCode { get; set; }
        [Column("deviceName")]
        [StringLength(50)]
        public string DeviceName { get; set; }
        [Column("active")]
        public bool? Active { get; set; }
        [Column("deleted")]
        public bool? Deleted { get; set; }
        [Column("lastSynchedDate", TypeName = "datetime")]
        public DateTime? LastSynchedDate { get; set; }
        [Column("lastSynchedUser")]
        [StringLength(50)]
        public string LastSynchedUser { get; set; }
    }
}
