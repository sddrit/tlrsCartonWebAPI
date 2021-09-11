using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
    [Keyless]
    public partial class ViewPendingRequestDailyCollection
    {
        [Required]
        [Column("requestNo")]
        [StringLength(20)]
        public string RequestNo { get; set; }
        [Column("customerCode")]
        [StringLength(20)]
        public string CustomerCode { get; set; }
        [Column("name")]
        [StringLength(50)]
        public string Name { get; set; }
        [Column("address")]
        [StringLength(152)]
        public string Address { get; set; }
        [Column("status")]
        [StringLength(100)]
        public string Status { get; set; }
        [Column("collectionCartonCount")]
        public int? CollectionCartonCount { get; set; }
        [Column("deliveryDate")]
        [StringLength(4000)]
        public string DeliveryDate { get; set; }
        [Required]
        [Column("contactPersonName")]
        [StringLength(200)]
        public string ContactPersonName { get; set; }
        [Required]
        [Column("deliveryLocation")]
        [StringLength(200)]
        public string DeliveryLocation { get; set; }
        [Required]
        [Column("deliveryRoute")]
        [StringLength(8000)]
        public string DeliveryRoute { get; set; }
        [Required]
        [Column("reminder")]
        public string Reminder { get; set; }
        [Column("collected")]
        public bool Collected { get; set; }

        [Column("deliveryDateInt")]
        public int? DeliveryDateInt { get; set; }
    }
}
