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
        public int DeliveryDate { get; set; }
        public string RequestType { get; set; }
        public string WoStatus { get; set; }
        public int WorkOrderCount { get; set; }
        public int CartonCount { get; set; }

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
