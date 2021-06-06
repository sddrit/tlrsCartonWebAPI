using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Utility
{
    public static class UserStoredProcedureSearch
    {
        public static string StoredProcedureName = "userSearch";
        public static List<string> StoredProcedureParameters = new List<string>()
        {
           "@value",
           "@pageIndex",
           "@pageSize",
           "@totalRecords"
        };
        public static string Sql = "EXEC " + StoredProcedureName + " " + string.Join(",", StoredProcedureParameters) + " OUTPUT";
    }

    public static class UserInsertUpdateDeleteStoredProcedureSearch
    {
        public static string StoredProcedureName = "userInsertUpdateDelete";
        public static List<string> StoredProcedureParameters = new List<string>()
        {
            "@id",
            "@userName", 
            "@userFullName" ,
            "@empId" ,
            "@departmentId", 
            "@passwordHash",
            "@passwordSalt",
            "@roleId" ,
            "@email",
            "@active",
            "@statementType" ,
            "@createdId"
        };
        public static string Sql = "EXEC " + StoredProcedureName + " " + string.Join(",", StoredProcedureParameters) ;
    }

}
