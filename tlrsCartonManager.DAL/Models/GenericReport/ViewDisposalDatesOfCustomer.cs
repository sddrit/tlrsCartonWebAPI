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
        [Column("cartonNo")]
        public int CartonNo { get; set; }
        [Required]
        [Column("customerCode")]
        [StringLength(20)]
        public string CustomerCode { get; set; }
        [Required]
        [Column("name")]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(50)]
        public string Status { get; set; }
        [Required]
        [StringLength(50)]
        public string StatusConfirmed { get; set; }
        [Column("disposalDate")]
        public int? DisposalDate { get; set; }
        [Column("disposalTimeFrame")]
        public int? DisposalTimeFrame { get; set; }
        [Column("firstInDate")]
        public int FirstInDate { get; set; }
        [Column("lastTransactionDate")]
        public int LastTransactionDate { get; set; }

        public IList<KeyValuePair<string, string>> GetValues()
        {
            return new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("Carton No", CartonNo.ToString()),
                new KeyValuePair<string, string>("Customer Code", CustomerCode),
                new KeyValuePair<string, string>("Name", Name),
                new KeyValuePair<string, string>("Status", Status),
                new KeyValuePair<string, string>("Status Confirmed", StatusConfirmed),
                new KeyValuePair<string, string>("Disposal Date", DisposalDate.HasValue ? DisposalDate.ToString() : string.Empty),
                new KeyValuePair<string, string>("Disposal Time",
                    DisposalTimeFrame.HasValue ? DisposalTimeFrame.ToString() : string.Empty),
                new KeyValuePair<string, string>("First In Date", FirstInDate.ToString()),
                new KeyValuePair<string, string>("Last Transaction Date", LastTransactionDate.ToString())
            };
        }
    }
}
