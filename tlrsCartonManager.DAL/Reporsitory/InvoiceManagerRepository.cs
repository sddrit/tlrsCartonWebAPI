﻿using AutoMapper;
using tlrsCartonManager.DAL.Reporsitory;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using tlrsCartonManager.DAL.Reporsitory.IRepository;
using tlrsCartonManager.DAL.Models;
using tlrsCartonManager.DAL.Dtos;
using Microsoft.Data.SqlClient;
using tlrsCartonManager.DAL.Utility;
using System.Data;
using tlrsCartonManager.DAL.Helper;
using static tlrsCartonManager.DAL.Utility.Status;
using tlrsCartonManager.DAL.Extensions;
using Newtonsoft.Json;


namespace tlrsCartonManager.DAL.Reporsitory
{
    public class InvoiceManagerRepository : IInvoiceManagerRepository
    {
        private readonly tlrmCartonContext _tcContext;
        private readonly IMapper _mapper;
        private readonly ISearchManagerRepository _searchManager;

        public InvoiceManagerRepository(tlrmCartonContext tccontext, IMapper mapper, ISearchManagerRepository searchManager)
        {
            _tcContext = tccontext;
            _mapper = mapper;
            _searchManager = searchManager;
        }

        #region Invoicing
        public async Task<InvoiceHeaderDto> GetInvoiceList(string invoiceNo)
        {
            var carton = _mapper.Map<InvoiceHeaderDto>(await _tcContext.InvoiceHeaders.
                          Include(x => x.InvoiceDetails).
                          Where(x => x.InvoiceId == invoiceNo).FirstOrDefaultAsync());
            return carton;

        }
        public async Task<PagedResponse<InvoiceSearchDto>> SearchInvoice(string searchText, int pageIndex, int pageSize)
        {
            List<SqlParameter> parms = _searchManager.Search("invoiceSearch", searchText, pageIndex, pageSize, out SqlParameter outParam);
            var cartonList = await _tcContext.Set<InvoiceSearch>().FromSqlRaw(SearchStoredProcedure.Sql, parms.ToArray()).ToListAsync();
            var totalRows = (int)outParam.Value;
            #region paging
            var postResponse = _mapper.Map<List<InvoiceSearchDto>>(cartonList);

            var paginationResponse = new PagedResponse<InvoiceSearchDto>
            {
                Data = postResponse,
                pageNumber = pageIndex,
                pageSize = pageSize,
                totalCount = totalRows,
                totalPages = (int)Math.Ceiling(totalRows / (double)pageSize),

            };
            #endregion

            return paginationResponse;
        }
        public TableResponse<InvoiceReturn> CreateInvoice(int fromDate, int toDate, int customerId)
        {

            #region Sql Parameter loading
            List<SqlParameter> parms = new List<SqlParameter>
            {

                new SqlParameter { ParameterName = InvoiceStoredProcedure.StoredProcedureParameters[0].ToString(), Value = fromDate.AsDbValue() },
                new SqlParameter { ParameterName = InvoiceStoredProcedure.StoredProcedureParameters[1].ToString(), Value = toDate.AsDbValue() },
                new SqlParameter { ParameterName = InvoiceStoredProcedure.StoredProcedureParameters[2].ToString(), Value = customerId.AsDbValue() },

            };
            #endregion
            var resultTable = _tcContext.Set<InvoiceReturn>().FromSqlRaw(InvoiceStoredProcedure.Sql, parms.ToArray()).ToList();

            var errorTable = resultTable.Where(x => string.IsNullOrEmpty(x.InvoiceId) == true).FirstOrDefault();
            var tableResponse = new TableResponse<InvoiceReturn>
            {

                Message = errorTable == null ? "Invoice Created" : "Invoice Creation Failed",
                OutList = errorTable == null ? resultTable : null


            };
            return tableResponse;

        }
        #endregion
    }
}
