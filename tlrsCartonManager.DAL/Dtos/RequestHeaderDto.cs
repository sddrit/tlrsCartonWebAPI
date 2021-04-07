﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Dtos
{
    public class RequestHeaderDto
    {
        [Key]     
        public long TrackingId { get; set; }      
        public string RequestNo { get; set; }       
        public int? CustomerId { get; set; }      
        public int? Priority { get; set; }        
        public int? DeliveryDate { get; set; }       
        public int? OrdeReceivedBy { get; set; }       
        public string Remark { get; set; }     
        public string CustomerReference { get; set; }      
        public string ContactPerson { get; set; }       
        public int? NoOfCartons { get; set; }      
        public string RemarkCarton { get; set; }      
        public string RequestType { get; set; }       
        public int? Status { get; set; }      
        public int? CartonType { get; set; }       
        public string StorageCategory { get; set; }       
        public string DeviceId { get; set; }       
        public string MobileRequestNo { get; set; }       
        public string ContactPersonName { get; set; }       
        public string DeliveryLocation { get; set; }        
        public string DeliveryRouteId { get; set; }       
        public string Reminder1 { get; set; }       
        public string Reminder2 { get; set; }      
        public string Reminder3 { get; set; }    
        public virtual ICollection<RequestDetailDto> RequestDetails { get; set; }
    }
    public class RequestSearchDto
    {
        public string RequestNo { get; set; }
        public string  CustomerCode { get; set; }
        public string  CustomerName { get; set; }
        public int DeliveryDate { get; set; }
    }
}
