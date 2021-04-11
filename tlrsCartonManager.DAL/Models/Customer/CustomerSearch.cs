using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Models
{
   public  class CustomerSearch
    {
        [Key]
        public int TrackingId { get; set; }
        public string CustomerCode { get; set; }
        public string Name { get; set; }
        public string Address1 { get; set; }

        public string AccountType { get; set; }
    }
   
}
