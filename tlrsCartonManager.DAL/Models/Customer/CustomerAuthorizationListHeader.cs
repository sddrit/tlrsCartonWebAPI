using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
    [Table("CustomerAuthorizationListHeader")]
    public partial class CustomerAuthorizationListHeader
    {
        public CustomerAuthorizationListHeader()
        {
            CustomerAuthorizationListDetails = new HashSet<CustomerAuthorizationListDetail>();
        }

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
        [Column("email")]
        [StringLength(50)]
        public string Email { get; set; }
        [Column("active")]
        public bool? Active { get; set; }
        [Column("createdUserId")]
        public int CreatedUserId { get; set; }
        [Column("createdDate", TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }
        [Column("luUserId")]
        public int? LuUserId { get; set; }
        [Column("luDate", TypeName = "datetime")]
        public DateTime? LuDate { get; set; }
        [Column("deleted")]
        public bool? Deleted { get; set; }
        [Column("contactNo")]
        [StringLength(10)]
        public string ContactNo { get; set; }

        [ForeignKey(nameof(CustomerId))]
        [InverseProperty("CustomerAuthorizationListHeaders")]
        public virtual Customer Customer { get; set; }
        [InverseProperty(nameof(CustomerAuthorizationListDetail.Authorization))]
        public virtual ICollection<CustomerAuthorizationListDetail> CustomerAuthorizationListDetails { get; set; }
    }
    public partial class CustomerAuthorizationHeader
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
        [Column("email")]
        [StringLength(50)]
        public string Email { get; set; }
        [Column("active")]
        public bool? Active { get; set; }
        [Column("createdUser")]
        public int CreatedUser { get; set; }
        [Column("createdDate", TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }
        [Column("luUser")]
        public int? LuUser { get; set; }
        [Column("luDate", TypeName = "datetime")]
        public DateTime? LuDate { get; set; }
        [Column("deleted")]
        public bool? Deleted { get; set; }
        [Column("contactNo")]
        [StringLength(10)]
        public string ContactNo { get; set; }
    }
}


