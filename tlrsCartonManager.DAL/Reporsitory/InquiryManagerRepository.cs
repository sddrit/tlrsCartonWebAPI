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
using tlrsCartonManager.DAL.Models.Operation;
using tlrsCartonManager.DAL.Models.Ownership;
using tlrsCartonManager.DAL.Exceptions;
using tlrsCartonManager.DAL.Models.Dispatch;

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
            
            var cartonList = await _tcContext.Set<CartonInquiry>().FromSqlRaw(SearchStoredProcedureBasic.Sql, parms.ToArray()).ToListAsync();
           
            cartonOverView.CartonHeader = cartonList.FirstOrDefault();

            cartonOverView.RequestHeader = _tcContext.InvoiceConfirmations.Where(x => x.CartonNo == cartonNo.ToString()).OrderBy(x => x.TrackingId).ToList();

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

        public async Task<OperationOverview> GetOperationOverview(int date)
        {
            OperationOverview operationOverView = new OperationOverview();

            List<SqlParameter> parms = new List<SqlParameter>
            {
               new SqlParameter { ParameterName = InquiryOperationOverviewStoredProcedure.StoredProcedureParameters[0].ToString(), Value = date.AsDbValue() },
               new SqlParameter { ParameterName = InquiryOperationOverviewStoredProcedure.StoredProcedureParameters[1].ToString(), Value = SearchCriterias.CSummary.ToString() }

            };
            operationOverView.CartonSummaryList = await _tcContext.Set<CartonSummary>().FromSqlRaw(InquiryOperationOverviewStoredProcedure.Sql, parms.ToArray()).ToListAsync();
            operationOverView.ScanBySummaryList = (ICollection<CartonUserSummary>)GetCartonUserOverview(date, SearchCriterias.SSummary.ToString());
            operationOverView.FumigationSummaryList = (ICollection<CartonUserSummary>)GetCartonUserOverview(date, SearchCriterias.FSummary.ToString());

            operationOverView.BaySummaryList = (ICollection<CartonLocationSummary>)GetCartonLocationOverview(date, SearchCriterias.BSummary.ToString());
            operationOverView.VehicleSummaryList = (ICollection<CartonLocationSummary>)GetCartonLocationOverview(date, SearchCriterias.VSummary.ToString());

            operationOverView.PalletedSummaryList = (ICollection<CartonUserSummary>)GetCartonUserOverview(date, SearchCriterias.PSummary.ToString());

            List<SqlParameter> parameters = new List<SqlParameter>
            {
               new SqlParameter { ParameterName = InquiryOperationOverviewStoredProcedure.StoredProcedureParameters[0].ToString(), Value = date.AsDbValue() },
               new SqlParameter { ParameterName = InquiryOperationOverviewStoredProcedure.StoredProcedureParameters[1].ToString(), Value = SearchCriterias.RDetail.ToString() }

            };
            operationOverView.RequestDetailList = await _tcContext.Set<RequestedDetail>().FromSqlRaw(InquiryOperationOverviewStoredProcedure.Sql, parameters.ToArray()).ToListAsync();
            return operationOverView;
        }

        public List<CartonUserSummary> GetCartonUserOverview(int date, string criteria)
        {
            List<SqlParameter> parms = new List<SqlParameter>
            {
               new SqlParameter { ParameterName = InquiryOperationOverviewStoredProcedure.StoredProcedureParameters[0].ToString(), Value = date.AsDbValue() },
               new SqlParameter { ParameterName = InquiryOperationOverviewStoredProcedure.StoredProcedureParameters[1].ToString(), Value = criteria.AsDbValue() }

            };
            return _tcContext.Set<CartonUserSummary>().FromSqlRaw(InquiryOperationOverviewStoredProcedure.Sql, parms.ToArray()).ToList();

        }
        public List<CartonLocationSummary> GetCartonLocationOverview(int date, string criteria)
        {

            List<SqlParameter> parms = new List<SqlParameter>
            {
               new SqlParameter { ParameterName = InquiryOperationOverviewStoredProcedure.StoredProcedureParameters[0].ToString(), Value = date.AsDbValue() },
               new SqlParameter { ParameterName = InquiryOperationOverviewStoredProcedure.StoredProcedureParameters[1].ToString(), Value = criteria.AsDbValue() }

            };
            return _tcContext.Set<CartonLocationSummary>().FromSqlRaw(InquiryOperationOverviewStoredProcedure.Sql, parms.ToArray()).ToList();

        }
        public async Task<PagedResponse<CartonInquiry>> SearchCartonHeader(string columnValueFrom, string columnValueTo, int pageIndex, int pageSize)
        {
            List<SqlParameter> parms = _searchManager.SearchFromTo("cartonInquiryFromTo", columnValueFrom, columnValueTo, pageIndex, pageSize, out SqlParameter outParam);
            var cartonList = await _tcContext.Set<CartonInquiry>().FromSqlRaw(SearchStoredProcedureFromTo.Sql, parms.ToArray()).ToListAsync();
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

        public async Task<PagedResponse<CartonInquiry>> SearchCartonHeaderRMS1(string columnValueFrom, string columnValueTo, int pageIndex, int pageSize)
        {
            List<SqlParameter> parms = _searchManager.SearchFromTo("cartonInquiryFromToRMS1", columnValueFrom, columnValueTo, pageIndex, pageSize, out SqlParameter outParam);
            var cartonList = await _tcContext.Set<CartonInquiry>().FromSqlRaw(SearchStoredProcedureFromTo.Sql, parms.ToArray()).ToListAsync();
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

        public Task<PagedResponse<CartonInquiry>> SearchCartonHeader(string columnValue, int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public async Task<List<OperationOverviewByWoType>> GetOperationOverviewByWoTypeAsync(int date, string woType)
        {
            List<SqlParameter> parms = new List<SqlParameter>
            {
               new SqlParameter { ParameterName = InquiryOperationOverviewByWoTypeStoredProcedure.StoredProcedureParameters[0].ToString(), Value = date.AsDbValue() },
               new SqlParameter { ParameterName = InquiryOperationOverviewByWoTypeStoredProcedure.StoredProcedureParameters[1].ToString(), Value = woType.AsDbValue() }

            };
            return await _tcContext.Set<OperationOverviewByWoType>().FromSqlRaw(InquiryOperationOverviewByWoTypeStoredProcedure.Sql, parms.ToArray()).ToListAsync();
        }
        public async Task<PagedResponse<OperationOverviewByUserLocaion>> GetOperationOverviewByUserLocationAsync(int date, string user, string locationCode,
            bool isRcLocation, bool isVehicle, string searchText, int pageIndex, int pageSize)
        {
            List<SqlParameter> parms = new List<SqlParameter>
            {
               new SqlParameter { ParameterName = InquiryOperationOverviewByUserLocationStoredProcedure.StoredProcedureParameters[0].ToString(), Value = date.AsDbValue() },
               new SqlParameter { ParameterName = InquiryOperationOverviewByUserLocationStoredProcedure.StoredProcedureParameters[1].ToString(), Value = user.AsDbValue() },
               new SqlParameter { ParameterName = InquiryOperationOverviewByUserLocationStoredProcedure.StoredProcedureParameters[2].ToString(), Value = locationCode.AsDbValue() },
               new SqlParameter { ParameterName = InquiryOperationOverviewByUserLocationStoredProcedure.StoredProcedureParameters[3].ToString(), Value = isRcLocation.AsDbValue() },
               new SqlParameter { ParameterName = InquiryOperationOverviewByUserLocationStoredProcedure.StoredProcedureParameters[4].ToString(), Value = isVehicle.AsDbValue() },
               new SqlParameter { ParameterName = InquiryOperationOverviewByUserLocationStoredProcedure.StoredProcedureParameters[5].ToString(), Value = searchText.AsDbValue() },
               new SqlParameter { ParameterName = InquiryOperationOverviewByUserLocationStoredProcedure.StoredProcedureParameters[6].ToString(), Value = pageIndex.AsDbValue() },
               new SqlParameter { ParameterName = InquiryOperationOverviewByUserLocationStoredProcedure.StoredProcedureParameters[7].ToString(), Value = pageSize.AsDbValue() },
            };
            var outParam = new SqlParameter { ParameterName = InquiryOperationOverviewByUserLocationStoredProcedure.StoredProcedureParameters[8].ToString(), SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };
            parms.Add(outParam);

            try
            {
                var operationOverviewByUserLocation = await _tcContext.Set<OperationOverviewByUserLocaion>().FromSqlRaw(InquiryOperationOverviewByUserLocationStoredProcedure.Sql, parms.ToArray()).ToListAsync();




                var totalRows = (int)outParam.Value;
                #region paging
                var postResponse = _mapper.Map<List<OperationOverviewByUserLocaion>>(operationOverviewByUserLocation);

                var paginationResponse = new PagedResponse<OperationOverviewByUserLocaion>
                {
                    Data = postResponse,
                    pageNumber = pageIndex,
                    pageSize = pageSize,
                    totalCount = totalRows,
                    totalPages = (int)Math.Ceiling(totalRows / (double)pageSize),

                };

                return paginationResponse;
            }
           
            #endregion
          

             catch (Exception ex)
            {
                throw ex;

            }
        }

        public async Task<PagedResponse<ViewRequestSummary>> GetRequestInquiryByCustomer(string cusomerCode,string searchText, int pageIndex, int pageSize)
        {
            List<SqlParameter> parms = _searchManager.Search("requestInquiry",  searchText, cusomerCode,pageIndex, pageSize,out SqlParameter outParam);

            var requestList = await _tcContext.Set<ViewRequestSummary>().FromSqlRaw(SearchStoredProcedureByTypeBase.Sql, parms.ToArray()).ToListAsync();

            var paginationResponse = new PagedResponse<ViewRequestSummary>
            {
                Data = requestList,
                pageNumber = pageIndex,
                pageSize = pageSize,
                totalCount = (int)outParam.Value,
                totalPages = (int)Math.Ceiling((int)outParam.Value / (double)pageSize),

            };
            return paginationResponse;
        }

        public List<CartonHistory> GetCartonHistory(int cartonNo, string rms)
        {
            List<SqlParameter> parms = new List<SqlParameter>
            {
               new SqlParameter { ParameterName = InquiryCartonHistoryStoredProcedure.StoredProcedureParameters[0].ToString(), Value = cartonNo.AsDbValue() },
               new SqlParameter { ParameterName = InquiryCartonHistoryStoredProcedure.StoredProcedureParameters[1].ToString(), Value = rms.AsDbValue() }

            };
            var cartonHistoryList= _tcContext.Set<CartonHistory>().FromSqlRaw(InquiryCartonHistoryStoredProcedure.Sql, parms.ToArray()).ToList();
            if(rms=="RMS2" &&( cartonHistoryList == null || cartonHistoryList.Count==0 ))
            {
                throw new ServiceException(new ErrorMessage[]
                 {
                        new ErrorMessage()
                        {
                            Code = string.Empty,
                            Message = $"Unable to find carton history "
                        }
                 });


            }
            return cartonHistoryList;
        }


        public List<CartonDispatchViewModel> GetCartonDispatch(string requestNo)
        {
            List<SqlParameter> parms = new List<SqlParameter>
            {
               new SqlParameter { ParameterName = InquiryCartonDispatchStoredProcedure.StoredProcedureParameters[0].ToString(), Value = requestNo.AsDbValue() }

            };

           return _tcContext.Set<CartonDispatchViewModel>().FromSqlRaw(InquiryCartonDispatchStoredProcedure.Sql, parms.ToArray()).ToList();
        }
    }
}
