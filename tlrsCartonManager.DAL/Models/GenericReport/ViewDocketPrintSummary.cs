using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Models.GenericReport
{
    [Keyless]
    public class ViewDocketPrintSummary :  IGenericReportDataItem
    {
        [Required]
        [StringLength(20)]
        [Column("Request No")]
        public string RequestNo { get; set; }

        [Column("Customer Code")]
        [StringLength(20)]
        public string CustomerCode { get; set; }


        [StringLength(50)]
        public string Name { get; set; }
        
        [StringLength(50)]
        public string Route { get; set; }

        [Column("No Of Cartons")]
        public int? NoOfCartons { get; set; }

        [Column("No Of Picked Cartons")]
        public int? NoOfPickedCartons { get; set; }

        [Column("No Of Fumigated Cartons")]
        public int? NoOfFumigatedCartons { get; set; }

        [Column("Delivery Date")]
        public int? DeliveryDate { get; set; }

        [Column("Docket Type")]
        [StringLength(32)]
        public string DocketType { get; set; }

        [Column("Allocated Qty")]
        public int? AllocatedQty { get; set; }
        

        [Required]
        [StringLength(11)]
        [Column("Docket Print Status")]
        public string DocketPrintStatus { get; set; }

        [Column("Docket No")]
        public decimal? DocketNo { get; set; }

        [Column("Printed By")]
        [StringLength(50)]
        public string PrintedBy { get; set; }

        [Column("Printed On")]
        [StringLength(4000)]
        public string PrintedOn { get; set; }

        [Column("Request Type")]
        public string RequestType { get; set; }


        public IList<KeyValuePair<string, string>> GetValues()
        {
            return new List<KeyValuePair<string, string>>()
            {
                       new KeyValuePair<string, string>("Request No", RequestNo),
                       new KeyValuePair<string, string>("Customer Code", CustomerCode),
                       new KeyValuePair<string, string>("Name", Name),
                       new KeyValuePair<string, string>("Route", Route),
                       new KeyValuePair<string, string>("No Of Cartons", NoOfCartons.ToString()),
                       new KeyValuePair<string, string>("No Of PickedCartons", NoOfPickedCartons.ToString()),
                       new KeyValuePair<string, string>("No Of FumigatedCartons", NoOfFumigatedCartons.ToString()),
                       new KeyValuePair<string, string>("Delivery Date", DeliveryDate.ToString()),
                       new KeyValuePair<string, string>("Docket Type", DocketType),
                       new KeyValuePair<string, string>("Allocated Qty", AllocatedQty.ToString()),
                       new KeyValuePair<string, string>("Docket Print Status", DocketPrintStatus),
                       new KeyValuePair<string, string>("Docket No", DocketNo.ToString()),
                       new KeyValuePair<string, string>("Printed By", PrintedBy),
                       new KeyValuePair<string, string>("Printed On", PrintedOn),
                       new KeyValuePair<string, string>("Request Type", RequestType)

            };
        }
    }
}
