using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
    [Keyless]
    public partial class ViewPrintedDocket
    {
        [Required]
        [StringLength(20)]
        public string RequestNo { get; set; }
        [Column("customerCode")]
        [StringLength(20)]
        public string CustomerCode { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(50)]
        public string Route { get; set; }
        public int? NoOfCartons { get; set; }
        public int? NoOfPickedCartons { get; set; }
        public int? NoOfFumigatedCartons { get; set; }
        public int? DeliveryDate { get; set; }
        [StringLength(32)]
        public string DocketType { get; set; }
        public int? AllocatedQty { get; set; }
        [StringLength(50)]
        public string DeliveryRouteId { get; set; }
        [Required]
        [StringLength(11)]
        public string DocketPrintStatus { get; set; }
        [Column(TypeName = "numeric(18, 0)")]
        public decimal? SerialNo { get; set; }
        [StringLength(50)]
        public string PrintedBy { get; set; }
        [StringLength(4000)]
        public string PrintedOn { get; set; }
        public string RequestType { get; set; }

    }
}
