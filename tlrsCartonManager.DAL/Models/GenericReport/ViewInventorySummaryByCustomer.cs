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
    public partial class ViewInventorySummaryByCustomer : IGenericReportDataItem
    {
        [Required]
        [Column("Customer Code")]
        [StringLength(20)]
        public string CustomerCode { get; set; }

        [Required]
        [Column("Name")]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [Column("Address")]
        [StringLength(50)]
        public string Address1 { get; set; }

        [Column("Contact No")]
        [StringLength(50)]
        public string ContactTelephone1 { get; set; }

        [Column("Contact Name")]
        [StringLength(50)]
        public string ContactName { get; set; }

        [Column("Service Provided")]
        [StringLength(50)]
        public string ServiceProvided { get; set; }

        [Column("Empty Carton Count")]
        public int EmptyCartonCount { get; set; }

        [Column("Collection Carton Count")]
        public int CollectionCartonCount { get; set; }

        [Column("Retreival Carton Count")]
        public int RetreivalCartonCount { get; set; }

        [Column("PermOut Carton Count")]
        public int PermOutCartonCount { get; set; }

        [Column("Disposal Carton Count")]
        public int DisposalCartonCount { get; set; }


        public IList<KeyValuePair<string, string>> GetValues()
        {
            return new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("Customer Code", CustomerCode),
                new KeyValuePair<string, string>("Name", Name),
                new KeyValuePair<string, string>("Address", Address1),
                new KeyValuePair<string, string>("Contact No", ContactTelephone1),
                new KeyValuePair<string, string>("Contact Name", ContactName),
                new KeyValuePair<string, string>("Service Provided", ServiceProvided),
                new KeyValuePair<string, string>("Empty Carton Count", EmptyCartonCount.ToString()),
                new KeyValuePair<string, string>("Retreival Carton Count", RetreivalCartonCount.ToString()),
                new KeyValuePair<string, string>("Perm Out", PermOutCartonCount.ToString()),
                new KeyValuePair<string, string>("Dispose Carton Count", DisposalCartonCount.ToString())
            };
        }
    }
}
