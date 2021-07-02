using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Dtos.MetaData
{
    public class RequestTypeDto
    {
         
        public string TypeCode { get; set; }
        [Key]
        public int Id { get; set; }       
        public bool? Active { get; set; }
       
    }
}
