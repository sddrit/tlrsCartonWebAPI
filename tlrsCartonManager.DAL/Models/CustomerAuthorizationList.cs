using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
    [Table("CustomerAuthorizationList")]
    public partial class CustomerAuthorizationList
    {
        [Key]
        [Column("trackingId")]
        public int TrackingId { get; set; }
        [Column("customerId")]
        public int CustomerId { get; set; }
        [Required]
        [Column("name")]

        [StringLength(50)]
        public string Name { get; set; }
        [Column("department")]
        [StringLength(50)]
        public string Department { get; set; }
        [Column("designation")]
        [StringLength(50)]
        public string Designation { get; set; }
        [Column("levelOfAuthority")]
        public int LevelOfAuthority { get; set; }
        [Column("email")]
        [StringLength(50)]
        public string Email { get; set; }
        [Column("createdUser")]
        public int CreatedUser { get; set; }
        [Column("createdDate", TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }
        [Column("luUser")]
        public int? LuUser { get; set; }
        [Column("luDate", TypeName = "datetime")]
        public DateTime? LuDate { get; set; }

        [ForeignKey(nameof(CustomerId))]
        [InverseProperty("CustomerAuthorizationLists")]
        public virtual Customer Customer { get; set; }
        [ForeignKey(nameof(LevelOfAuthority))]
        [InverseProperty(nameof(AuthorizationLevel.CustomerAuthorizationLists))]
        public virtual AuthorizationLevel LevelOfAuthorityNavigation { get; set; }
    }
}
