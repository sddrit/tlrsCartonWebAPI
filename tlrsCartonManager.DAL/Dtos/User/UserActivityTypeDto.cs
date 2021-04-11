using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Dtos
{
    public class UserActivityTypeDto
    {
        [Key]      
        public int ActivityId { get; set; }      
        public string ActivityDescription { get; set; }
    }
}
