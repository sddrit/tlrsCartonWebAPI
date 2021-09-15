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
    public partial class ViewCartonOwnershipTransfer : IGenericReportDataItem
    {
        [Column("Carton No")]
        public int CartonNo { get; set; }
        [Required]
        [Column("From Customer Code")]
        [StringLength(20)]
        public string FromCustomerCode { get; set; }
        [Column("From Customer Name")]
        [StringLength(50)]
        public string FromCustomerName { get; set; }
        [Required]
        [Column("To Customer Code")]
        [StringLength(20)]
        public string ToCustomerCode { get; set; }
        [Column("To Customer Name")]
        [StringLength(50)]
        public string ToCustomerName { get; set; }
       
        [Column("Ownership Changed Date")]
        public int? OwnershipChangedDate { get; set; }
        [Column("Changed By")]
        [StringLength(100)]
        public string ChangedBy { get; set; }
        public IList<KeyValuePair<string, string>> GetValues()
        {
            return new List<KeyValuePair<string, string>>()
            {
             new KeyValuePair<string, string>("Carton No", CartonNo.ToString()),
             new KeyValuePair<string, string>("From Customer Code", FromCustomerCode),
             new KeyValuePair<string, string>("From Customer Name", FromCustomerName),
             new KeyValuePair<string, string>("To Customer Code", ToCustomerCode),
             new KeyValuePair<string, string>("To Customer Name",ToCustomerName),
             new KeyValuePair<string, string>("Ownership Changed Date",OwnershipChangedDate.ToString()),
             new KeyValuePair<string, string>("Changed By", ChangedBy)
            };

        }
    }
}
