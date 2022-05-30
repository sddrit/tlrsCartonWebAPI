using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Models
{
    [Keyless]
    public class ViewCustomerDocketCopy
    {
        [Required]
        [Column("customerCode")]      
        public string CustomerCode { get; set; }
        [Column("docketType")]       
        public int DocketType { get; set; }
    }
}
