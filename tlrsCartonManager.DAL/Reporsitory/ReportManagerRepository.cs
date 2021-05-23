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
            DateTime asAtDate, string reportType, bool includeSubAccount, out SqlParameter outParam)
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
              

            };
            outParam = new SqlParameter
            {
                ParameterName = InventoryByCustomerStoredProcedure.StoredProcedureParameters[5].ToString(),
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };
            parms.Add(outParam);
            return parms;

        }
        public async Task<InventoryByCustomerReponse> GetInventoryByCustomer(int customerId, string woType,
            DateTime asAtDate, bool includeSubAccount)
        {
            var parms = SendResponse(customerId, woType, asAtDate, InventoryReportTypes.Detail.ToString(),
                includeSubAccount,out SqlParameter outParam);
            var inventoryList = await _tcContext.Set<InventoryByCustomer>().
              FromSqlRaw(InventoryByCustomerStoredProcedure.Sql, parms.ToArray()).ToListAsync();
            var totalRows = (int)outParam.Value;
      
            var postResponse = _mapper.Map<List<InventoryByCustomer>>(inventoryList);           

            parms = SendResponse(customerId, woType, asAtDate, InventoryReportTypes.SummaryInventory.ToString(),
               includeSubAccount, out outParam);
            var inventorySummaryList = await _tcContext.Set<InventoryByCustomerSummary>().
             FromSqlRaw(InventoryByCustomerStoredProcedure.Sql, parms.ToArray()).ToListAsync();

            parms = SendResponse(customerId, woType, asAtDate, InventoryReportTypes.SummaryRetreival.ToString(),
              includeSubAccount ,out outParam);
            var retreivalSummaryList = await _tcContext.Set<InventoryByRetreivalSummary>().
             FromSqlRaw(InventoryByCustomerStoredProcedure.Sql, parms.ToArray()).ToListAsync();

            var inventoryByCustomer = new InventoryByCustomerReponse()
            {
                InventoryList = postResponse,
                InventorySummary = inventorySummaryList,
                RetreivalSummary = retreivalSummaryList

            };

            return inventoryByCustomer;
        }

        public async Task<IEnumerable<ViewPendingRequest>> GetPendingRequestSummary(DateTime asAtDate)
        {
           return await _tcContext.ViewPendingRequests.Where(x=>
           x.DeliveryDateInt<=Convert.ToInt32( asAtDate.ToString("yyyyMMdd"))).ToListAsync();
        }

        public async Task<IEnumerable<ViewPendingRequest>> GetDailyLogCollection(bool asAtToday,DateTime fromDate, DateTime toDate, string route)
        {          
            if(asAtToday)
            {

                fromDate = new DateTime(1900, 01, 01);
                toDate = System.DateTime.Today;

            }
            if (string.IsNullOrEmpty(route))
            {
                return await _tcContext.ViewPendingRequests
                    .Where(x => x.DeliveryDateInt >= Convert.ToInt32(fromDate.ToString("yyyyMMdd")) &&
                    x.DeliveryDateInt <= Convert.ToInt32(toDate.ToString("yyyyMMdd"))).ToListAsync();

            }
            else
            {
                return await _tcContext.ViewPendingRequests
                    .Where(x => x.DeliveryDateInt >= Convert.ToInt32(fromDate.ToString("yyyyMMdd")) &&
                    x.DeliveryDateInt <= Convert.ToInt32(toDate.ToString("yyyyMMdd"))&&
                    x.DeliveryRoute==route                    
                    ).ToListAsync();

            }

        }
    }
}

