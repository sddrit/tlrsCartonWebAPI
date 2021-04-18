using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
    public partial class CartonInquiry
    {
        public CartonInquiry()
        {
           
        }

        [Key]
        [Column("cartonNo")]
        public int CartonNo { get; set; }
        [Column("status")]
        [StringLength(50)]
        public string Status { get; set; }
        [Column("lastRequestNo")]
        [StringLength(20)]
        public string LastRequestNo { get; set; }
        [Column("customerCode")]
        public string CustomerCode { get; set; }
        [Column("customerName")]        
        public string CustomerName { get; set; }
        [Column("locationCode")]
        [StringLength(20)]
        public string LocationCode { get; set; }
        [Column("lastTransactionDate")]
        public int? LastTransactionDate { get; set; }
        [Column("disposalDate")]
        public int? DisposalDate { get; set; }
        [Column("disposalTimeFrame")]
        public string  DisposalTimeFrame { get; set; }  
       
        [Column("alternativeCartonNo")]
        [StringLength(50)]
        public string AlternativeCartonNo { get; set; }
        [Column("cartonType")]
        public string CartonType { get; set; }
        [Column("lastUpdateDate")]
        public int? LastUpdateDate { get; set; }
        [Column("firstInDate")]
        public int? FirstInDate { get; set; }
        [Column("lastConfirmedStatus")]
        [StringLength(50)]
        public string LastConfirmedStatus { get; set; }
        [Column("lastConfirmedRequestNo")]
        [StringLength(20)]
        public string LastConfirmedRequestNo { get; set; }
        [Column("lastOwnershipChangedDate", TypeName = "datetime")]
        public DateTime? LastOwnershipChangedDate { get; set; }
        [Column("route")]
        public string Route { get; set; }
        [Column("createdDate", TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column("luDate", TypeName = "datetime")]
        public DateTime? LuDate { get; set; }
        [Column("createdUserName")]
        public string CreatedUserName { get; set; }
        [Column("luUserName")]
        public string LuUserName { get; set; }

       
    }
}
