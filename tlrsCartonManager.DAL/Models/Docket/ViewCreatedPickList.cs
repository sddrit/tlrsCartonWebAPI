using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using tlrsCartonManager.DAL.Models.GenericReport;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
    [Keyless]
    public partial class ViewCreatedPickList : IGenericReportDataItem
    {
        [Required]
        [StringLength(50)]
        [Column("PickList No")]
        public string PickListNo { get; set; }

        [Column("Carton No")]
        public int CartonNo { get; set; }

        [Required]
        [StringLength(50)]
        [Column("Bar Code")]
        public string Barcode { get; set; }

        [StringLength(20)]
        [Column("Location Code")]
        public string LocationCode { get; set; }

        [StringLength(20)]
        [Column("WareHouse Code")]
        public string WareHouseCode { get; set; }

        [StringLength(50)]
        [Column("Last Sent Device Id")]
        public string LastSentDeviceId { get; set; }

        [StringLength(100)]
        [Column("Assigned User")]
        public string AssignedUser { get; set; }

        [StringLength(20)]
        [Column("Request No")]
        public string RequestNo { get; set; }

        [StringLength(100)]
        [Column("Picked User")]
        public string PickedUser { get; set; }

        [Column("IsPicked")]
        public bool IsPicked { get; set; }

        [Column("Pick Date")]
        public long? PickDate { get; set; }

        public bool? Deleted { get; set; }

        [Column("Created User")]
        [StringLength(100)]
        public string CreatedUser { get; set; }

        [Column("Created Date")]
        [StringLength(4000)]
        public string CreatedDate { get; set; }

        [Column("Lu User")]
        public int? LuUser { get; set; }

        [Column("Lu Date")]
        [StringLength(4000)]
        public string LuDate { get; set; }

        public IList<KeyValuePair<string, string>> GetValues()
        {
            return new List<KeyValuePair<string, string>>()
            {
                 new KeyValuePair<string, string>("PickList No",PickListNo),
                 new KeyValuePair<string, string>("Carton No",CartonNo.ToString()),
                 new KeyValuePair<string, string>("Bar Code",Barcode),
                 new KeyValuePair<string, string>("Location Code",LocationCode),
                 new KeyValuePair<string, string>("WareHouse Code",WareHouseCode),
                 new KeyValuePair<string, string>("Last SentDevice Id",LastSentDeviceId),
                 new KeyValuePair<string, string>("Assigned User",AssignedUser),
                 new KeyValuePair<string, string>("Request No",RequestNo),
                 new KeyValuePair<string, string>("Picked User",PickedUser),
                 new KeyValuePair<string, string>("IsPicked",IsPicked.ToString()),
                 new KeyValuePair<string, string>("Pick Date",PickDate.ToString()),
                 new KeyValuePair<string, string>("Deleted",Deleted.ToString()),
                 new KeyValuePair<string, string>("Created User",CreatedUser),
                 new KeyValuePair<string, string>("Created Date",CreatedDate),
                 new KeyValuePair<string, string>("Lu User",LuUser.ToString()),
                 new KeyValuePair<string, string>("Lu Date",LuDate)

            };
        }
    }
}
