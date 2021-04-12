﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Utility
{
    public class InvoiceStoredProcedure
	{
		public static string StoredProcedureName = "invoiceInsertUpdateDelete";
		public static List<string> StoredProcedureParameters = new List<string>()
		{
			"@fromDate",
			"@toDate",
			"@customerId"

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
}
