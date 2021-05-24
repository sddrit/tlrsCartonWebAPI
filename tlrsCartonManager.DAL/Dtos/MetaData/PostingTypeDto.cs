using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Dtos
{
    public class PostingTypeDto
    {
        [Key]       
        public string Code { get; set; }      
        public string Description { get; set; }       
        public bool? Active { get; set; }     
        public bool Deleted { get; set; }
        public bool IsShowQty { get; set; }
        public bool IsShowAmount { get; set; }
        public bool IsShowPercentage { get; set; }
    }

}
