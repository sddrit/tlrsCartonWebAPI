using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
    [Table("CutomerInvoiceProfile")]
    public partial class CutomerInvoiceProfile
    {
        [Key]
        [Column("trackingId")]
        public int TrackingId { get; set; }
        [Column("customerId")]
        public int? CustomerId { get; set; }
        [Column("invoiceProfileId")]
        public int? InvoiceProfileId { get; set; }
        [Column("expireDate", TypeName = "datetime")]
        public DateTime? ExpireDate { get; set; }
        [Column("createdUser")]
        public int? CreatedUser { get; set; }
        [Column("createDate", TypeName = "datetime")]
        public DateTime? CreateDate { get; set; }
        [Column("luUser")]
        public int? LuUser { get; set; }
        [Column("luDate", TypeName = "datetime")]
        public DateTime? LuDate { get; set; }

        [ForeignKey(nameof(InvoiceProfileId))]
        [InverseProperty("CutomerInvoiceProfiles")]
        public virtual InvoiceProfile InvoiceProfile { get; set; }
        [ForeignKey(nameof(InvoiceProfileId))]
        [InverseProperty(nameof(Customer.CutomerInvoiceProfiles))]
        public virtual Customer InvoiceProfileNavigation { get; set; }
    }
}
