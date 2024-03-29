﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
    [Table("InvoiceTemplateHeaderCustomer")]
    public partial class InvoiceTemplateHeaderCustomer
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("description")]
        [StringLength(500)]
        public string Description { get; set; }
        [Required]
        [Column("customerCode")]
        [StringLength(20)]
        public string CustomerCode { get; set; }
        [Column("storageType")]
        public int StorageType { get; set; }
        [Required]
        [Column("invoiceTypeCode")]
        [StringLength(20)]
        public string InvoiceTypeCode { get; set; }
        [Column("isSplitInvoice")]
        public bool IsSplitInvoice { get; set; }
        [Required]
        [Column("active")]
        public bool? Active { get; set; }
        [Column("deleted")]
        public bool Deleted { get; set; }
        [Column("createdUserId")]
        public int CreatedUserId { get; set; }
        [Column("createdDate", TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }
        [Column("luUserId")]
        public int LuUserId { get; set; }
        [Column("luDate", TypeName = "datetime")]
        public DateTime LuDate { get; set; }

      
    }
}
