using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Dtos
{
    public class InvoiceConfirmationDto
    {
        [Key]
        public string RequestNo { get; set; }
        public string DocketNo { get; set; }
        public DateTime ReceiveDate { get; set; }
        public int UserId { get; set; }

    }
    public class InvoiceConfirmationSearchDto
    {
        public string RequestNo { get; set; }
        public int RequestDate { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string WOType { get; set; }
    }
    public class InvoiceConfirmationHeaderDto
    {
        public string RequestNo { get; set; }
        public int RequestDate { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string WOType { get; set; }
        public ICollection<InvoiceConfirmationDetailDto> InvoiceConfirmationDetailDto { get; set; }
    }
    public class InvoiceConfirmationDetailDto
    {
        [Key]
        public int CartonNo { get; set; }
        public int? DisposalDate { get; set; }
        public string DisposalTimeFrame { get; set; }      
        public bool? Picked { get; set; }
        public string PickListNo { get; set; }
       public string LocationCode { get; set; }

    }
}
