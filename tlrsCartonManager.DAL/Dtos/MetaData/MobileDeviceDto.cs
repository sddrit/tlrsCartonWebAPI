﻿using System;
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
        public int Id { get; set; }
        public string Code { get; set; }

        public string Description { get; set; }

        public bool? Active { get; set; }


    }

}
