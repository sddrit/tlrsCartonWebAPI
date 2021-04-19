using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace tlrsCartonManager.DAL.Models.Docket
{
   
    public partial class DocketPrintDetail
    {
        [Key]
        [Column("serialNo", TypeName = "numeric(18, 0)")]
        public decimal SerialNo { get; set; }
        [Column("requestNo")]
        [StringLength(20)]
        public string RequestNo { get; set; }
        [Column("printedOn", TypeName = "datetime")]
        public DateTime? PrintedOn { get; set; }
        [Column("printedBy")]
        [StringLength(50)]
        public string PrintedBy { get; set; }
        [Column("customerCode")]
        [StringLength(10)]
        public string CustomerCode { get; set; }
        [Column("contactPerson")]
        [StringLength(50)]
        public string ContactPerson { get; set; }
        [Column("createdUser")]
        [StringLength(50)]
        public string CreatedUser { get; set; }
        [Column("createdDate", TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
    }
}
