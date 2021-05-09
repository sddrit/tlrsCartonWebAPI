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
       
        public string PickListNo { get; set; }      
        public string LastSentDeviceId { get; set; }
        public string AssignedUser { get; set; }
        public int  NoOfCartons { get; set; }
    }
}
