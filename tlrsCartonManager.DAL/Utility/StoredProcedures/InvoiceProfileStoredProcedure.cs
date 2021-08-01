using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Utility
{
    public class InvoiceProfileRateStoredProcedure
	{
		public static string StoredProcedureName = "invoiceTemplateHeaderCustomerRates";
		public static List<string> StoredProcedureParameters = new List<string>()
		{
			"@id",
			"@customerCode",
			"@statementType"

		};
		
		public static string Sql = "EXEC " + StoredProcedureName + " " + string.Join(",", StoredProcedureParameters);
	}

	public class InvoiceProfileHeaderStoredProcedure
	{
		public static string StoredProcedureName = "invoiceTemplateHeaderCustomerInsertUpdateDelete";
		public static List<string> StoredProcedureParameters = new List<string>()
		{
			"@id",
			"@description",
			"@customerCode",
			"@storageType",
			"@invoiceTypeCode",
			"@isSplitInvoice",
			"@userId",
			"@statementType",
			"@supportingDocs",
			"@active"

		};

		public static string Sql = "EXEC " + StoredProcedureName + " " + string.Join(",", StoredProcedureParameters);
	}

	public class InvoiceProfileRateInsertStoredProcedure
	{
		public static string StoredProcedureName = "invoiceTemplateRatesCustomerInsertUpdateDelete";
		public static List<string> StoredProcedureParameters = new List<string>()
		{
			"@id" ,
			"@customerCode",
			"@storageType",
			"@userId",
            "@udtInvoiceTemplateCustomer"
        };

		public static List<string> StoredProcedureTypeNames = new List<string>()
		{
			"dbo.udtInvoiceTemplateCustomer"
		};

		public static string Sql = "EXEC " + StoredProcedureName + " " + string.Join(",", StoredProcedureParameters);
	}

}
