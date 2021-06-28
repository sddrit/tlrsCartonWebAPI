using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Utility
{
    public static class CartonStoredProcedure
    {
        public static string StoredProcedureName = "cartonUpdate";
        public static List<string> StoredProcedureParameters = new List<string>()
        {
           "@cartonNo",
           "@alternativeCartonNo",
           "@customerCode",
           "@disposalDate",
           "@disposalTimeFrame",
           "@cartonType",
           "@userId"
        };
        public static string Sql = "EXEC " + StoredProcedureName + " " + string.Join(",", StoredProcedureParameters) ;

    }
    public static class CartonRequestStoredProcedure
    {
        public static string StoredProcedureName = "cartonInquiryRequestHistory";
        public static List<string> StoredProcedureParameters = new List<string>()
        {
           "@cartonNo"
        };
        public static string Sql = "EXEC " + StoredProcedureName + " " + string.Join(",", StoredProcedureParameters);

    }
}
