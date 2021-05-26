using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
    [Keyless]
    public partial class ViewCustomerTransaction
    {
        [Required]
        [Column("customerCode")]
        [StringLength(10)]
        public string CustomerCode { get; set; }
        [Column("customerId")]
        public int CustomerId { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        [Column("mainCustomerCode")]
        public int? MainCustomerCode { get; set; }
        [Required]
        [StringLength(50)]
        public string CartonNo { get; set; }
        [Required]
        [StringLength(101)]
        public string Status { get; set; }
        [Required]
        [StringLength(20)]
        public string LastRequestNo { get; set; }
        public int? DeliveryDateInt { get; set; }
        [StringLength(4000)]
        public string DeliveryDate { get; set; }
        [StringLength(4000)]
        public string LastTransactionDate { get; set; }
        public int LastTransactionDateInt { get; set; }
    }
}
