﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Dto
{
    public class UserDto
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserFullName { get; set; }
        public string EmpId { get; set; }
        public int? AppId { get; set; }
        public string UserPassword { get; set; }
        public DateTime? LastLoginDate { get; set; }
    }
}
