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
            CustomerAuthorizationListDetails = new HashSet<CustomerAuthorizationListDetail>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("description")]
        [StringLength(100)]
        public string Description { get; set; }
        [Column("status")]
        public int? Status { get; set; }

        [InverseProperty(nameof(CustomerAuthorizationListDetail.LevelNavigation))]
        public virtual ICollection<CustomerAuthorizationListDetail> CustomerAuthorizationListDetails { get; set; }
    }
}
