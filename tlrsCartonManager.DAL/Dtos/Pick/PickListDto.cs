using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Dtos.Pick
{
    public class PickListDto
    {
        [Key]     
        public long TrackingId { get; set; }       
        public string PickListNo { get; set; }     
        public int CartonNo { get; set; }       
        public string Barcode { get; set; }       
        public string LocationCode { get; set; }      
        public string WareHouseCode { get; set; }      
        public string LastSentDeviceId { get; set; }       
        public int? AssignedUserId { get; set; }       
        public string RequestNo { get; set; }
        public int PickedUserId { get; set; }       
        public bool IsPicked { get; set; }      
        public long? PickDate { get; set; }     
      
    }
    public class PickListSearchDto
    {
        public string PickListNo { get; set; }
        public int CartonNo { get; set; }
        public string Barcode { get; set; }
        public string LocationCode { get; set; }
        public string LastSentDeviceId { get; set; }  
        public string RequestNo { get; set; }
    }
}
