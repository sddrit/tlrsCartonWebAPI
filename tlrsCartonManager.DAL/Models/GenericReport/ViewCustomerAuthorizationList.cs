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
    public partial class ViewCustomerAuthorizationList : IGenericReportDataItem
    {
        [StringLength(20)]
        public string CustomerCode { get; set; }
        [StringLength(50)]
        public string CustomerName { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(50)]
        public string Department { get; set; }
        [StringLength(50)]
        public string Designation { get; set; }
        [StringLength(50)]
        public string Email { get; set; }
        [StringLength(10)]
        public string ContactNo { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
        public int? LevelOfAuthority { get; set; }
        [Required]
        [StringLength(20)]
        public string MainCustomerCode { get; set; }

        public IList<KeyValuePair<string, string>> GetValues()
        {
            return new List<KeyValuePair<string, string>>()
            {
                 new KeyValuePair<string, string>("Customer Code",CustomerCode),
                 new KeyValuePair<string, string>("Customer Name",CustomerName),
                 new KeyValuePair<string, string>("Name",Name),
                 new KeyValuePair<string, string>("Department",Department),
                 new KeyValuePair<string, string>("Designation",Designation),
                 new KeyValuePair<string, string>("Email",Email),
                 new KeyValuePair<string, string>("Contact No",ContactNo),
                 new KeyValuePair<string, string>("Active",Active.ToString()),
                 new KeyValuePair<string, string>("Deleted",Deleted.ToString()),
                 new KeyValuePair<string, string>("Level Of Authority",LevelOfAuthority.ToString()),
                 new KeyValuePair<string, string>("Main Customer Code",MainCustomerCode)

            };
        }
    }
}
