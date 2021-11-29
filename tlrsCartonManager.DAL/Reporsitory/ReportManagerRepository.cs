using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tlrsCartonManager.DAL.Reporsitory.IRepository;
using Microsoft.Data.SqlClient;
using tlrsCartonManager.DAL.Utility;
using System.Data;
using static tlrsCartonManager.DAL.Utility.Status;
using tlrsCartonManager.DAL.Extensions;
using tlrsCartonManager.DAL.Models;
using tlrsCartonManager.DAL.Models.Report;
using tlrsCartonManager.Core.Enums;

namespace tlrsCartonManager.DAL.Reporsitory
{
    public class ReportManagerRepository : IReportManagerRepository
    {
        private readonly tlrmCartonContext _tcContext;
        private readonly IMapper _mapper;
        private readonly ICustomerManagerRepository _customerManger;

        public ReportManagerRepository(tlrmCartonContext tccontext, IMapper mapper,
            ICustomerManagerRepository customerManger)
        {
            _tcContext = tccontext;
            _mapper = mapper;
            _customerManger = customerManger;

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
                includeSubAccount, out SqlParameter outParam);
            var inventoryList = await _tcContext.Set<InventoryByCustomer>().
              FromSqlRaw(InventoryByCustomerStoredProcedure.Sql, parms.ToArray()).ToListAsync();
            var totalRows = (int)outParam.Value;

            var postResponse = _mapper.Map<List<InventoryByCustomer>>(inventoryList);

            var inventorySummaryList = postResponse.GroupBy(t => t.WoType)
                           .Select(t => new InventoryByCustomerSummary()
                           {
                               WoType = t.Key,
                               CartonCount = t.Count()
                           }).ToList();

            var retrievalList = postResponse.Where(x => x.WoType.ToLower() == RequestTypes.retrieval.ToString()).ToList();

            var retreivalSummaryList = retrievalList.GroupBy(t => t.RetrievalType)
                          .Select(t => new InventoryByRetreivalSummary()
                          {
                              WoType = t.Key,
                              CartonCount = t.Count()
                          }).ToList();

            var inventoryByCustomer = new InventoryByCustomerReponse()
            {
                InventoryList = postResponse,
                InventorySummary = inventorySummaryList,
                RetreivalSummary = retreivalSummaryList

            };

            return inventoryByCustomer;
        }

        public async Task<IEnumerable<ViewPendingRequestPivot>> GetPendingRequestSummary(DateTime asAtDate)
        {
            return await _tcContext.ViewPendingRequestPivot.Where(x =>
            x.DeliveryDateInt <= Convert.ToInt32(asAtDate.ToString("yyyyMMdd"))).ToListAsync();
        }
        //public async Task<IEnumerable<ViewPendingRequestPivot>> GetPendingRequestSummary(DateTime asAtDate)
        //{
        //    return await _tcContext.ViewPendingRequests.Where(x =>
        //    x.DeliveryDateInt <= Convert.ToInt32(asAtDate.ToString("yyyyMMdd"))).ToListAsync();
        //}

        public async Task<IEnumerable<ViewPendingRequestDailyCollection>> GetDailyLogCollection(bool asAtToday, DateTime fromDate, DateTime toDate, string route)
        {
            if (asAtToday)
            {
                fromDate = new DateTime(1900, 01, 01);
                toDate = System.DateTime.Today;
            }
            if (string.IsNullOrEmpty(route))
            {
                return await _tcContext.ViewPendingRequestDailyCollections
                    .Where(x => x.DeliveryDateInt >= Convert.ToInt32(fromDate.ToString("yyyyMMdd")) &&
                    x.DeliveryDateInt <= Convert.ToInt32(toDate.ToString("yyyyMMdd")) && x.Collected == false).ToListAsync();

            }
            else
            {
                return await _tcContext.ViewPendingRequestDailyCollections
                    .Where(x => x.DeliveryDateInt >= Convert.ToInt32(fromDate.ToString("yyyyMMdd")) &&
                    x.DeliveryDateInt <= Convert.ToInt32(toDate.ToString("yyyyMMdd")) &&
                    x.DeliveryRoute == route && x.Collected == false
                    ).ToListAsync();
            }

        }
        public async Task<IEnumerable<ViewTobeDisposedCartonList>> GetToBeDisposedCartonList(string customerCode, bool includeSubAccount)
        {
            List<SqlParameter> parms = new List<SqlParameter>
            {
                new SqlParameter { ParameterName = ToBeDisposedCartonListStoredProcedure.StoredProcedureParameters[0].ToString(), Value = customerCode.AsDbValue() },
                new SqlParameter { ParameterName = ToBeDisposedCartonListStoredProcedure.StoredProcedureParameters[1].ToString(), Value = includeSubAccount.AsDbValue() }

            };

            return await _tcContext.Set<ViewTobeDisposedCartonList>().FromSqlRaw(ToBeDisposedCartonListStoredProcedure.Sql, parms.ToArray()).ToListAsync();
        }
        public async Task<IEnumerable<ViewPendingRequest>> GetCartonsInPendingRequest(string customerCode, bool includeSubAccount)
        {
            var customerId = _customerManger.GetCustomerId(customerCode);
            if (includeSubAccount)
                return await _tcContext.ViewPendingRequests.Where(x => x.MainCustomerCode == customerId).ToListAsync();
            else
                return await _tcContext.ViewPendingRequests.Where(x => x.CustomerCode == customerCode).ToListAsync();
        }

