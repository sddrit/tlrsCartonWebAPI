using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Dtos.DailyCollectionMark
{
    public class DailyCollectionMarkDto
    {
        
        public string RequestNo { get; set; }
       
        public string CustomerCode { get; set; }       
        public string Name { get; set; }       
        public string Address { get; set; }        
       
        public int? CollectionCartonCount { get; set; }        
        public int DeliveryDate { get; set; }        
        public string DeliveryRoute { get; set; }       
        public bool Collected { get; set; }
        
    }

    public class DailyCollectionMarkUpdateDto
    {

        public string RequestNo { get; set; }       
        public bool Collected { get; set; }

    }
}
