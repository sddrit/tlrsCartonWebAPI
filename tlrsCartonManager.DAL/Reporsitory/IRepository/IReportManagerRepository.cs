using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tlrsCartonManager.DAL.Dtos;
using tlrsCartonManager.DAL.Helper;
using tlrsCartonManager.DAL.Models;
using tlrsCartonManager.DAL.Models.Report;
using tlrsCartonManager.DAL.Utility;

namespace tlrsCartonManager.DAL.Reporsitory.IRepository
{
    public interface IReportManagerRepository
    {
        Task<InventoryByCustomerReponse> GetInventoryByCustomer(int customerId,string woType, DateTime asAtDate, bool includeSubAccount);
        Task<IEnumerable<ViewPendingRequestPivot>> GetPendingRequestSummary(DateTime asAtDate);
        Task<IEnumerable<ViewPendingRequestDailyCollection>> GetDailyLogCollection(bool asAtToday ,DateTime fromDate, DateTime toDate, string route);
        Task<IEnumerable<ViewTobeDisposedCartonList>> GetToBeDisposedCartonList(string customerCode, bool includeSubAccount);
        Task<IEnumerable<ViewPendingRequest>> GetCartonsInPendingRequest(string customerCode, bool includeSubAccount);
        Task<IEnumerable<ViewCustomerTransaction>> GetCustomerTransactions(string customerCode,DateTime fromDate, DateTime toDate, bool includeSubAccount);
        Task<IEnumerable<ViewCartonsInLocation>> GetCartonsInLocation(string locationCode);
        Task<IEnumerable<RetentionTracker>> GetRetentionTracker(string customerCode,  DateTime asAtDate, bool includeSubAccount);
        Task<IEnumerable<RetentionTrackerDisposal>> GetRetentionTrackerDisposal(string customerCode, DateTime fromDate,DateTime toDate, bool includeSubAccount);
        Task<IEnumerable<RetrievalTracker>> GetRetrievalTracker(string customerCode,DateTime fromDate, DateTime toDate, bool includeSubAccount);
        Task<IEnumerable<LongOutstanding>> GetLongOutStanding(string customerCode, DateTime asAtDate, bool includeSubAccount);
        Task<IEnumerable<InventorySummaryAsAtdate>> GetnventorySummaryAsAtDate(DateTime asAtDate);
        Task<IEnumerable<CartonsInRCCollectionWoPending>> GetCartonsInRCCollectionWoPending(DateTime asAtDate, bool isAsAtDate);
        Task<IEnumerable<CartonsInRCCollectionWoPending>> GetCartonsInRCWoPending(DateTime asAtDate);
        Task<DailyPalletedSummary> GetDailyPalletedSummary(DateTime asAtDate, DateTime toDate, string locationCode);
        Task<IEnumerable<CartonsEnteredByCs>> CartonEnteredByCs(DateTime fromDate, DateTime toDate);
        Task<IEnumerable<ViewCustomerLoyality>> CustomerLoyality();
        Task<IEnumerable<DateWiseCollectionSummaryByCustomer>> DateWiseCollectionSummaryByCustomer(DateTime fromDate, DateTime toDate);

        Task<IEnumerable<InvoiceNotGeneratedCustomerList>> InvoiceNotGeneratedCustomerList(DateTime fromDate, DateTime toDate, string billingCycle);
    }
}
