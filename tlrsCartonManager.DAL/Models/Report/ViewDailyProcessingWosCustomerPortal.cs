using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Models.Report
{
    [Keyless]
    public class ViewDailyProcessingWosCustomerPortal
    {
        [Column("requestNo")]
        public string RequestNo { get; set; }

        [Column("customerCode")]
        public string CustomerCode { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("deliveryDate")]
        public int DeliveryDate { get; set; }

        [Column("noOfCartons")]
        public int NoOfCartons { get; set; }

        [Column("requestType")]
        public string RequestType { get; set; }

        [Column("processStatus")]
        public string ProcessStatus { get; set; }

        [Column("rejectReason")]
        public string RejectReason { get; set; }

    }
}
