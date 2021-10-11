using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Dtos.Company
{
    public class TaxEffectiveDateDto
    {
        public long Id { get; set; }       
        public string TaxCode { get; set; }        
        public int EffectiveFromDate { get; set; }           
        public decimal Rate { get; set; }     
        public bool ShowTaxInfoOnInvoice { get; set; }
        public string RateType { get; set; }
        public decimal? MinAmount { get; set; }


    }
}
