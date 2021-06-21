using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Utility
{

    public class ImportDataAlternativeNoStoredprocedure
    {
        public static string StoredProcedureName = "importDataAlternativeNo";
        public static List<string> StoredProcedureParameters = new List<string>()
        {
            "@userId",
            "@alternativeList",
            "@successCount"

        };
        public static List<string> StoredProcedureTypeNames = new List<string>()
        {
            "dbo.udtImportAlternativeNos",

        };
        public static string Sql = "EXEC " + StoredProcedureName + " " + string.Join(",", StoredProcedureParameters) + " OUTPUT ";
    }
    public class ImportDataDestructionDateStoredprocedure
    {
        public static string StoredProcedureName = "importDataDestructionDates";
        public static List<string> StoredProcedureParameters = new List<string>()
        {
            "@userId",
            "@disposalList",
            "@successCount"

        };
        public static List<string> StoredProcedureTypeNames = new List<string>()
        {
            "dbo.udtImportDestructionDateList",

        };
        public static string Sql = "EXEC " + StoredProcedureName + " " + string.Join(",", StoredProcedureParameters) + " OUTPUT ";
    }
}
