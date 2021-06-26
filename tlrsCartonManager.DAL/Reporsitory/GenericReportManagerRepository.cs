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

        public IList<IList<KeyValuePair<string, string>>> GetReportData(GenericReportData model)
        {
            switch (model.ReportName)
            {
                case "GenerateInventorySummaryByCustomer":
                    var inventorySummaryData = GetSearchResult<ViewInventorySummaryByCustomer>(model);
                    return inventorySummaryData.Select(item => item.GetValues()).ToList();

                case "GenerateDisposalDatesOfCustomer":
                    var viewDisposalDatesOfCustomersData = GetSearchResult<ViewDisposalDatesOfCustomer>(model);
                    return viewDisposalDatesOfCustomersData.Select(item => item.GetValues()).ToList();

                case "GenerateRequestSummary":
                    var viewRequestSummaryData = GetSearchResult<ViewRequestSummary>(model);
                    return viewRequestSummaryData.Select(item => item.GetValues()).ToList();

                case "GenerateCustomerSummary":
                    var viewCustomerSummaryData = GetSearchResult<ViewCustomerSummary>(model);
                    return viewCustomerSummaryData.Select(item => item.GetValues()).ToList();

                case "GenerateCreatedPickList":
                    var viewCreatedPickListData = GetSearchResult<ViewCreatedPickList>(model);
                    return viewCreatedPickListData.Select(item => item.GetValues()).ToList();

                case "GeneratedCreatedInvoiceList":
                    var viewCreatedInvoiceListData = GetSearchResult<ViewCreatedInvoiceList>(model);
                    return viewCreatedInvoiceListData.Select(item => item.GetValues()).ToList();

                case "GenerateCustomerAuthorizationList":
                    var viewCustomerAuthorizationListData = GetSearchResult<ViewCustomerAuthorizationList>(model);
                    return viewCustomerAuthorizationListData.Select(item => item.GetValues()).ToList();

                default:
                    throw new Exception("Report not implemented");
            }
        }

        public List<T> GetSearchResult<T>(GenericReportData model) where T : class
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

            return _tcContext.Set<T>().FromSqlRaw(GenericReportStoredProcedure.Sql, parms.ToArray()).ToList();
        }

        public List<GenericReportColumn> GetReportColumns(string reportName)
        {
            List<SqlParameter> parms = new List<SqlParameter>
            {
               new SqlParameter { ParameterName = GenericReportColumnStoredProcedure.StoredProcedureParameters[0].ToString(), Value =reportName.AsDbValue()}

            };

            return _tcContext.Set<GenericReportColumn>().FromSqlRaw(GenericReportColumnStoredProcedure.Sql, parms.ToArray()).ToList();
        }
    }
}
