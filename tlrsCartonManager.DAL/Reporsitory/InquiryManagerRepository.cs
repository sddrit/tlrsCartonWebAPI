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
using tlrsCartonManager.DAL.Models;
using tlrsCartonManager.DAL.Dtos;
using Microsoft.Data.SqlClient;
using tlrsCartonManager.DAL.Utility;
using System.Data;
using tlrsCartonManager.DAL.Helper;
using static tlrsCartonManager.DAL.Utility.Status;
using tlrsCartonManager.DAL.Extensions;
using Newtonsoft.Json;
using tlrsCartonManager.DAL.Dtos.Carton;
using tlrsCartonManager.DAL.Dtos.Pick;
using tlrsCartonManager.DAL.Models.Carton;
using tlrsCartonManager.DAL.Models.Docket;

namespace tlrsCartonManager.DAL.Reporsitory
{
    public class InquiryManagerRepository : IInquiryManagerRepository
    {
        private readonly tlrmCartonContext _tcContext;
        private readonly IMapper _mapper;
        private readonly ISearchManagerRepository _searchManager;

        public InquiryManagerRepository(tlrmCartonContext tccontext, IMapper mapper, ISearchManagerRepository searchManager)
        {
            _tcContext = tccontext;
            _mapper = mapper;
            _searchManager = searchManager;
        }

        public async Task<CartonOverviewDto> GetCartonOverview(int cartonNo)
        {
            CartonOverviewDto cartonOverView = new CartonOverviewDto();

            List<SqlParameter> parms = _searchManager.Search("cartonInquiry", cartonNo.ToString(), SearchStoredProcedure.PageIndex, SearchStoredProcedure.PageSize, out SqlParameter outParam);
            var cartonList = await _tcContext.Set<CartonInquiry>().FromSqlRaw(SearchStoredProcedure.Sql, parms.ToArray()).ToListAsync();
            cartonOverView.CartonHeader = cartonList.FirstOrDefault();

            cartonOverView.RequestHeader = _tcContext.InvoiceConfirmations.Where(x => x.CartonNo == cartonNo.ToString()).OrderBy(x => x.LastTransactionDate).ToList();

            var cartonLocationList = _tcContext.CartonLocations.Where(x => x.CartonNo == cartonNo).OrderBy(x => x.Id).ToList();
            cartonOverView.LocationDetail = _mapper.Map<List<CartonLocationDto>>(cartonLocationList);

            var pickList = _tcContext.PickLists.Where(x => x.CartonNo == cartonNo).OrderBy(x => x.TrackingId).ToList();
            cartonOverView.PickList = _mapper.Map<List<PickListDto>>(pickList);

            List<SqlParameter> parameters = new List<SqlParameter>
            {
               new SqlParameter { ParameterName = InquiryDocketByCartonStoredProcedure.StoredProcedureParameters[0].ToString(), Value = cartonNo.AsDbValue() }
            };

            cartonOverView.PrintedDockets = await _tcContext.Set<DocketPrintDetail>().FromSqlRaw(InquiryDocketByCartonStoredProcedure.Sql, parameters.ToArray()).ToListAsync();

            cartonOverView.CartonOwnership = _tcContext.CartonOwnerShips.Where(x => x.CartonNo == cartonNo).OrderBy(x => x.OwnershipChangedDate).ToList();

            return cartonOverView;
        }

        public async Task<PagedResponse<CartonInquiry>> SearchCartonHeader(string columnValue, int pageIndex, int pageSize)
        {
            List<SqlParameter> parms = _searchManager.Search("cartonInquiry", columnValue, pageIndex, pageSize, out SqlParameter outParam);
            var cartonList = await _tcContext.Set<CartonInquiry>().FromSqlRaw(SearchStoredProcedure.Sql, parms.ToArray()).ToListAsync();
            var totalRows = (int)outParam.Value;
            #region paging         

            var paginationResponse = new PagedResponse<CartonInquiry>
            {
                Data = cartonList,
                pageNumber = pageIndex,
                pageSize = pageSize,
                totalCount = totalRows,
                totalPages = (int)Math.Ceiling(totalRows / (double)pageSize),

            };
            #endregion

            return paginationResponse;
        }

        public async Task<PagedResponse<CartonInquiry>> SearchCartonHeaderRMS1(string columnValue, int pageIndex, int pageSize)
        {
            List<SqlParameter> parms = _searchManager.Search("cartonInquiryRMS1", columnValue, pageIndex, pageSize, out SqlParameter outParam);
            var cartonList = await _tcContext.Set<CartonInquiry>().FromSqlRaw(SearchStoredProcedure.Sql, parms.ToArray()).ToListAsync();
            var totalRows = (int)outParam.Value;
            #region paging         

            var paginationResponse = new PagedResponse<CartonInquiry>
            {
                Data = cartonList,
                pageNumber = pageIndex,
                pageSize = pageSize,
                totalCount = totalRows,
                totalPages = (int)Math.Ceiling(totalRows / (double)pageSize),

            };
            #endregion

            return paginationResponse;
        }
    }
}
