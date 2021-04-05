using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Dtos
{
    public class CartonTypeDto
    {
        [Key]       
        public int Id { get; set; }     
        public string Type { get; set; }      
        public string Description { get; set; }       
        public string Size { get; set; }       
        public bool? Active { get; set; }      
        public int? CreatedUser { get; set; }       
        public DateTime? CreatedDate { get; set; }     
        public int? LuUser { get; set; }       
        public DateTime? LuDate { get; set; }
    }

}
