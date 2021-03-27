using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Dto
{
    public class UserPasswordDto
    {
        [Key]       
        public long TrackingId { get; set; }       
        public int? UserId { get; set; }      
        public byte[] PasswordHash { get; set; }       
        public byte[] PasswordSalt { get; set; }      
        public DateTime? PasswordExpiryDate { get; set; }      
        public DateTime? PasswordCreatedDate { get; set; }
    }
}
