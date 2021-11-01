using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Utility.StoredProcedures
{
    public static class SequenceStoredProcedure
    {

        public static string StoredProcedureName = "sequenceInsertUpdateDelete";
        public static List<string> StoredProcedureParameters = new List<string>()
        {
            "@sequenceType",
            "@lastNo" ,
            "@active" ,
            "@currentSuffix"  ,
            "@requestTypeCode" ,
            "@statementType" ,
            "@userId" 
        };
        public static string Sql = "EXEC " + StoredProcedureName + " " + string.Join(",", StoredProcedureParameters);

    }

}
