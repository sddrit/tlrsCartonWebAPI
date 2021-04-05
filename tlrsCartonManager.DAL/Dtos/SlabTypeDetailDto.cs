using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Dtos
{
    public class SlabTypeDetailDto
    {
        [Key]
        public int TrackingId { get; set; }       
        public int? Id { get; set; }      
        public int? RowId { get; set; }      
        public int? FromSlab { get; set; }        
        public int? ToSlab { get; set; }       
        public decimal? Rate { get; set; }
     
    }

}
