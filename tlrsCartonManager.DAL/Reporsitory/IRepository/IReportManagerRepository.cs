﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tlrsCartonManager.DAL.Dtos;
using tlrsCartonManager.DAL.Helper;
using tlrsCartonManager.DAL.Models;
using tlrsCartonManager.DAL.Models.Report;

namespace tlrsCartonManager.DAL.Reporsitory.IRepository
{
    public interface IReportManagerRepository
    {

        Task<InventoryByCustomerReponse> GetInventoryByCustomer(int customerId,
            string woType, DateTime asAtDate, bool includeSubAccount);
        Task<IEnumerable<ViewPendingRequest>> GetPendingRequestSummary(DateTime asAtDate);
        Task<IEnumerable<ViewPendingRequest>> GetDailyLogCollection(bool asAtToday ,DateTime fromDate, DateTime toDate, string route);

        Task<IEnumerable<ViewTobeDisposedCartonList>> GetToBeDisposedCartonList(string customerCode, bool includeSubAccount);
    }
}
