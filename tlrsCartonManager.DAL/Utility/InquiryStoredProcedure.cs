using System;
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
}
