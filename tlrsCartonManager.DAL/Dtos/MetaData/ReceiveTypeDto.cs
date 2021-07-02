using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using tlrsCartonManager.DAL.Models.Base;

#nullable disable

namespace tlrsCartonManager.DAL.Models.MetaData
{

    public partial class ReceiveTypeDto
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public bool? Active { get; set; }
        public bool Deleted { get; set; }
    }
}
