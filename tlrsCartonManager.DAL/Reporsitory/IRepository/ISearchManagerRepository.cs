using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tlrsCartonManager.DAL.Helper;

namespace tlrsCartonManager.DAL.Reporsitory.IRepository
{
    public interface ISearchManagerRepository
    {
        List<SqlParameter> Search(string storedProcedure, string columnValue, int pageIndex, int pageSize, out SqlParameter outParam);

        List<SqlParameter> Search(string storedProcedure, string columnValue, string searchColumn, string sortOrder, int pageIndex, int pageSize, out SqlParameter outParam);
        List<SqlParameter> Search(string storedProcedure, string type, string columnValue, int pageIndex, int pageSize, out SqlParameter outParam);

        List<SqlParameter> Search(string storedProcedure, string type, string columnValue, string searchColumn, string sortOrder, int pageIndex, int pageSize, out SqlParameter outParam);

        List<SqlParameter> SearchFromTo(string storedProcedure, string fromValue, string toValue, int pageIndex, int pageSize, out SqlParameter outParam);

        List<SqlParameter> SearchFromToSearchBy(string storedProcedure, string fromValue, string toValue, string searchBy, string searchColumn, string sortOrder, int pageIndex, int pageSize, out SqlParameter outParam);

        List<SqlParameter> SearchFromToSearchByType(string storedProcedure, string fromValue, string toValue,
      string searchBy, int pageIndex, int pageSize, string type, out SqlParameter outParam);

        List<SqlParameter> SearchFromToSearchByType(string storedProcedure, string fromValue, string toValue,
       string searchBy, int pageIndex, int pageSize, string type, string searchColumn, string sortOrder, out SqlParameter outParam);

        List<SqlParameter> SearchSearchByTypeAndCustomerCode(string storedProcedure, string customerCode, string type,
             string searchBy, string searchColumn, string sortOrder, int pageIndex, int pageSize, out SqlParameter outParam);

    }
}