        public async Task<IEnumerable<ViewCustomerTransaction>> GetCustomerTransactions(string customerCode, DateTime fromDate, DateTime toDate, bool includeSubAccount)
        {
            var customerId = _customerManger.GetCustomerId(customerCode);
            int fDate = Convert.ToInt32(fromDate.ToString("yyyyMMdd"));
            int tDate = Convert.ToInt32(toDate.ToString("yyyyMMdd"));

            if (includeSubAccount)
                return await _tcContext.ViewCustomerTransactions.Where(x => x.MainCustomerCode == customerId &&
                x.LastTransactionDateInt >= fDate && x.LastTransactionDateInt <= tDate).ToListAsync();
            else
                return await _tcContext.ViewCustomerTransactions.Where(x => x.CustomerCode == customerCode &&
                x.LastTransactionDateInt >= fDate && x.LastTransactionDateInt <= tDate).ToListAsync();
        }

        public async Task<IEnumerable<ViewCartonsInLocation>> GetCartonsInLocation(string locationCode)
        {
            return await _tcContext.ViewCartonsInLocations.Where(x => x.LocationCode == locationCode).ToListAsync();
        }
        public async Task<IEnumerable<RetentionTracker>> GetRetentionTracker(string customerCode, DateTime asAtDate, bool includeSubAccount)
        {
            List<SqlParameter> parms = new List<SqlParameter>
            {
                new SqlParameter { ParameterName = RetentionTrackerStoredProcedure.StoredProcedureParameters[0].ToString(),
                    Value = customerCode.AsDbValue() },
                new SqlParameter { ParameterName = RetentionTrackerStoredProcedure.StoredProcedureParameters[1].ToString(),
                    Value = includeSubAccount.AsDbValue() },
                new SqlParameter { ParameterName = RetentionTrackerStoredProcedure.StoredProcedureParameters[2].ToString(),
                    Value = asAtDate.DateToInt().AsDbValue() }
            };

            return await _tcContext.Set<RetentionTracker>().FromSqlRaw(RetentionTrackerStoredProcedure.Sql, parms.ToArray()).ToListAsync();
        }
        public async Task<IEnumerable<RetentionTrackerDisposal>> GetRetentionTrackerDisposal(string customerCode,
            DateTime fromDate, DateTime toDate, bool includeSubAccount)
        {
            List<SqlParameter> parms = new List<SqlParameter>
            {
                new SqlParameter { ParameterName = RetentionTrackerDisposalStoredProcedure.StoredProcedureParameters[0].ToString(),
                    Value = customerCode.AsDbValue() },
                new SqlParameter { ParameterName = RetentionTrackerDisposalStoredProcedure.StoredProcedureParameters[1].ToString(),
                    Value = includeSubAccount.AsDbValue() },
                new SqlParameter { ParameterName = RetentionTrackerDisposalStoredProcedure.StoredProcedureParameters[2].ToString(),
                    Value = fromDate.DateToInt().AsDbValue() },
                new SqlParameter { ParameterName = RetentionTrackerDisposalStoredProcedure.StoredProcedureParameters[3].ToString(),
                    Value = toDate.DateToInt().AsDbValue() }
            };

            return await _tcContext.Set<RetentionTrackerDisposal>().FromSqlRaw(RetentionTrackerDisposalStoredProcedure.Sql, parms.ToArray()).ToListAsync();
        }
        public async Task<IEnumerable<RetrievalTracker>> GetRetrievalTracker(string customerCode,
           DateTime fromDate, DateTime toDate, bool includeSubAccount)
        {
            List<SqlParameter> parms = new List<SqlParameter>
            {
                new SqlParameter { ParameterName = RetreivalTrackerStoredProcedure.StoredProcedureParameters[0].ToString(),
                    Value = customerCode.AsDbValue() },
                new SqlParameter { ParameterName = RetreivalTrackerStoredProcedure.StoredProcedureParameters[1].ToString(),
                    Value = includeSubAccount.AsDbValue() },
                new SqlParameter { ParameterName = RetreivalTrackerStoredProcedure.StoredProcedureParameters[2].ToString(),
                    Value = fromDate.DateToInt().AsDbValue() },
                new SqlParameter { ParameterName = RetreivalTrackerStoredProcedure.StoredProcedureParameters[3].ToString(),
                    Value = toDate.DateToInt().AsDbValue() }
            };

            return await _tcContext.Set<RetrievalTracker>().FromSqlRaw(RetreivalTrackerStoredProcedure.Sql, parms.ToArray()).ToListAsync();
        }
        public async Task<IEnumerable<LongOutstanding>> GetLongOutStanding(string customerCode, DateTime asAtDate, bool includeSubAccount)
        {
            List<SqlParameter> parms = new List<SqlParameter>
            {
                new SqlParameter { ParameterName = LongOutStandingStoredProcedure.StoredProcedureParameters[0].ToString(),
                    Value = customerCode.AsDbValue() },
                new SqlParameter { ParameterName = LongOutStandingStoredProcedure.StoredProcedureParameters[1].ToString(),
                    Value = includeSubAccount.AsDbValue() },
                new SqlParameter { ParameterName = LongOutStandingStoredProcedure.StoredProcedureParameters[2].ToString(),
                    Value = asAtDate.DateToInt().AsDbValue() }
            };

            return await _tcContext.Set<LongOutstanding>().FromSqlRaw(LongOutStandingStoredProcedure.Sql, parms.ToArray()).ToListAsync();
        }

