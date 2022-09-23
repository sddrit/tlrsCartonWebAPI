using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tlrsCartonManager.DAL.Extensions;
using tlrsCartonManager.DAL.Helper;
using tlrsCartonManager.DAL.Utility;

namespace tlrsCartonManager.DAL.Reporsitory.IRepository
{
    public class SearchManagerRepository:ISearchManagerRepository
    {
        public List<SqlParameter> Search(string storedProcedure,string columnValue, int pageIndex, int pageSize,out SqlParameter outParam)
        {
            SearchStoredProcedureBasic.StoredProcedureName = storedProcedure;
            List<SqlParameter> parms = new List<SqlParameter>
            {
               new SqlParameter { ParameterName = SearchStoredProcedureBasic.StoredProcedureParameters[0].ToString(), Value = columnValue.AsDbValue()},
               new SqlParameter { ParameterName = SearchStoredProcedureBasic.StoredProcedureParameters[1].ToString(), Value = pageIndex },
               new SqlParameter { ParameterName = SearchStoredProcedureBasic.StoredProcedureParameters[2].ToString(), Value = pageSize },

            };
            outParam = new SqlParameter { ParameterName = SearchStoredProcedureBasic.StoredProcedureParameters[3].ToString(), SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };
            parms.Add(outParam);
            return parms;

        }

        public List<SqlParameter> Search(string storedProcedure, string columnValue, string searchColumn, string sortOrder, int pageIndex, int pageSize, out SqlParameter outParam)
        {
            SearchStoredProcedure.StoredProcedureName = storedProcedure;
            List<SqlParameter> parms = new List<SqlParameter>
            {
               new SqlParameter { ParameterName = SearchStoredProcedure.StoredProcedureParameters[0].ToString(), Value = columnValue.AsDbValue()},
               new SqlParameter { ParameterName = SearchStoredProcedure.StoredProcedureParameters[1].ToString(), Value = searchColumn.AsDbValue() },
               new SqlParameter { ParameterName = SearchStoredProcedure.StoredProcedureParameters[2].ToString(), Value = sortOrder.AsDbValue() },
               new SqlParameter { ParameterName = SearchStoredProcedure.StoredProcedureParameters[3].ToString(), Value = pageIndex },
               new SqlParameter { ParameterName = SearchStoredProcedure.StoredProcedureParameters[4].ToString(), Value = pageSize },

            };
            outParam = new SqlParameter { ParameterName = SearchStoredProcedure.StoredProcedureParameters[5].ToString(), SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };
            parms.Add(outParam);
            return parms;

        }

        public List<SqlParameter> Search(string storedProcedure,string type, string columnValue, int pageIndex, int pageSize, out SqlParameter outParam)
        {
            SearchStoredProcedureByTypeBase.StoredProcedureName = storedProcedure;
            List<SqlParameter> parms = new List<SqlParameter>
            {
               new SqlParameter { ParameterName = SearchStoredProcedureByTypeBase.StoredProcedureParameters[0].ToString(), Value = type.AsDbValue()},
               new SqlParameter { ParameterName = SearchStoredProcedureByTypeBase.StoredProcedureParameters[1].ToString(), Value = columnValue.AsDbValue()},
               new SqlParameter { ParameterName = SearchStoredProcedureByTypeBase.StoredProcedureParameters[2].ToString(), Value = pageIndex },
               new SqlParameter { ParameterName = SearchStoredProcedureByTypeBase.StoredProcedureParameters[3].ToString(), Value = pageSize },

            };
            outParam = new SqlParameter { ParameterName = SearchStoredProcedureByTypeBase.StoredProcedureParameters[4].ToString(), SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };
            parms.Add(outParam);
            return parms;
        }

        public List<SqlParameter> Search(string storedProcedure, string type, string columnValue, string searchColumn, string sortOrder,int pageIndex, int pageSize, out SqlParameter outParam)
        {
            SearchStoredProcedureByType.StoredProcedureName = storedProcedure;
            List<SqlParameter> parms = new List<SqlParameter>
            {
               new SqlParameter { ParameterName = SearchStoredProcedureByType.StoredProcedureParameters[0].ToString(), Value = type.AsDbValue()},
               new SqlParameter { ParameterName = SearchStoredProcedureByType.StoredProcedureParameters[1].ToString(), Value = columnValue.AsDbValue()},
               new SqlParameter { ParameterName = SearchStoredProcedureByType.StoredProcedureParameters[2].ToString(), Value = searchColumn.AsDbValue()},
               new SqlParameter { ParameterName = SearchStoredProcedureByType.StoredProcedureParameters[3].ToString(), Value = sortOrder.AsDbValue()},
               new SqlParameter { ParameterName = SearchStoredProcedureByType.StoredProcedureParameters[4].ToString(), Value = pageIndex },
               new SqlParameter { ParameterName = SearchStoredProcedureByType.StoredProcedureParameters[5].ToString(), Value = pageSize },

            };
            outParam = new SqlParameter { ParameterName = SearchStoredProcedureByType.StoredProcedureParameters[6].ToString(), SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };
            parms.Add(outParam);
            return parms;
        }

