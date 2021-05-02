using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tlrsCartonManager.DAL.Models.Docket;
using tlrsCartonManager.DAL.Models.Ownership;

namespace tlrsCartonManager.DAL.Models.Carton
{
    public class CartonOverview
    {
        public CartonStorage CartonHeader { get; set; }
        public List<InvoiceConfirmation> RequestHeader { get; set; }
        public List<CartonLocation> LocationDetail { get; set; }        
        public List<PickList> PickList { get; set; }
        public List<DocketPrintDetail>  PrintedDockets { get; set; }
        public List<CartonOwnerShip> CartonOwnership { get; set; }
    }
}
