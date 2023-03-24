using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Dtos
{
    public class WorkerDto
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; }
        
    }
   
}
