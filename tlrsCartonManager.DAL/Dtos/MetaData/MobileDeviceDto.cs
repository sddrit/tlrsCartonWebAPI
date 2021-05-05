using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Dtos
{
    public class MobileDeviceDto
    {
        [Key]
        public string DeviceCode { get; set; }

        public string DeviceName { get; set; }

        public bool? Active { get; set; }

        public bool? Deleted { get; set; }

    }

}