        public async Task<IEnumerable<InventorySummaryAsAtdate>> GetnventorySummaryAsAtDate(DateTime asAtDate)
        {
            List<SqlParameter> parms = new List<SqlParameter>
                {
                    new SqlParameter { ParameterName = InventorySummaryAsAtDate.StoredProcedureParameters[0].ToString(),
                        Value = asAtDate.DateToInt().AsDbValue() }
                };

            return await _tcContext.Set<InventorySummaryAsAtdate>().FromSqlRaw(InventorySummaryAsAtDate.Sql, parms.ToArray()).ToListAsync();

        }

        public async Task<IEnumerable<CartonsInRCCollectionWoPending>> GetCartonsInRCCollectionWoPending(DateTime asAtDate, bool isAsAtDate)
        {
            List<SqlParameter> parms = new List<SqlParameter>
                {
                    new SqlParameter { ParameterName = CartonsInRCCollectionWoPendingStoredProcedure.StoredProcedureParameters[0].ToString(),
                        Value = asAtDate.DateToInt().AsDbValue() },
                    new SqlParameter { ParameterName = CartonsInRCCollectionWoPendingStoredProcedure.StoredProcedureParameters[1].ToString(),
                        Value = isAsAtDate.AsDbValue() },

                };

            return await _tcContext.Set<CartonsInRCCollectionWoPending>().FromSqlRaw(CartonsInRCCollectionWoPendingStoredProcedure.Sql, parms.ToArray()).ToListAsync();

        }

