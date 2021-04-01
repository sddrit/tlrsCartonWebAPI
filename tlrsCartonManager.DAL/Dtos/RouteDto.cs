using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Dtos
{
    public class RouteDto
    {
        [Key]       
        public int Id { get; set; }      
        public string Description { get; set; }       
        public int? Status { get; set; }
    }

}
