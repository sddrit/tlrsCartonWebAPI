using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
    [Keyless]
    public partial class ViewUserRole
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("description")]
        [StringLength(50)]
        public string Description { get; set; }
       
    }
}
