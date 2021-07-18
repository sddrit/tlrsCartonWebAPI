using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Utility.StoredProcedures
{
    public static class LocationStoredProcedure
    {

        public static string StoredProcedureName = "locationInsertUpdateDelete";
        public static List<string> StoredProcedureParameters = new List<string>()
        {
            "@code",
            "@name" ,
            "@active" ,
            "@rms1Location"  ,
            "@isVehicle" ,
            "@isRcLocation" ,
            "@isBay" ,
            "@warehouseCode" ,
            "@statementType",
            "@userId"
        };
        public static string Sql = "EXEC " + StoredProcedureName + " " + string.Join(",", StoredProcedureParameters);

    }

}
