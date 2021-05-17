﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Models
{
    public class RequestSearch
    {
        [Key]
        public string RequestNo { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public int CartonCount { get; set; }
        public int DeliveryDate { get; set; }
    }
}
