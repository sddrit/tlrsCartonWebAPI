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
            SearchStoredProcedure.StoredProcedureName = storedProcedure;
            List<SqlParameter> parms = new List<SqlParameter>
            {
               new SqlParameter { ParameterName = SearchStoredProcedure.StoredProcedureParameters[0].ToString(), Value = columnValue.AsDbValue()},
               new SqlParameter { ParameterName = SearchStoredProcedure.StoredProcedureParameters[1].ToString(), Value = pageIndex },
               new SqlParameter { ParameterName = SearchStoredProcedure.StoredProcedureParameters[2].ToString(), Value = pageSize },

            };
            outParam = new SqlParameter { ParameterName = SearchStoredProcedure.StoredProcedureParameters[3].ToString(), SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };
            parms.Add(outParam);
            return parms;

        }
  
    }
}
