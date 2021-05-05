using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tlrsCartonManager.DAL.Dtos.Pick;
using tlrsCartonManager.DAL.Models;
using tlrsCartonManager.DAL.Models.Carton;
using tlrsCartonManager.DAL.Models.Docket;
using tlrsCartonManager.DAL.Models.Ownership;

namespace tlrsCartonManager.DAL.Dtos.Carton
{
    public class CartonOverviewDto
    {
        public CartonInquiry CartonHeader { get; set; }
        public ICollection<InvoiceConfirmation> RequestHeader { get; set; }
        public ICollection<CartonLocationDto> LocationDetail { get; set; }
        public ICollection<PickListDto> PickList { get; set; }
        public ICollection<DocketPrintDetail> PrintedDockets { get; set; }
        public ICollection<CartonOwnerShip> CartonOwnership { get; set; }
    }
}
