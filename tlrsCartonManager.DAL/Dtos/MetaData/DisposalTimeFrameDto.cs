using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace tlrsCartonManager.DAL.Dtos.MetaData
{
    
    public partial class DisposalTimeFrameDto
    {
       

        [Key]
       
        public int Id { get; set; }
       
        public string Description { get; set; }
       
        public bool? Active { get; set; }
     
        public bool? Deleeted { get; set; }
    
    }
}
