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
           "@searColumn",
           "@sortOrder",
           "@pageIndex",
           "@pageSize",
           "@totalRecords"
        };
        public static string Sql = "EXEC " + StoredProcedureName + " " + string.Join(",", StoredProcedureParameters) + " OUTPUT";
    }

    public static class UserStoredProcedureCustomerPortalSearch
    {
        public static string StoredProcedureName = "userSearchCustomerPortal";
        public static List<string> StoredProcedureParameters = new List<string>()
        {
           "@value",
           "@searColumn",
           "@sortOrder",
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
            "@createdId",
            "@passwordEncryptionMobile",
            "@lock"
        };
        public static string Sql = "EXEC " + StoredProcedureName + " " + string.Join(",", StoredProcedureParameters) ;
    }

    public static class LoginStoredProcedure
    {
        public static string StoredProcedureName = "userLoginValidate";
     

        public static List<string> StoredProcedureParameters = new List<string>()
        {           
            "@userName",
            "@hostName"
        };
        public static string Sql = "EXEC " + StoredProcedureName + " " + string.Join(",", StoredProcedureParameters);
      

    }

    public static class LoginPermissionStoredProcedure
    {
       
        public static string StoredProcedureName = "userPermission";

        public static List<string> StoredProcedureParameters = new List<string>()
        {
            "@userName"
        };
     
        public static string Sql = "EXEC " + StoredProcedureName + " " + string.Join(",", StoredProcedureParameters);

    }

    public static class LoginAttemptsUpdateStoredProcedure
    {

        public static string StoredProcedureName = "userLoginAttemptsInsertUpdate";

        public static List<string> StoredProcedureParameters = new List<string>()
        {
            "@userName",
            "@isReset"
        };

        public static string Sql = "EXEC " + StoredProcedureName + " " + string.Join(",", StoredProcedureParameters);

    }

    public static class UserPermissionOnAuthorized
    {

        public static string StoredProcedureName = "userPermissionOnAuthorized";

        public static List<string> StoredProcedureParameters = new List<string>()
        {
            "@id",
            "@submoduleName"

        };

        public static string Sql = "EXEC " + StoredProcedureName + " " + string.Join(",", StoredProcedureParameters);

    }

}
