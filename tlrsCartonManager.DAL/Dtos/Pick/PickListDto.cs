using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Dtos.Pick
{
    public class PickListHeaderDto
    {
        [Key]

        public string PickListNo { get; set; }
        public string LastSentDeviceId { get; set; }
        public List<int> AssignedUserId { get; set; }
        public string AssignedUserName { get; set; }
        public string PickedUserName { get; set; }        
        public DateTime CreatedDate { get; set; }          
        public bool? Printed { get; set; }
        public string SplitWoNumber { get; set; }
        public int? PickedCount { get; set; }

        public ICollection<PickListDetailItemDto> PickListDetail { get; set; }

    }

    public class PickListSummaryDto
    {            
        public string AssignedUserName { get; set; }       
        public int PickedCount { get; set; }
        public int CartonCount { get; set; }
        

    }

    public class PickListHeaderSingleSearchDto
    {
        [Key]

        public string PickListNo { get; set; }
        public string LastSentDeviceId { get; set; }
        public int? AssignedUserId { get; set; }
        public string AssignedUserName { get; set; }
        public string PickedUserName { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool? Printed { get; set; }
        public string SplitWoNumber { get; set; }
        public int? PickedCount { get; set; }

        public ICollection<PickListDetailItemDto> PickListDetail { get; set; }

    }


    public class PickListResponseDto
    {
        public string PickListNo { get; set; }
        public string LastSentDeviceId { get; set; }
        public List<int> AssignedUserId { get; set; }
        public ICollection<PickListResponseDetailDto> PickListDetail { get; set; }
    }
    public class PickListResponseDetailDto
    {
        public long TrackingId { get; set; }
        public int CartonNo { get; set; }
        public byte[] RowVersion { get; set; }  
       
    }
    public class PickListSearchDto
    {
        public string PickListNo { get; set; }
        public string LastSentDeviceId { get; set; }
        public string AssignedUser { get; set; }
        public int NoOfCartons { get; set; }
        public bool? Printed { get; set; }
        public int NoOfCartonsPicked { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
    public class PickListDetailItemDto
    {
        public string RequestNo { get; set; }
        public string RequestType { get; set; }
        public int CartonNo { get; set; }
        public string LocationCode { get; set; }
        public string WarehouseCode { get; set; }
        public string WoType { get; set; }
        public string CustomerCode { get; set; }
        public string Name { get; set; }
        public string CustomerAddress { get; set; }
        public int? DeliveryDate { get; set; }
        public byte[] RowVersion { get; set; }


        public long TrackingId { get; set; }
        public int PickedUserId { get; set; }
        public string PickedUserName { get; set; }
        public bool IsPicked { get; set; }
        public long? PickDate { get; set; }

        public int? LastScannedDate { get; set; }
        public string Status { get; set; }

        public string Note { get; set; }

    }
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
    public class PickListSummaryRequest
    {
        public string RequestType { get; set; }
        public string WoType { get; set; }
        public int CartonCount { get; set; }

    }

    public class PickListSummaryDate
    {
        public int DeliveryDate { get; set; }       
        public int CartonCount { get; set; }

    }

    public class PickListSummaryWareHouse
    {
        public string WareHouseCode { get; set; }
        public int CartonCount { get; set; }
        public string RequestType { get; set; }

    }

    public class PickListSummaryCustomer
    {
        public string CustomerCode { get; set; }
        public string Name { get; set; }
        public int CartonCount { get; set; }
        public string RequestType { get; set; }
        public string WareHouseCode { get; set; }

    }
}
