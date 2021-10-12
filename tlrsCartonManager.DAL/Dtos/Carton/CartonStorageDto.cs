using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tlrsCartonManager.DAL.Models.Carton;

namespace tlrsCartonManager.DAL.Dtos
{
    public class CartonStorageDto
    {
        [Key]
        public int CartonNo { get; set; }
        public string Status { get; set; }
        public string LastRequestNo { get; set; }
        public int? CustomerId { get; set; }
        public string LocationCode { get; set; }
        public int? LastTransactionDate { get; set; }
        public int? DisposalDate { get; set; }
        public int? DisposalTimeFrame { get; set; }
        public int? ActualDisposalDate { get; set; }
        public int? StatusInOut { get; set; }
        public DateTime? StatusInOutDate { get; set; }
        public string AlternativeCartonNo { get; set; }
        public int? CartonType { get; set; }
        public int? LastUpdateDate { get; set; }
        public int? FirstInDate { get; set; }
        public string LastConfirmedStatus { get; set; }
        public string LastConfirmedRequestNo { get; set; }
        public DateTime? LastOwnershipChangedDate { get; set; }
        public int? LastDeliveryRoute { get; set; }
        public int CreatedUserId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerCode { get; set; }
        public virtual ICollection<CartonLocationDto> CartonLocations { get; set; }
        public virtual ICollection<CartonRequest> CartonRequests { get; set; }

    }
    public class CartonStorageSearchDto
    {
        public int CartonNo { get; set; }
        public string Status { get; set; }
        public string LastRequestNo { get; set; }
        public string CustomerName { get; set; }
        public string CustomerCode { get; set; }
        public string LocationCode { get; set; }
        public string AlternativeCartonNo { get; set; }
        public string CartonType { get; set; }
    }
}
