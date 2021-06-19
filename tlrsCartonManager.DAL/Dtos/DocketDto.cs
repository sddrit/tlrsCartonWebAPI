using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Dtos
{
    public class DocketPrintDetailModel
    {
        public int? Id { get; set; }

        public int? No1 { get; set; }
        public string Col1 { get; set; }

        public int? No2 { get; set; }
        public string Col2 { get; set; }

        public int? No3 { get; set; }
        public string Col3 { get; set; }

        public int? No4 { get; set; }
        public string Col4 { get; set; }


    }
    public class DocketPrintEmptyDetailModel
    {
        public int? No { get; set; }
        public int? FromCarton { get; set; }
        public int? ToCarton { get; set; }

    }
    public class DocketPrintModel
    {
        public string RequestNo { get; set; }
        public string PrintedBy { get; set; }
        public string RequestType { get; set; }

    }
    public class DocketRePrintModel
    {
        public int? SerialNo { get; set; }
        public string RequestNo { get; set; }
        public string PrintedBy { get; set; }
        public string RequestType { get; set; }

    }
    public class DocketPrintResultModel
    {
        public string RequestNo { get; set; }
        public string DocketType { get; set; }
        public int SerialNo { get; set; }
        public string CustomerCode { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string ContactPerson { get; set; }
        public string PONo { get; set; }
        public string ContactNo { get; set; }
        public string Department { get; set; }
        public bool IsPrintAlternativeNo { get; set; }
        public List<DocketPrintEmptyDetailModel> EmptyList { get; set; }
        public List<DocketPrintDetailModel> CartonList { get; set; }

    }
    public class DocketPrintBulkModel
    {

        public List<DocketPrintModel> RequestNos { get; set; }

    }

    public class DocketPrintBulkResult
    {
        public string RequestNo { get; set; }
        public string RequestType { get; set; }
        public int? SerialNo { get; set; }
        public bool IsProcessed { get; set; }
        public int? Id { get; set; }
        public int? No1 { get; set; }
        public string Col1 { get; set; }
        public int? No2 { get; set; }
        public string Col2 { get; set; }
        public int? No3 { get; set; }
        public string Col3 { get; set; }
        public int? No4 { get; set; }
        public string Col4 { get; set; }

    }
}
