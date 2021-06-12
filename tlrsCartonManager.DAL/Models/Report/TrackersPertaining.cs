using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Models.Report
{
    public class TrackersPertaining
    {
    }
    public class RetentionTracker
    {
        public string CustomerName { get; set; }
        public string CartonNumber { get; set; }
        public string DateLodged { get; set; }
        public string DestructionDateGivenbyBank { get; set; }
        public int? TargetforDestruction { get; set; }
    }
    public class RetentionTrackerDisposal
    {
        public string CustomerName { get; set; }
        public string AuthorizedOfficer { get; set; }
        public string CartonNumber { get; set; }
        public string DateLodged { get; set; }
        public string DestructionDateGivenbyBank { get; set; }
        public int? TargetforDestruction { get; set; }
        public string Dateassignedbyvendorfordestruction { get; set; }
        public int? DestructionAgeing { get; set; }

    }
    public class RetrievalTracker
    {
        public string CustomerName { get; set; }
        public string RequestStaffName { get; set; }
        public string ReqDepartmentBranchName { get; set; }
        public string CartonNumber { get; set; }
        public string RequestedDatebySCB { get; set; }
        public string DeliveredDatetoSCB { get; set; }
        public int? SLAAdherence { get; set; }
        public string ReturneddatetoTransnational { get; set; }
        public int? OverallAgeing { get; set; }

    }
    public class LongOutstanding
    {
        public string CustomerName { get; set; }
        public string RequestStaffName { get; set; }
        public string ReqDepartmentBranchName { get; set; }
        public string CartonNumber { get; set; }
        public string RequestedDatebySCB { get; set; }
        public string DeliveredDatetoSCB { get; set; }       
        public int? OverallAgeing { get; set; }

    }
}
