using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Dtos
{
    public class CartonLocationDto
    {
        [Key]
        public long Id { get; set; }
        public int? CartonNo { get; set; }
        public string BarCode { get; set; }
        public string LocationCode { get; set; }
        public string ContainerType { get; set; }
        public bool? IsFromMobile { get; set; }
        public long? ScannedDate { get; set; }
        public int? ClientId { get; set; }
        public string ScanDateTime { get; set; }
        public string Remark { get; set; }

    }

}
