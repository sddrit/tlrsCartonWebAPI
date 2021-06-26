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

    public static class LoginStoredProcedure
    {
        public static string StoredProcedureNameValidate = "userLoginValidate";
        public static string StoredProcedureNamePermission = "userPermission";

        public static List<string> StoredProcedureParameters = new List<string>()
        {           
            "@userName"
        };
        public static string SqlValidate = "EXEC " + StoredProcedureNameValidate + " " + string.Join(",", StoredProcedureParameters);
        public static string SqlPermission = "EXEC " + StoredProcedureNamePermission + " " + string.Join(",", StoredProcedureParameters);

    }

}
