using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
    [Keyless]
    public partial class ViewInventorySummaryByCustomer
    {
        [Required]
        [Column("customerCode")]
        [StringLength(20)]
        public string CustomerCode { get; set; }
        [Required]
        [Column("name")]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [Column("address1")]
        [StringLength(50)]
        public string Address1 { get; set; }
        [Column("contactTelephone1")]
        [StringLength(50)]
        public string ContactTelephone1 { get; set; }
        [Column("contactName")]
        [StringLength(50)]
        public string ContactName { get; set; }
        [Column("serviceProvided")]
        [StringLength(50)]
        public string ServiceProvided { get; set; }
        [Column("emptyCartonCount")]
        public int EmptyCartonCount { get; set; }
        [Column("collectionCartonCount")]
        public int CollectionCartonCount { get; set; }
        [Column("retreivalCartonCount")]
        public int RetreivalCartonCount { get; set; }
        [Column("permOutCartonCount")]
        public int PermOutCartonCount { get; set; }
        [Column("disposalCartonCount")]
        public int DisposalCartonCount { get; set; }
    }
}
