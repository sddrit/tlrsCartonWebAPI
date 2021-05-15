using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Dtos.Company
{
    public class CompanyDto
    {        
        public int CompanyId { get; set; }
      
        public string CompanyName { get; set; }
      
        public string Address1 { get; set; }
       
        public string Address2 { get; set; }
       
        public string Address3 { get; set; }
       
        public string Country { get; set; }
        
        public string Tel { get; set; }
      
        public string Fax { get; set; }
        
        public string Email { get; set; }
        
        public string VatNo { get; set; }
       
        public string NbtNo { get; set; }
    
        public string SvatNo { get; set; }
        public int UserId { get; set; }
        public ICollection<TaxEffectiveDateDto> TaxEffectiveDate { get; set; }
    }
}