        public async Task<IEnumerable<CartonsInRCCollectionWoPending>> GetCartonsInRCWoPending(DateTime asAtDate)
        {
            List<SqlParameter> parms = new List<SqlParameter>
                {
                    new SqlParameter { ParameterName = CartonsInRCWoPendingStoredProcedure.StoredProcedureParameters[0].ToString(),
                        Value = asAtDate.DateToInt().AsDbValue() }
                };

            return await _tcContext.Set<CartonsInRCCollectionWoPending>().FromSqlRaw(CartonsInRCWoPendingStoredProcedure.Sql, parms.ToArray()).ToListAsync();

        }
        public async Task<DailyPalletedSummary> GetDailyPalletedSummary(DateTime asAtDate,DateTime toDate, string locationCode)
        {
            List<SqlParameter> parms = new List<SqlParameter>
                {
                    new SqlParameter { ParameterName = DailyPalletedSummaryStoredProcedure.StoredProcedureParameters[0].ToString(),
                        Value = asAtDate.DateToInt().AsDbValue() },
                    new SqlParameter { ParameterName = DailyPalletedSummaryStoredProcedure.StoredProcedureParameters[1].ToString(),
                        Value = toDate.DateToInt().AsDbValue() },
                    new SqlParameter { ParameterName = DailyPalletedSummaryStoredProcedure.StoredProcedureParameters[2].ToString(),
                        Value = locationCode.AsDbValue() }
                };

            var result = await _tcContext.Set<DailyPalletedDetail>().FromSqlRaw(DailyPalletedSummaryStoredProcedure.Sql, parms.ToArray()).ToListAsync();

            var palletedSummaryByScannedUsers = result.GroupBy(t => t.ScannedUser)
                        .Select(t => new DailyPalletedSummaryByScannedUser()
                        {
                            ScannedUser = t.Key,
                            CartonCount = t.Count()
                        }).ToList();

            var dailyPalletedSummaryByWareHouse = result.GroupBy(t => t.WareHouseCode)
                      .Select(t => new DailyPalletedSummaryByWareHouse()
                      {
                          WareHouseCode = t.Key,
                          CartonCount = t.Count()
                      }).ToList();

            DailyPalletedSummary dailyPalletedSummary = new DailyPalletedSummary()
            {
                PalletedSummaryByScannedUsers = palletedSummaryByScannedUsers,
                PalletedSummaryByWareHouses = dailyPalletedSummaryByWareHouse,
                PalletedDetails = result
            };

            return dailyPalletedSummary;

        }

        public async Task<IEnumerable<CartonsEnteredByCs>> CartonEnteredByCs(DateTime fromDate, DateTime toDate)
        {
            List<SqlParameter> parms = new List<SqlParameter>
                {
                    new SqlParameter { ParameterName = CartonEnteredByCsStoredProcedure.StoredProcedureParameters[0].ToString(),
                        Value =fromDate.DateToInt().AsDbValue() },
                     new SqlParameter { ParameterName = CartonEnteredByCsStoredProcedure.StoredProcedureParameters[1].ToString(),
                        Value =toDate.DateToInt().AsDbValue() }
                };

            return await _tcContext.Set<CartonsEnteredByCs>().FromSqlRaw(CartonEnteredByCsStoredProcedure.Sql, parms.ToArray()).ToListAsync();

        }

        public async Task<IEnumerable<ViewCustomerLoyality>> CustomerLoyality()
        {
            return await _tcContext.ViewCustomerLoyalities.ToListAsync();

        }

        public async Task<IEnumerable<DateWiseCollectionSummaryByCustomer>> DateWiseCollectionSummaryByCustomer(DateTime fromDate, DateTime toDate)
        {
            List<SqlParameter> parms = new List<SqlParameter>
                {
                    new SqlParameter { ParameterName = DateWiseCollectionSummaryByCustomerStoredProcedure.StoredProcedureParameters[0].ToString(),
                        Value =fromDate.DateToInt().AsDbValue() },
                     new SqlParameter { ParameterName = DateWiseCollectionSummaryByCustomerStoredProcedure.StoredProcedureParameters[1].ToString(),
                        Value =toDate.DateToInt().AsDbValue() }
                };

            return await _tcContext.Set<DateWiseCollectionSummaryByCustomer>().FromSqlRaw(DateWiseCollectionSummaryByCustomerStoredProcedure.Sql, parms.ToArray()).ToListAsync();

        }

        public async Task<IEnumerable<InvoiceNotGeneratedCustomerList>> InvoiceNotGeneratedCustomerList(DateTime fromDate, DateTime toDate, string billingCycle)
        {
            List<SqlParameter> parms = new List<SqlParameter>
                {
                    new SqlParameter { ParameterName = InvoiceNotGeneratedCustomerListStoredProcedure.StoredProcedureParameters[0].ToString(),
                        Value =fromDate.DateToInt().AsDbValue() },
                     new SqlParameter { ParameterName = InvoiceNotGeneratedCustomerListStoredProcedure.StoredProcedureParameters[1].ToString(),
                        Value =toDate.DateToInt().AsDbValue() },
                     new SqlParameter { ParameterName = InvoiceNotGeneratedCustomerListStoredProcedure.StoredProcedureParameters[2].ToString(),
                        Value =billingCycle.AsDbValue() },
                };

            return await _tcContext.Set<InvoiceNotGeneratedCustomerList>().FromSqlRaw(InvoiceNotGeneratedCustomerListStoredProcedure.Sql, parms.ToArray()).ToListAsync();

        }
    }


   

}

