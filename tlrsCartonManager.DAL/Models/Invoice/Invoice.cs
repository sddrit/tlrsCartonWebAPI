using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Models
{

    public class InvoicePostModel
    {
        public DateTime fromDate { get; set; }
        public DateTime toDate { get; set; }
        public string customerCode { get; set; }
        public string invoiceNo { get; set; }
    }

    [Keyless]
    [NotMapped]
    public class InvoiceResponse
    {
        public string MainInvoiceNo  { get; set; }   
        public InvoiceMainResponse InvoiceMainResponses { get; set; }
        public List<InvoiceSubResponse> InvoiceSubDetails { get; set; }
        public List<InvoiceSeparateResponse> InvoiceSeparateDetails { get; set; }
        public List<BranchWiseDetail> BranchWiseDetails { get; set; }

    }

    [Keyless]
    [NotMapped]
    public class InvoiceMainResponse
    {
        public InvoiceHeaderResponse InvoiceHeaders { get; set; }
        public List<InvoiceResponseDetail> InvoiceDetails { get; set; }
        public List<TransactionSummaryResponse> TransactionSummaryResponses { get; set; }
    }

    [Keyless]
    [NotMapped]
    public class InvoiceSubResponse
    {
        public InvoiceHeaderResponse InvoiceHeaders { get; set; }
        public List<InvoiceResponseDetail> InvoiceDetails { get; set; }

        public List<TransactionSummaryResponse> TransactionSummaryResponses { get; set; }
    }

    [Keyless]
    [NotMapped]
    public class InvoiceSeparateResponse
    {
        public InvoiceHeaderResponse InvoiceHeaders { get; set; }
        public List<InvoiceResponseDetail> InvoiceDetails { get; set; }

        public List<TransactionSummaryResponse> TransactionSummaryResponses { get; set; }
    }

    public class TransactionSummaryResponse
    {
        public string CustomerCode { get; set; }
        public string Name { get; set; }
        public string WoType { get; set; }
        public string RequestType { get; set; }
        public string RequestNo { get; set; }
        public string LastTransactionDate { get; set; }
        public string DocketNo { get; set; }
        public int? CartonCount { get; set; }
    }

    public class InvoiceHeaderResponse
    {
        [Key]

        public string InvoiceId { get; set; }
        public string CustomerCode { get; set; }
        public string Name { get; set; }
        public decimal? InvoiceValue { get; set; }
        public string CustomerAddress { get; set; }
        public string ContactPerson { get; set; }
        public string PoNo { get; set; }
        public string VatNo { get; set; }
        public string InvoiceTye { get; set; }
        public int FromDate { get; set; }

        public int ToDate { get; set; }

        public string SvatNo { get; set; }

        public decimal VatPercentage { get; set; }
    }

    [NotMapped]
    public class InvoiceResponseDetail
    {
        public int? InvoiceNoGroup { get; set; }
        public string Description { get; set; }
        public string CustomerCode { get; set; }
        public int? StorageType { get; set; }
        public int? LineId { get; set; }
        public int? Qty { get; set; }
        public decimal? Rate { get; set; }
        public decimal? Amount { get; set; }
        public decimal? Total { get; set; }
        public string InvoiceNo { get; set; }

    }

    public class BranchWiseHeader
    {
        public decimal? Discount { get; set; }
        public List<BranchWiseDetail>  BranchWiseDetails { get; set; }
    }

    public class BranchWiseDetail
    {
        public int Id { get; set; }
        public string CustomerCode { get; set; }
        public string Name { get; set; }

        public string Type { get; set; }

        public int? CartonQty { get; set; }

        public decimal? Amount { get; set; }
        
    }

    public class InvoiceModel
    {
        [Key]

        public string InvoiceId { get; set; }
        public string CustomerCode { get; set; }
        public string Name { get; set; }
        public decimal? InvoiceValue { get; set; }
        public int FromDate { get; set; }
        public int ToDate { get; set; }  

    }
}
