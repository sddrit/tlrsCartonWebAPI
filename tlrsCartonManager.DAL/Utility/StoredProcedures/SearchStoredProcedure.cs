using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Utility
{
    public static class SearchStoredProcedure
    {
        public static int PageIndex = 1;
        public static int PageSize = 10;

        public static string StoredProcedureName { get; set; }
        public static List<string> StoredProcedureParameters
        {
            get
            {
                return new List<string>()
                {
                   "@value",
                   "@pageIndex",
                   "@pageSize",
                   "@totalRecords"
                };
            }          
        }
        public static string Sql
        {
            get
            { 
                return  "EXEC " + StoredProcedureName + " " + string.Join(",", StoredProcedureParameters) + " OUTPUT"; 
            } 
        }    

    }

    public static class SearchStoredProcedureByType
    {
        public static string StoredProcedureName { get; set; }
        public static List<string> StoredProcedureParameters
        {
            get
            {
                return new List<string>()
                {
                   "@type",
                   "@value",
                   "@pageIndex",
                   "@pageSize",
                   "@totalRecords"
                };
            }
        }
        public static string Sql
        {
            get
            {
                return "EXEC " + StoredProcedureName + " " + string.Join(",", StoredProcedureParameters) + " OUTPUT";
            }
        }

    }

    public static class SearchStoredProcedureFromTo
    {
        public static string StoredProcedureName { get; set; }
        public static List<string> StoredProcedureParameters
        {
            get
            {
                return new List<string>()
                {                  
                   "@valueFrom",
                   "@valueTo",
                   "@pageIndex",
                   "@pageSize",
                   "@totalRecords"
                };
            }
        }
        public static string Sql
        {
            get
            {
                return "EXEC " + StoredProcedureName + " " + string.Join(",", StoredProcedureParameters) + " OUTPUT";
            }
        }

    }
    public static class SearchStoredProcedureFromToSearchBy
    {
        public static string StoredProcedureName { get; set; }
        public static List<string> StoredProcedureParameters
        {
            get
            {
                return new List<string>()
                {
                   "@valueFrom",
                   "@valueTo",
                   "@searchBy",
                   "@pageIndex",
                   "@pageSize",
                   "@totalRecords"
                };
            }
        }
        public static string Sql
        {
            get
            {
                return "EXEC " + StoredProcedureName + " " + string.Join(",", StoredProcedureParameters) + " OUTPUT";
            }
        }

    }

    public static class SearchStoredProcedureFromToSearchByType
    {
        public static string StoredProcedureName { get; set; }
        public static List<string> StoredProcedureParameters
        {
            get
            {
                return new List<string>()
                {
                   "@valueFrom",
                   "@valueTo",
                   "@searchBy",
                   "@type",
                   "@pageIndex",
                   "@pageSize",
                   "@totalRecords"

                };
            }
        }
        public static string Sql
        {
            get
            {
                return "EXEC " + StoredProcedureName + " " + string.Join(",", StoredProcedureParameters) + " OUTPUT";
            }
        }

    }

}
