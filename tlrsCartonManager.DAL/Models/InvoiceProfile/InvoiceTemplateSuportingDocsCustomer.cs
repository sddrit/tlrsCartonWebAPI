using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
    [Table("InvoiceTemplateSuportingDocsCustomer")]
    public partial class InvoiceTemplateSuportingDocsCustomer
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("customerCode")]
        [StringLength(20)]
        public string CustomerCode { get; set; }
        [Column("docId")]
        public int DocId { get; set; }
        [Column("createdUserId")]
        public int CreatedUserId { get; set; }
        [Column("createdDate", TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }
        [Column("luId")]
        public int LuId { get; set; }
        [Column("luDate", TypeName = "datetime")]
        public DateTime LuDate { get; set; }
    }
}
