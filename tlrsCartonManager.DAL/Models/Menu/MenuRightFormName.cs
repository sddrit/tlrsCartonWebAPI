using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
    public partial class MenuRightFormName
    {
        [Key]
        [Column("formRightID")]
        public int FormRightId { get; set; }
        [Required]
        [Column("formRightName")]
        [StringLength(50)]
        public string FormRightName { get; set; }
    }
}