        public List<SqlParameter> SearchFromTo(string storedProcedure, string fromValue, string toValue, int pageIndex, int pageSize, out SqlParameter outParam)
        {
            SearchStoredProcedureFromTo.StoredProcedureName = storedProcedure;
            List<SqlParameter> parms = new List<SqlParameter>
            {
               new SqlParameter { ParameterName = SearchStoredProcedureFromTo.StoredProcedureParameters[0].ToString(), Value = fromValue.AsDbValue()},
               new SqlParameter { ParameterName = SearchStoredProcedureFromTo.StoredProcedureParameters[1].ToString(), Value = toValue.AsDbValue()},
               new SqlParameter { ParameterName = SearchStoredProcedureFromTo.StoredProcedureParameters[2].ToString(), Value = pageIndex },
               new SqlParameter { ParameterName = SearchStoredProcedureFromTo.StoredProcedureParameters[3].ToString(), Value = pageSize },

            };
            outParam = new SqlParameter { ParameterName = SearchStoredProcedureFromTo.StoredProcedureParameters[4].ToString(), SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };
            parms.Add(outParam);
            return parms;

        }
        public List<SqlParameter> SearchFromToSearchBy(string storedProcedure, string fromValue, string toValue,
           string searchBy, string searchColumn, string sortOrder, int pageIndex, int pageSize, out SqlParameter outParam)
        {
            SearchStoredProcedureFromToSearchBy.StoredProcedureName = storedProcedure;
            List<SqlParameter> parms = new List<SqlParameter>
            {
               new SqlParameter { ParameterName = SearchStoredProcedureFromToSearchBy.StoredProcedureParameters[0].ToString(), Value = fromValue.AsDbValue()},
               new SqlParameter { ParameterName = SearchStoredProcedureFromToSearchBy.StoredProcedureParameters[1].ToString(), Value = toValue.AsDbValue()},
               new SqlParameter { ParameterName = SearchStoredProcedureFromToSearchBy.StoredProcedureParameters[2].ToString(), Value = searchBy.AsDbValue()},
               new SqlParameter { ParameterName = SearchStoredProcedureFromToSearchBy.StoredProcedureParameters[3].ToString(), Value = searchColumn.AsDbValue() },
               new SqlParameter { ParameterName = SearchStoredProcedureFromToSearchBy.StoredProcedureParameters[4].ToString(), Value = sortOrder.AsDbValue() },
               new SqlParameter { ParameterName = SearchStoredProcedureFromToSearchBy.StoredProcedureParameters[5].ToString(), Value = pageIndex },
               new SqlParameter { ParameterName = SearchStoredProcedureFromToSearchBy.StoredProcedureParameters[6].ToString(), Value = pageSize },

            };
            outParam = new SqlParameter { ParameterName = SearchStoredProcedureFromToSearchBy.StoredProcedureParameters[7].ToString(), SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };
            parms.Add(outParam);
            return parms;

        }

        public List<SqlParameter> SearchFromToSearchByType(string storedProcedure, string fromValue, string toValue,
       string searchBy, int pageIndex, int pageSize,string type, out SqlParameter outParam)
        {
            SearchStoredProcedureFromToSearchByType.StoredProcedureName = storedProcedure;
            List<SqlParameter> parms = new List<SqlParameter>
            {
               new SqlParameter { ParameterName = SearchStoredProcedureFromToSearchByType.StoredProcedureParameters[0].ToString(), Value = fromValue.AsDbValue()},
               new SqlParameter { ParameterName = SearchStoredProcedureFromToSearchByType.StoredProcedureParameters[1].ToString(), Value = toValue.AsDbValue()},
               new SqlParameter { ParameterName = SearchStoredProcedureFromToSearchByType.StoredProcedureParameters[2].ToString(), Value = searchBy.AsDbValue()},
               new SqlParameter { ParameterName = SearchStoredProcedureFromToSearchByType.StoredProcedureParameters[3].ToString(), Value = type.AsDbValue()},
               new SqlParameter { ParameterName = SearchStoredProcedureFromToSearchByType.StoredProcedureParameters[4].ToString(), Value = pageIndex },
               new SqlParameter { ParameterName = SearchStoredProcedureFromToSearchByType.StoredProcedureParameters[5].ToString(), Value = pageSize },

            };
            outParam = new SqlParameter { ParameterName = SearchStoredProcedureFromToSearchByType.StoredProcedureParameters[6].ToString(), SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };
            parms.Add(outParam);
            return parms;

        }


