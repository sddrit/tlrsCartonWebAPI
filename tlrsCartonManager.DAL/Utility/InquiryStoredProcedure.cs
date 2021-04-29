﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Utility
{
    public class InquiryDocketByCartonStoredProcedure
    {
        
        public static string StoredProcedureName = "docketInquiryByCartonNo";
        public static List<string> StoredProcedureParameters = new List<string>()
        {
           "@cartonNo"          
        };
            public static string Sql = "EXEC " + StoredProcedureName + " " + string.Join(",", StoredProcedureParameters) ;
        
    }
    public class InquiryOperationOverviewStoredProcedure
    {

        public static string StoredProcedureName = "operationOverview";
        public static List<string> StoredProcedureParameters = new List<string>()
        {
           "@date",
           "@criteria"
        };
        public static string Sql = "EXEC " + StoredProcedureName + " " + string.Join(",", StoredProcedureParameters);

    }
    public class InquiryOperationOverviewByWoTypeStoredProcedure
    {

        public static string StoredProcedureName = "operationOverviewByWoType";
        public static List<string> StoredProcedureParameters = new List<string>()
        {
           "@woType",
           "@deliveryDate"
        };
        public static string Sql = "EXEC " + StoredProcedureName + " " + string.Join(",", StoredProcedureParameters);

    }
    public class InquiryOperationOverviewByUserLocationStoredProcedure
    {

        public static string StoredProcedureName = "operationOverviewByUserLocation";
        public static List<string> StoredProcedureParameters = new List<string>()
        {
            "@date",
            "@user",
            "@locationCode",
            "@isRcLocation",
            "@isVehicle",
            "@searchValue",
            "@pageIndex",
            "@pageSize",
            "@totalRecords"
        };
        public static string Sql = "EXEC " + StoredProcedureName + " " + string.Join(",", StoredProcedureParameters) + " OUTPUT";

    }
}
