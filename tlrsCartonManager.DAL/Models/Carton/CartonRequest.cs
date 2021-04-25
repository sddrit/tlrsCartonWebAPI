using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Models.Carton
{
    public class CartonRequest
    {
        
        public string WoType { get; set; }
        public string LastRequestNo { get; set; }
        public DateTime LastTransactionDate { get; set; }
        public string CustomerCode { get; set; }
        public string Name { get; set; }
    }
}
