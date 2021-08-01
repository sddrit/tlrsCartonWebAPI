using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Models.InvoiceProfile
{
    public class InvoiceProfileSearch
    {
        public int Id { get; set; }
        public string CustomerCode { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string StorageType { get; set; }

    }
    public class InvoiceProfileRate
    {
      
        public int LineId { get; set; }    
        public string Description { get; set; }
        public string RequestType { get; set; }
        public string WoType { get; set; }
        public decimal Rate { get; set; }

    }
    public class InvoiceProfileRateModel
    {
        public int Id { get; set; }      
        public string CustomerCode { get; set; }
        public int StorageType { get; set; }
        public List<InvoiceProfileRate> InvoiceProfileRates { get; set; }

    }

    public class InvoiceProfileHeaderModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string CustomerCode { get; set; }
        public int StorageType { get; set; }
        public string InvoiceTypeCode { get; set; }
        public bool IsSplitInvoice { get; set; }
        public bool Active { get; set; }
        public List<SupportingDocsViewModel> SupportingDocs { get; set; }

    }

    public class SupportingDocsViewModel
    {
        public int Id { get; set; }

    }
}
