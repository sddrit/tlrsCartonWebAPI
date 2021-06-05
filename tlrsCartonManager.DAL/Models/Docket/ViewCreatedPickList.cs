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
        public string PickListNo { get; set; }
        public int CartonNo { get; set; }
        [Required]
        [StringLength(50)]
        public string Barcode { get; set; }
        [StringLength(20)]
        public string LocationCode { get; set; }
        [StringLength(20)]
        public string WareHouseCode { get; set; }
        [StringLength(50)]
        public string LastSentDeviceId { get; set; }
        [StringLength(100)]
        public string AssignedUser { get; set; }
        [StringLength(20)]
        public string RequestNo { get; set; }
        [StringLength(100)]
        public string PickedUser { get; set; }
        public bool IsPicked { get; set; }
        public long? PickDate { get; set; }
        public bool? Deleted { get; set; }
        [Column("createdUser")]
        [StringLength(100)]
        public string CreatedUser { get; set; }
        [StringLength(4000)]
        public string CreatedDate { get; set; }
        [Column("luUser")]
        public int? LuUser { get; set; }
        [Column("LUDate")]
        [StringLength(4000)]
        public string LuDate { get; set; }

        public IList<KeyValuePair<string, string>> GetValues()
        {
            return new List<KeyValuePair<string, string>>()
            {
                 new KeyValuePair<string, string>("PickListNo",PickListNo),
                 new KeyValuePair<string, string>("CartonNo",CartonNo.ToString()),
                 new KeyValuePair<string, string>("Barcode",Barcode),
                 new KeyValuePair<string, string>("LocationCode",LocationCode),
                 new KeyValuePair<string, string>("WareHouseCode",WareHouseCode),
                 new KeyValuePair<string, string>("LastSentDeviceId",LastSentDeviceId),
                 new KeyValuePair<string, string>("AssignedUser",AssignedUser),
                 new KeyValuePair<string, string>("RequestNo",RequestNo),
                 new KeyValuePair<string, string>("PickedUser",PickedUser),
                 new KeyValuePair<string, string>("IsPicked",IsPicked.ToString()),
                 new KeyValuePair<string, string>("PickDate",PickDate.ToString()),
                 new KeyValuePair<string, string>("Deleted",Deleted.ToString()),
                 new KeyValuePair<string, string>("CreatedUser",CreatedUser),
                 new KeyValuePair<string, string>("CreatedDate",CreatedDate),
                 new KeyValuePair<string, string>("LuUser",LuUser.ToString()),
                 new KeyValuePair<string, string>("LuDate",LuDate)

            };
        }
    }
}
