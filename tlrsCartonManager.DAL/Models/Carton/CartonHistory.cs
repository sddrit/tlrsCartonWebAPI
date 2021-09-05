using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Models.Carton
{
    public class CartonHistory
    {
        public int Id { get; set; }
        public int? CartonNo { get; set; }
        public string AlternativeCartonNo { get; set; }
        public string CustomerCode { get; set; }
        public string LocationCode { get; set; }
        public int? LastTransactionDate { get; set; }
        public string LastRequestNo { get; set; }
        public string Status { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ScannedDateTime { get; set; }
        public string Name { get; set; }
        public string DocketNo { get; set; }
        public string WareHouseCode { get; set; }
        public string InOut { get; set; }

    }
}
