using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Utility
{
    public class InvoiceStoredProcedure
	{
		public static string StoredProcedureName = "invoiceCalculation";
		public static List<string> StoredProcedureParameters = new List<string>()
		{
			"@fromDate",
			"@toDate",
			"@customerCode",
			"@userId",
			"@invoiceNo",
			"@statementType",
			"@isSubInvoice"

		};		
		public static string Sql = "EXEC " + StoredProcedureName + " " + string.Join(",", StoredProcedureParameters);
	}

	public class InvoiceBrachWiseStoredProcedure
	{
		public static string StoredProcedureName = "invoiceCalculationBranchWiseReport";
		public static List<string> StoredProcedureParameters = new List<string>()
		{						
			"@invoiceNo"

		};
		public static string Sql = "EXEC " + StoredProcedureName + " " + string.Join(",", StoredProcedureParameters) ;
	}

	public class InvoiceValidationStoredProcedure
	{
		public static string StoredProcedureName = "invoiceCalculationValidate";
		public static List<string> StoredProcedureParameters = new List<string>()
		{
			"@fromDate",
			"@toDate",
			"@customerCode",
			"@invoiceNo",
			"@isSubInvoice",
			"@isTransactionSummary"

		};
		public static string Sql = "EXEC " + StoredProcedureName + " " + string.Join(",", StoredProcedureParameters);
	}



	public class InvoiceTransactionSummaryStoredProcedure
	{
		public static string StoredProcedureName = "invoiceCalculationTransactionSummaryReport";
		public static List<string> StoredProcedureParameters = new List<string>()
		{
			"@fromDate",
			"@toDate",
			"@invoiceNo",
			"@separteTransactionSummary",
			"@customerCode"

		};
		public static string Sql = "EXEC " + StoredProcedureName + " " + string.Join(",", StoredProcedureParameters);
	}

	public class InvoiceStoredProcedureById
	{
		public static string StoredProcedureName = "invoicesearchbyid";
		public static List<string> StoredProcedureParameters = new List<string>()
		{
			"@invoiceId"

		};
		public static string Sql = "EXEC " + StoredProcedureName + " " + string.Join(",", StoredProcedureParameters);
	}
	public class InvoiceConfirmationByRequestNoStoredProcedure
	{
		public static string StoredProcedureName = "invoiceConfirmationDetailByRequestNo";
		public static List<string> StoredProcedureParameters = new List<string>()
		{
			"@requestNo"		

		};
		public static string Sql = "EXEC " + StoredProcedureName + " " + string.Join(",", StoredProcedureParameters);
	}
	public class InvoiceConfirmationStoredProcedure
	{
		public static string StoredProcedureName = "invoiceConfirmationInsert";
		public static List<string> StoredProcedureParameters = new List<string>()
		{
			"@invoiceConfirmation"

		};
		public static List<string> StoredProcedureTypeNames = new List<string>()
		{
			"dbo.udtInvoiceConfirmation",

		};

		public static string Sql = "EXEC " + StoredProcedureName + " " + string.Join(",", StoredProcedureParameters);
	}
	public class InvoiceDisaprroveStoredProcedure
	{
		public static string StoredProcedureName = "invoiceDisapprove";
		public static List<string> StoredProcedureParameters = new List<string>()
		{
			"@requestNo",
			"@reason",
			"@userId"

		};
		public static string Sql = "EXEC " + StoredProcedureName + " " + string.Join(",", StoredProcedureParameters);
	}
	public class InvoiceDisaprroveValidateStoredProcedure
	{
		public static string StoredProcedureName = "invoiceDisConfirmationValidateSearch";
		public static List<string> StoredProcedureParameters = new List<string>()
		{
			"@requestNo"

		};
		public static string Sql = "EXEC " + StoredProcedureName + " " + string.Join(",", StoredProcedureParameters);
	}
}
