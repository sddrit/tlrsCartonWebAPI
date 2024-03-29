﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
    [Keyless]
    public partial class ViewPendingRequest
    {
        [Required]
        [Column("requestNo")]
        [StringLength(20)]
        public string RequestNo { get; set; }
        [Column("woType")]
        [StringLength(20)]
        public string WoType { get; set; }
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
        [Column("docketNo")]
        [StringLength(20)]
        public string DocketNo { get; set; }
        [Column("deliveryDateInt")]
        public int? DeliveryDateInt { get; set; }
        [Column("noOfCartons")]
        public int? NoOfCartons { get; set; }
        [Column("deliveryDate")]
        [StringLength(10)]
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
        [Column("remarkCarton")]
        [StringLength(50)]
        public string RemarkCarton { get; set; }
        [Required]
        [Column("deliveryRoute")]
        [StringLength(50)]
        public string DeliveryRoute { get; set; }
        [Required]
        [Column("reminder")]
        public string Reminder { get; set; }
        [Column("requestType")]
        public string RequestType { get; set; }

        [Column("mainCustomerCode")]
        public int? MainCustomerCode { get; set; }


        [Column("trackingId")]
        public int? TrackingId { get; set; }


        [Column("reminder1")]
        public string Reminder1 { get; set; }


        [Column("reminder2")]
        public string Reminder2 { get; set; }

        [Column("reminder3")]
        public string Reminder3 { get; set; }
    }
}
