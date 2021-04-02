using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
    [Table("Log")]
    public partial class Log
    {
        [Key]
        public int Id { get; set; }
        public int LogLevelId { get; set; }
        [Required]
        public string ShortMessage { get; set; }
        public string FullMessage { get; set; }
        [StringLength(200)]
        public string IpAddress { get; set; }
        public int? CustomerId { get; set; }
        public string PageUrl { get; set; }
        public string ReferrerUrl { get; set; }
        public DateTime CreatedOnUtc { get; set; }
    }
}
