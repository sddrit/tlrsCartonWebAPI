using AutoMapper;
using tlrsCartonManager.DAL.Reporsitory;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using tlrsCartonManager.DAL.Reporsitory.IRepository;
using tlrsCartonManager.DAL.Dtos;
using Microsoft.Data.SqlClient;
using tlrsCartonManager.DAL.Utility;
using System.Data;
using tlrsCartonManager.DAL.Helper;
using static tlrsCartonManager.DAL.Utility.Status;
using tlrsCartonManager.DAL.Extensions;
using Newtonsoft.Json;
using tlrsCartonManager.DAL.Models;
using tlrsCartonManager.DAL.Models.Report;

namespace tlrsCartonManager.DAL.Reporsitory
{
    public class ReportManagerRepository : IReportManagerRepository
    {
        private readonly tlrmCartonContext _tcContext;
        private readonly IMapper _mapper;


        public ReportManagerRepository(tlrmCartonContext tccontext, IMapper mapper)
        {
            _tcContext = tccontext;
            _mapper = mapper;

        }

        private List<SqlParameter> SendResponse(int customerId, string woType,
            DateTime asAtDate, string reportType, bool includeSubAccount, int pageIndex, int pageSize, out SqlParameter outParam)
        {

            List<SqlParameter> parms = new List<SqlParameter>
            {
               new SqlParameter { ParameterName = InventoryByCustomerStoredProcedure.StoredProcedureParameters[0].ToString(),
                   Value = customerId.AsDbValue()},
               new SqlParameter { ParameterName = InventoryByCustomerStoredProcedure.StoredProcedureParameters[1].ToString(),
                   Value = woType.AsDbValue()},
               new SqlParameter { ParameterName = InventoryByCustomerStoredProcedure.StoredProcedureParameters[2].ToString(),
                   Value = asAtDate.AsDbValue()},
               new SqlParameter { ParameterName = InventoryByCustomerStoredProcedure.StoredProcedureParameters[3].ToString(),
                   Value = includeSubAccount.AsDbValue()},
               new SqlParameter { ParameterName = InventoryByCustomerStoredProcedure.StoredProcedureParameters[4].ToString(),
                   Value = reportType.AsDbValue()},
               new SqlParameter { ParameterName = InventoryByCustomerStoredProcedure.StoredProcedureParameters[5].ToString(),
                   Value = pageIndex },
               new SqlParameter { ParameterName = InventoryByCustomerStoredProcedure.StoredProcedureParameters[6].ToString(),
                   Value = pageSize },

            };
            outParam = new SqlParameter
            {
                ParameterName = InventoryByCustomerStoredProcedure.StoredProcedureParameters[7].ToString(),
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };
            parms.Add(outParam);
            return parms;

        }
        public async Task<InventoryByCustomerReponse> GetInventoryByCustomer(int customerId, string woType,
            DateTime asAtDate, bool includeSubAccount, int pageIndex, int pageSize)
        {
            var parms = SendResponse(customerId, woType, asAtDate, InventoryReportTypes.Detail.ToString(),
                includeSubAccount, pageIndex, pageSize, out SqlParameter outParam);
            var inventoryList = await _tcContext.Set<InventoryByCustomer>().
              FromSqlRaw(InventoryByCustomerStoredProcedure.Sql, parms.ToArray()).ToListAsync();
            var totalRows = (int)outParam.Value;
            #region paging
            var postResponse = _mapper.Map<List<InventoryByCustomer>>(inventoryList);

            var paginationResponse = new PagedResponse<InventoryByCustomer>
            {
                Data = postResponse,
                pageNumber = pageIndex,
                pageSize = pageSize,
                totalCount = totalRows,
                totalPages = (int)Math.Ceiling(totalRows / (double)pageSize),

            };
            #endregion

            parms = SendResponse(customerId, woType, asAtDate, InventoryReportTypes.SummaryInventory.ToString(),
               includeSubAccount, pageIndex, pageSize, out outParam);
            var inventorySummaryList = await _tcContext.Set<InventoryByCustomerSummary>().
             FromSqlRaw(InventoryByCustomerStoredProcedure.Sql, parms.ToArray()).ToListAsync();

            parms = SendResponse(customerId, woType, asAtDate, InventoryReportTypes.SummaryRetreival.ToString(),
              includeSubAccount, pageIndex, pageSize, out outParam);
            var retreivalSummaryList = await _tcContext.Set<InventoryByRetreivalSummary>().
             FromSqlRaw(InventoryByCustomerStoredProcedure.Sql, parms.ToArray()).ToListAsync();

            var inventoryByCustomer = new InventoryByCustomerReponse()
            {
                InventoryList = paginationResponse,
                InventorySummary = inventorySummaryList,
                RetreivalSummary = retreivalSummaryList

            };

            return inventoryByCustomer;
        }


    }
}

