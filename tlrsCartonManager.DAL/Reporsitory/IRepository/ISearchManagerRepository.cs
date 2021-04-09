using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tlrsCartonManager.DAL.Helper;

namespace tlrsCartonManager.DAL.Reporsitory.IRepository
{
    public  interface ISearchManagerRepository
    {
        List<SqlParameter> Search(string storedProcedure, string columnValue, int pageIndex, int pageSize, out SqlParameter outParam);
        List<SqlParameter> Search(string storedProcedure, string type, string columnValue, int pageIndex, int pageSize, out SqlParameter outParam);
    }
}
