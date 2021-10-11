using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
    [Table("Company")]
    public partial class Company
    {
        [Key]
        [Column("companyId")]
        public int CompanyId { get; set; }
        [Required]
        [Column("companyName")]
        [StringLength(100)]
        public string CompanyName { get; set; }
        [Required]
        [Column("address1")]
        [StringLength(50)]
        public string Address1 { get; set; }
        [Required]
        [Column("address2")]
        [StringLength(50)]
        public string Address2 { get; set; }
        [Required]
        [Column("address3")]
        [StringLength(50)]
        public string Address3 { get; set; }
        [Required]
        [Column("country")]
        [StringLength(50)]
        public string Country { get; set; }
        [Required]
        [Column("tel")]
        [StringLength(50)]
        public string Tel { get; set; }
        [Required]
        [Column("fax")]
        [StringLength(50)]
        public string Fax { get; set; }
        [Required]
        [Column("email")]
        [StringLength(50)]
        public string Email { get; set; }
        [Required]
        [Column("vatNo")]
        [StringLength(50)]
        public string VatNo { get; set; }
        [Required]
        [Column("nbtNo")]
        [StringLength(50)]
        public string NbtNo { get; set; }
        [Required]
        [Column("svatNo")]
        [StringLength(50)]
        public string SvatNo { get; set; }

        [Column("tenantCode")]        
        public string TenantCode { get; set; }        

        [Column("accountMangerName")]
        public string AccountMangerName { get; set; }

        [Column("accountManagerEmail")]
        public string AccountManagerEmail { get; set; }

        [Column("bankAccountNo")]
        public string BankAccountNo { get; set; }

        [Column("bankName")]
        public string BankName { get; set; }

        [Column("branchName")]
        public string BranchName { get; set; }
    }
}
