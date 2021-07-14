using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Models.DashBoard
{
    public class DashBoardWeeklyStatus
    {
        List<DashBoardWeeklyWOStaus> WoStatusList { get; set; }
        List<DashBoardWeeklyWOStausCarton> WoCartontatusList { get; set; }

    }
    public class DashBoardWeeklyWOStatusDetail
    {
        public DateTime DeliveryDate { get; set; }
        public string WoType { get; set; }
        public int? ReceivedCount { get; set; }
        public int? CompletedCount { get; set; }
        public int? PendingCount { get; set; }

    }
    public class DashBoardWeeklyPendingRetrievalByTypeDetail
    {
        public DateTime DeliveryDate { get; set; }
        public string WoType { get; set; }
        public int? PendingCount { get; set; }

    }

    public class DashBoardWeeklyWOStaus
    {
        public int DeliveryDate { get; set; }
        public string RequestType { get; set; }
        public string WoStatus { get; set; }
        public int WorkOrderCount { get; set; }

    }

    public class DashBoardWeeklyWOStausCarton
    {
        public int DeliveryDate { get; set; }
        public string RequestType { get; set; }
        public string WoStatus { get; set; }
        public int CartonCount { get; set; }

    }

    public class DailyDashBoardResponse
    {
        public List<DailyDashBoardByWo> DailyDashBoardByWos { get; set; }
        public List<DailyDashBoardByCarton> DailyDashBoardByCartons { get; set; }
        //public List<DailyDashBoardByVehicle> DailyDashBoardByVehicles { get; set; }

    }

    public class DailyDashBoardData
    {
        public string RequestType { get; set; }
        public int DataGroup { get; set; }
        public int DataGroupCount { get; set; }

    }
    public class DailyDashBoardByWo
    {
        public string RequestType { get; set; }
        public int WoCount { get; set; }

    }
    public class DailyDashBoardByCarton
    {
        public string RequestType { get; set; }
        public int CartonCount { get; set; }

    }
    public class DailyDashBoardByVehicle
    {
        public string VehicleNo { get; set; }
        public int CartonCount { get; set; }

    }

    public class DashBoardWeeklyCartonsInAndConfirm
    {
        public long ScannedDate { get; set; }
        public string Status { get; set; }
        public int CartonCount { get; set; }

    }

    public class DashBoardWeeklyWeeklyScannedCartons
    {
        public string ScannedUser { get; set; }
        public long ScannedDate { get; set; }
        public int CartonCount { get; set; }
    }

    public class DashBoarWeeklyScannedCartonsByWH
    {
        public string WareHouseCode { get; set; }
        public long ScannedDate { get; set; }
        public int CartonCount { get; set; }

    }

}
