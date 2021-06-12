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
           "@statementType",           
           "@udtMenuUserRoleAction"
        };
        public static List<string> StoredProcedureTypeNames = new List<string>()
        {
            
            "dbo.udtMenuUserRoleAction"

        };
        public static string Sql = "EXEC " + StoredProcedureName + " " + string.Join(",", StoredProcedureParameters) ;
    }
}

public class UserRoleByIdStoredProcedure
{
    public static string StoredProcedureName = "userRolePermissionSelect";
    public static List<string> StoredProcedureParameters = new List<string>()
        {
           "@id"
        };
   
    public static string Sql = "EXEC " + StoredProcedureName + " " + string.Join(",", StoredProcedureParameters);
}
