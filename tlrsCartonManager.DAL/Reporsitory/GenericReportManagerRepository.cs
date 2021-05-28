using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using tlrsCartonManager.DAL.Dtos;
using tlrsCartonManager.DAL.Extensions;
using tlrsCartonManager.DAL.Helper;
using tlrsCartonManager.DAL.Models;
using tlrsCartonManager.DAL.Models.Report;
using tlrsCartonManager.DAL.Reporsitory.IRepository;
using tlrsCartonManager.DAL.Utility;
using static tlrsCartonManager.DAL.Utility.Status;

namespace tlrsCartonManager.DAL.Reporsitory
{
    public class GenericReportManagerRepository : IGenericReportManagerRepository
    {

        private readonly tlrmCartonContext _tcContext;
        private readonly IMapper _mapper;

        public GenericReportManagerRepository(tlrmCartonContext tccontext, IMapper mapper)
        {
            _tcContext = tccontext;
            _mapper = mapper;
            
        }        
        public  object GetReportData(GenericReportData model)
        {           
            GenericViewNames gv;
            GenericViewNames.TryParse(_tcContext.MenuModels.Where(x => x.ReportName == model.ReportName)?.FirstOrDefault()?.SqlObjectName, out gv);            
            switch (gv)
            {
                case
                    GenericViewNames.viewInventorySummaryByCustomer:
                    return GetSearchResult<ViewInventorySummaryByCustomer>(model);
                case
                    GenericViewNames.viewDisposalDatesOfCustomer:
                    return GetSearchResult<ViewDisposalDatesOfCustomer>(model);
                default:
                    return new object();
            }
        }
        public  List<T> GetSearchResult<T>(GenericReportData model) where T :class 
        {
            List<SqlParameter> parms = new List<SqlParameter>
            {
               new SqlParameter { ParameterName = GenericReportStoredProcedure.StoredProcedureParameters[0].ToString(), Value =model.ReportName.AsDbValue()},
               new SqlParameter
               {
                    ParameterName = GenericReportStoredProcedure.StoredProcedureParameters[1].ToString(),
                    TypeName = GenericReportStoredProcedure.StoredProcedureTypeNames[0].ToString(),
                    SqlDbType = SqlDbType.Structured,
                    Value = model.ReportFilters.ToList().ToDataTable()
               }
            };           

          return  _tcContext.Set<T>().FromSqlRaw(GenericReportStoredProcedure.Sql, parms.ToArray()).ToList();     
        }
        public List<GenericReportColumn> GetReportColumns(string reportName) 
        {
            List<SqlParameter> parms = new List<SqlParameter>
            {
               new SqlParameter { ParameterName = GenericReportColumnStoredProcedure.StoredProcedureParameters[0].ToString(), Value =reportName.AsDbValue()}
              
            };
            return  _tcContext.Set<GenericReportColumn>().FromSqlRaw(GenericReportColumnStoredProcedure.Sql, parms.ToArray()).ToList();
        }
    }       
}
