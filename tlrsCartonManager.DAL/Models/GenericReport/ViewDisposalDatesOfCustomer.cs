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
    public partial class ViewDisposalDatesOfCustomer : IGenericReportDataItem
    {
        [Column("Carton No")]
        public int CartonNo { get; set; }

        [Required]
        [Column("Customer Code")]
        [StringLength(20)]
        public string CustomerCode { get; set; }

        [Required]
        [Column("Name")]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; }

        [Required]
        [StringLength(50)]
        [Column("Status Confirmed")]
        public string StatusConfirmed { get; set; }

        [Column("Disposal Date")]
        public string DisposalDate { get; set; }

        [Column("Disposal Time Frame")]
        public string DisposalTimeFrame { get; set; }

        [Column("First In Date")]
        public string FirstInDate { get; set; }

        [Column("Last Transaction Date")]
        public string LastTransactionDate { get; set; }

        public IList<KeyValuePair<string, string>> GetValues()
        {
            return new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("Carton No", CartonNo.ToString()),
                new KeyValuePair<string, string>("Customer Code", CustomerCode),
                new KeyValuePair<string, string>("Name", Name),
                new KeyValuePair<string, string>("Status", Status),
                new KeyValuePair<string, string>("Status Confirmed", StatusConfirmed),
                new KeyValuePair<string, string>("Disposal Date", DisposalDate),
                new KeyValuePair<string, string>("Disposal Time Frame",DisposalTimeFrame),
                new KeyValuePair<string, string>("First In Date", FirstInDate),
                new KeyValuePair<string, string>("Last Transaction Date", LastTransactionDate)
            };
        }
    }
}
