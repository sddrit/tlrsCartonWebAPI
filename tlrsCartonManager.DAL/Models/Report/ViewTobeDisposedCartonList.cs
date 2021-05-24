using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
    [Keyless]
    public partial class ViewTobeDisposedCartonList
    {
        [Required]
        [StringLength(50)]
        public string CartonNo { get; set; }
        [Required]
        [StringLength(20)]
        public string LastRequestNo { get; set; }
        [Required]
        [StringLength(4000)]
        public string LastTransactionDate { get; set; }
        [Required]
        [StringLength(10)]
        public string CustomerCode { get; set; }
        public int CustomerId { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
    }
}
