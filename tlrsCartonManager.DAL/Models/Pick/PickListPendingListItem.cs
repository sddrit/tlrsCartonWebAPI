using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace tlrsCartonManager.DAL.Models.Pick
{
    [Keyless]
    public partial class PickListPendingListItem
    {
        [Required]
        [Column("requestNo")]
        [StringLength(20)]
        public string RequestNo { get; set; }
        [Column("requestType")]
        [StringLength(10)]
        public string RequestType { get; set; }
        [Column("cartonNo")]
        public int CartonNo { get; set; }
        [Column("locationCode")]
        [StringLength(20)]
        public string LocationCode { get; set; }
        [Column("warehouseCode")]
        [StringLength(20)]
        public string WarehouseCode { get; set; }
        [Column("woType")]
        [StringLength(20)]
        public string WoType { get; set; }
        [Required]
        [Column("customerCode")]
        [StringLength(20)]
        public string CustomerCode { get; set; }
        [Column("name")]
        [StringLength(50)]
        public string Name { get; set; }
        [Column("customerAddress")]
        [StringLength(50)]
        public string CustomerAddress { get; set; }
        [Column("deliveryDate")]
        public int? DeliveryDate { get; set; }
    }
}
