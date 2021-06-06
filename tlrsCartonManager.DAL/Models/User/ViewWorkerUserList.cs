using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
    [Keyless]
    public partial class ViewWorkerUserList
    {
        [Column("userId")]
        public int UserId { get; set; }
        [Required]
        [Column("userName")]
        [StringLength(100)]
        public string UserName { get; set; }
    }
}
