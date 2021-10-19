using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Dtos.Location
{
    public class LocationDto
    {
       
        public string Code { get; set; }
      
        public string Name { get; set; }
       
        public bool? Active { get; set; }
       
        public string Rms1Location { get; set; }
       
        public bool? IsVehicle { get; set; }
      
        public bool? IsRcLocation { get; set; }

        public bool? IsBay { get; set; }

        public bool? IsChamber { get; set; }

        public int? Capacity { get; set; }

        public string WareHouseCode { get; set; }

    }
}
