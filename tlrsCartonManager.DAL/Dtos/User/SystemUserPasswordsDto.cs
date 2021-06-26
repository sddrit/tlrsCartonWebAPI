using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Dtos
{
    public class SystemUserPasswordsDto
    {
        [Required]
        public string UserID { get; set; }

        [Required]
        public string PasswordText { get; set; }
    }
    public class UserLoginModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
