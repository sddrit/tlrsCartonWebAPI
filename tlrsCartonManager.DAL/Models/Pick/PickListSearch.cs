using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Models.Pick
{
    public class PickListSearch
    {
        [Key]
        public string PickListNo { get; set; }
        public int CartonNo { get; set; }      
        public string LocationCode { get; set; }
        public string LastSentDeviceId { get; set; }
        public string RequestNo { get; set; }
    }
}
