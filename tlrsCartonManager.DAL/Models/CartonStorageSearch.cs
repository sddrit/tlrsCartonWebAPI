using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Models
{
   public  class CartonStorageSearch
    {
        [Key]
        public int CartonNo { get; set; }
        public string Status { get; set; }
        public string AlternativeCartonNo { get; set; }
        public string CustomerName { get; set; }    
        public string CartonType { get; set; }
    }
   
}
