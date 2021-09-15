using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Dtos.DailyCollectionMark
{
    public class ReminderDto
    {
        public string RequestNo { get; set; }
        public string CustomerCode { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int DeliveryDate { get; set; }
        public string Reminder1 { get; set; }
        public string Reminder2 { get; set; }
        public string Reminder3 { get; set; }

    }

    public class ReminderUpdateDto
    {
        public string RequestNo { get; set; }
        public string Reminder1 { get; set; }
        public string Reminder2 { get; set; }
        public string Reminder3 { get; set; }

    }
}
