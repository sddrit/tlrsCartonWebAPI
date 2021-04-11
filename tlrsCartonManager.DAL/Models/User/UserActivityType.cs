using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
    [Table("UserActivityType")]
    public partial class UserActivityType
    {
        [Key]
        [Column("activityId")]
        public int ActivityId { get; set; }
        [Column("activityDescription")]
        [StringLength(100)]
        public string ActivityDescription { get; set; }
    }
}
