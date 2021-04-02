using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
    [Table("CustomerAuthorizationListDetail")]
    public partial class CustomerAuthorizationListDetail
    {
        [Key]
        [Column("trackingId")]
        public int TrackingId { get; set; }
        [Column("authorizationId")]
        public int? AuthorizationId { get; set; }
        [Column("level")]
        public int? Level { get; set; }

        [ForeignKey(nameof(AuthorizationId))]
        [InverseProperty(nameof(CustomerAuthorizationListHeader.CustomerAuthorizationListDetails))]
        public virtual CustomerAuthorizationListHeader Authorization { get; set; }
        [ForeignKey(nameof(Level))]
        [InverseProperty(nameof(AuthorizationLevel.CustomerAuthorizationListDetails))]
        public virtual AuthorizationLevel LevelNavigation { get; set; }
    }
}
