using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
    [Table("AuthorizationLevel")]
    public partial class AuthorizationLevel
    {
        public AuthorizationLevel()
        {
            CustomerAuthorizationLists = new HashSet<CustomerAuthorizationList>();
        }

        [Key]
        [Column("trackingId")]
        public int TrackingId { get; set; }
        [Column("authorizationdescription")]
        [StringLength(100)]
        public string Authorizationdescription { get; set; }
        [Column("status")]
        public int? Status { get; set; }

        [InverseProperty(nameof(CustomerAuthorizationList.LevelOfAuthorityNavigation))]
        public virtual ICollection<CustomerAuthorizationList> CustomerAuthorizationLists { get; set; }
    }
}
