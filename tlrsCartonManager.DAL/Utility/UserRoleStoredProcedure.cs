using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Utility
{
    public class UserRoleStoredProcedure
    {
        public static string StoredProcedureName = "userRoleInsertUpdateDelete";
        public static List<string> StoredProcedureParameters = new List<string>()
        {
           "@id",         
           "@userId",
           "@name",
           "@statementType",
           "@udtRolePermission"
        };
        public static List<string> StoredProcedureTypeNames = new List<string>()
        {

            "dbo.udtRolePermission"

        };
        public static string Sql = "EXEC " + StoredProcedureName + " " + string.Join(",", StoredProcedureParameters) ;
    }
}

public class UserRoleByIdStoredProcedure
{
    public static string StoredProcedureName = "rolePermissionSelect";
    public static List<string> StoredProcedureParameters = new List<string>()
        {
           "@id"
        };
   
    public static string Sql = "EXEC " + StoredProcedureName + " " + string.Join(",", StoredProcedureParameters);
}