        public List<SqlParameter> SearchFromToSearchByType(string storedProcedure, string fromValue, string toValue,
       string searchBy, int pageIndex, int pageSize, string type, string searchColumn, string sortOrder, out SqlParameter outParam)
        {
            SearchStoredProcedureFromToSearchByTypeSearchColumn.StoredProcedureName = storedProcedure;
            List<SqlParameter> parms = new List<SqlParameter>
            {
               new SqlParameter { ParameterName = SearchStoredProcedureFromToSearchByTypeSearchColumn.StoredProcedureParameters[0].ToString(), Value = fromValue.AsDbValue()},
               new SqlParameter { ParameterName = SearchStoredProcedureFromToSearchByTypeSearchColumn.StoredProcedureParameters[1].ToString(), Value = toValue.AsDbValue()},
               new SqlParameter { ParameterName = SearchStoredProcedureFromToSearchByTypeSearchColumn.StoredProcedureParameters[2].ToString(), Value = searchBy.AsDbValue()},
               new SqlParameter { ParameterName = SearchStoredProcedureFromToSearchByTypeSearchColumn.StoredProcedureParameters[3].ToString(), Value = type.AsDbValue()},
               new SqlParameter { ParameterName = SearchStoredProcedureFromToSearchByTypeSearchColumn.StoredProcedureParameters[4].ToString(), Value = searchColumn.AsDbValue() },
               new SqlParameter { ParameterName = SearchStoredProcedureFromToSearchByTypeSearchColumn.StoredProcedureParameters[5].ToString(), Value = sortOrder.AsDbValue() },
               new SqlParameter { ParameterName = SearchStoredProcedureFromToSearchByTypeSearchColumn.StoredProcedureParameters[6].ToString(), Value = pageIndex },
               new SqlParameter { ParameterName = SearchStoredProcedureFromToSearchByTypeSearchColumn.StoredProcedureParameters[7].ToString(), Value = pageSize },

            };
            outParam = new SqlParameter { ParameterName = SearchStoredProcedureFromToSearchByTypeSearchColumn.StoredProcedureParameters[8].ToString(), SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };
            parms.Add(outParam);
            return parms;

        }


        public List<SqlParameter> SearchSearchByTypeAndCustomerCode(string storedProcedure,string customerCode, string type, 
     string searchBy, string searchColumn, string sortOrder, int pageIndex, int pageSize,  out SqlParameter outParam)
        {
            SearchStoredProcedureCustomerCodeByType.StoredProcedureName = storedProcedure;
            List<SqlParameter> parms = new List<SqlParameter>
            {
               new SqlParameter { ParameterName = SearchStoredProcedureCustomerCodeByType.StoredProcedureParameters[0].ToString(), Value = customerCode.AsDbValue()},
               new SqlParameter { ParameterName = SearchStoredProcedureCustomerCodeByType.StoredProcedureParameters[1].ToString(), Value = type.AsDbValue()},
               new SqlParameter { ParameterName = SearchStoredProcedureCustomerCodeByType.StoredProcedureParameters[2].ToString(), Value = searchBy.AsDbValue()},
               new SqlParameter { ParameterName = SearchStoredProcedureCustomerCodeByType.StoredProcedureParameters[3].ToString(), Value = searchColumn.AsDbValue()},
               new SqlParameter { ParameterName = SearchStoredProcedureCustomerCodeByType.StoredProcedureParameters[4].ToString(), Value = sortOrder.AsDbValue() },
                new SqlParameter { ParameterName = SearchStoredProcedureCustomerCodeByType.StoredProcedureParameters[5].ToString(), Value = pageIndex },
               new SqlParameter { ParameterName = SearchStoredProcedureCustomerCodeByType.StoredProcedureParameters[6].ToString(), Value = pageSize },

            };
            outParam = new SqlParameter { ParameterName = SearchStoredProcedureCustomerCodeByType.StoredProcedureParameters[7].ToString(), SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };
            parms.Add(outParam);
            return parms;

        }


    }
}
