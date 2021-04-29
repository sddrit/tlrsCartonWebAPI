using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Models.Operation
{
    public class OperationOverview
    {
        public ICollection<CartonSummary> CartonSummaryList { get; set; }
        public ICollection<CartonUserSummary> ScanBySummaryList { get; set; }
        public ICollection<CartonUserSummary> FumigationSummaryList { get; set; }
        public ICollection<CartonLocationSummary> BaySummaryList { get; set; }
        public ICollection<CartonLocationSummary> VehicleSummaryList { get; set; }
        public ICollection<CartonUserSummary> PalletedSummaryList { get; set; }
        public ICollection<RequestedDetail> RequestDetailList { get; set; }

    }
    public class CartonSummary
    {
        public string WOType { get; set; }
        public int CartonQty { get; set; }
    }
    public class CartonLocationSummary
    {
        public string LocationCode { get; set; }
        public int CartonQty { get; set; }
    }
    public class CartonUserSummary
    {
        public string UserName { get; set; }
        public int CartonQty { get; set; }
    }
    public class RequestedDetail
    {
        public string RequestNo { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string Requested { get; set; }
        public string LBayScan { get; set; }
        public string VehicleScan { get; set; }
    }
    public class OperationOverviewByWoType
    {
        public string CustomerCode { get; set; }
        public string Name { get; set; }
        public string RequestNo { get; set; }
        public int RequestedQty { get; set; }
        public int PickedQty { get; set; }
        public int AllocatedQty { get; set; }
        public int LoadingBayQty { get; set; }
        public int VehicleQty { get; set; }
        public int ProjectBayQty { get; set; }
        public int ReferenceBayQty { get; set; }
        public int DisposalBayQty { get; set; }

    }
    public class OperationOverviewByUserLocaion
    {
        public int Id { get; set; }
        public int CartonNo { get; set; }
        public DateTime ScanDateTime { get; set; }
        public string LocationCode { get; set; }
        public string  UserName { get; set; }
    }
}
