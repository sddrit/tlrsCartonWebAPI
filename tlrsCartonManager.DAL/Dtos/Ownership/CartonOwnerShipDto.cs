using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Dtos.Ownership
{
    public class CartonOwnerShipDto
    {
        [Key]      
        public long Id { get; set; }      
        public int CartonNo { get; set; }       
        public string FromCustomerCode { get; set; }       
        public string ToCustomerCode { get; set; }       
        public DateTime? OwnershipChangedDate { get; set; }       
        public string OwnershipChangedBy { get; set; }
    }
}
