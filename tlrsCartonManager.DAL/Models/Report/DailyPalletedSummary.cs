using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Models.Report
{
    public class DailyPalletedSummary
    {
        public List<DailyPalletedDetail> PalletedDetails { get; set; }
        public List<DailyPalletedSummaryByScannedUser> PalletedSummaryByScannedUsers { get; set; }
        public List<DailyPalletedSummaryByWareHouse> PalletedSummaryByWareHouses { get; set; }

    }
    public class DailyPalletedDetail
    {
        public int CartonNo { get; set; }
        public string LocationCode { get; set; }
        public string WareHouseCode { get; set; }
        public string ScannedUser { get; set; }
        public DateTime? ScanDateTime { get; set; }
        public string PallettedLocation { get; set; }
        public DateTime? LastScannedDateTime { get; set; }
        public string LastScannedUser { get; set; }

        
    }
    public class DailyPalletedSummaryByScannedUser
    {
        public string ScannedUser { get; set; }
        public int CartonCount { get; set; }
    }
    public class DailyPalletedSummaryByWareHouse
    {
        public string WareHouseCode { get; set; }
        public int CartonCount { get; set; }
    }
}
