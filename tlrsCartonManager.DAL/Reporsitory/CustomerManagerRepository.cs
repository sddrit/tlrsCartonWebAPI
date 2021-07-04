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
using tlrsCartonManager.DAL.Exceptions;

namespace tlrsCartonManager.DAL.Reporsitory
{
    public class CustomerManagerRepository : ICustomerManagerRepository
    {
        private readonly tlrmCartonContext _tcContext;
        private readonly IMapper _mapper;

        public CustomerManagerRepository(tlrmCartonContext tccontext, IMapper mapper)
        {
            _tcContext = tccontext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CustomerDto>> GetCustomerList()
        {
            var customer = await _tcContext.Customers.ToListAsync();
            return _mapper.Map<IEnumerable<CustomerDto>>(customer);
        }

        public async Task<CustomerDto> GetCustomerById(int customerId)
        {            
            var subAccList = _mapper.Map<IEnumerable<CustomerSubAccountListDto>>(await _tcContext.Customers.
                                Where(x => x.MainCustomerCode == customerId && x.AccountType != "M" && x.Deleted == false).ToListAsync());

            var customerList = _mapper.Map<CustomerDto>(await _tcContext.Customers.
                                Include(x => x.CustomerAuthorizationListHeaders.Where(x => x.Deleted == false)).
                                ThenInclude(x => x.CustomerAuthorizationListDetails.Where(x => x.Deleted == false)).
                                FirstOrDefaultAsync(x => x.TrackingId == customerId && x.Deleted == false));

            if (customerList != null)
                customerList.CustomerSubAccountLists = (ICollection<CustomerSubAccountListDto>)subAccList;
            else
            {
                throw new ServiceException(new ErrorMessage[]
                    {
                        new ErrorMessage()
                        {
                            Code = string.Empty,
                            Message = $"Unable to find customer by {customerId}"
                        }
                    });
            }
            return customerList;
        }

        public async Task<List<CustomerAuthorizationHeaderDto>> GetCustomerAuthorizationById(int customerId)
        {
            var authorizedList = (await _tcContext.CustomerAuthorizationListHeaders.
                Where(x => x.CustomerId == customerId && x.Deleted == false)
                .Select(p => new CustomerAuthorizationHeaderDto()
                {
                    TrackingId = p.TrackingId,
                    Name = p.Name
                }).ToListAsync());
            if(authorizedList==null || (authorizedList!=null && authorizedList.Count==0))
            {                   
                throw new ServiceException(new ErrorMessage[]
                 {
                     new ErrorMessage()
                     {
                        Code = string.Empty,
                        Message = $"Unable to find authorization by {customerId}"
                     }
                 });    
            }
            return authorizedList;
        }

        public async Task<IEnumerable<CustomerMainCodeSearchDto>> GetCustomerByMainName(string customerName)
        {
            var mainAccList = await _tcContext.Customers.
                Where(x => (EF.Functions.Like(x.Name, "%" + customerName + "%") && (x.AccountType == "M") && x.Deleted == false)).ToListAsync();
            if (mainAccList == null)
            {
                throw new ServiceException(new ErrorMessage[]
                 {
                     new ErrorMessage()
                     {
                            Code = string.Empty,
                            Message = $"Unable to find customer by {customerName}"
                     }
                 });
            }
            return _mapper.Map<IEnumerable<CustomerMainCodeSearchDto>>(mainAccList);
        }

        public async Task<IEnumerable<CustomerMainCodeSearchDto>> GetCustomerByMainId(int customerId)
        {
            var mainAccList = await _tcContext.Customers.
                Where(x => x.TrackingId == customerId && x.AccountType == "M" && x.Deleted == false).ToListAsync();
            if(mainAccList==null)
            {
                throw new ServiceException(new ErrorMessage[]
                {
                     new ErrorMessage()
                     {
                            Code = string.Empty,
                            Message = $"Unable to find customer by main account {customerId}"
                     }
                });
            }
            return _mapper.Map<IEnumerable<CustomerMainCodeSearchDto>>(mainAccList);
        }
        public async Task<IEnumerable<CustomerSearchDto>> GetCustomerByName(string customerName, bool isAll)
        {
            var mainAccList = await _tcContext.Customers.
               Where(x => (EF.Functions.Like(x.Name, "%" + customerName + "%") && x.Deleted == false && x.Active == true)).ToListAsync();
            if (isAll)
                mainAccList = await _tcContext.Customers.
                 Where(x => (EF.Functions.Like(x.Name, "%" + customerName + "%") && x.Deleted == false)).ToListAsync();

            if (mainAccList == null)
            {
                throw new ServiceException(new ErrorMessage[]
                    {
                        new ErrorMessage()
                        {
                            Code = string.Empty,
                            Message = $"Unable to find Customer by name {customerName}"
                        }
                    });
            }
            return _mapper.Map<IEnumerable<CustomerSearchDto>>(mainAccList);
        }

        public async Task<IEnumerable<CustomerSearchDto>> GetCustomerByCode(string customerCode, bool isAll)
        {
            var mainAccList = await _tcContext.Customers.
               Where(x => (EF.Functions.Like(x.CustomerCode, "%" + customerCode + "%") && x.Deleted == false && x.Active == true)).ToListAsync();
            if (isAll)
                mainAccList = await _tcContext.Customers.
               Where(x => (EF.Functions.Like(x.CustomerCode, "%" + customerCode + "%") && x.Deleted == false)).ToListAsync();
            if (mainAccList == null)
            {
                throw new ServiceException(new ErrorMessage[]
                   {
                        new ErrorMessage()
                        {
                            Code = string.Empty,
                            Message = $"Unable to find Customer by code {customerCode}"
                        }
                   });
            }
            return _mapper.Map<IEnumerable<CustomerSearchDto>>(mainAccList);
        }

        public int? GetCustomerId(string customerCode)
        {
            return (int)(_tcContext.Customers.Where(x => x.CustomerCode == customerCode).FirstOrDefault()?.TrackingId ?? 0);
        }        

        public bool AddCustomer(CustomerDto customerInsert)
        {
            if (ValidateCustomer(customerInsert, TransactionTypes.Insert.ToString()))
            {
                if (!SaveCustomer(customerInsert, TransactionTypes.Insert.ToString()))
                {
                    throw new ServiceException(new ErrorMessage[]
                    {
                        new ErrorMessage()
                        {
                            Code = string.Empty,
                            Message = $"Unable to create customer"
                        }
                    });
                }
            }
            return true;
        }

        public bool UpdateCustomer(CustomerDto customerUpdate)
        {
            if (ValidateCustomer(customerUpdate, TransactionTypes.Update.ToString()))
            {
                if (!SaveCustomer(customerUpdate, TransactionTypes.Update.ToString()))
                {
                    throw new ServiceException(new ErrorMessage[]
                    {
                    new ErrorMessage()
                        {
                            Code = string.Empty,
                            Message = $"Unable to create customer"
                        }
                    });
                }
            }
            return true;
        }

        public bool DeleteCustomer(CustomerDeleteDto customerDelete)
        {
            var cutomerTransaction = new CustomerDto
            {
                TrackingId = customerDelete.TrackingId
            };
            if (!SaveCustomer(cutomerTransaction, TransactionTypes.Delete.ToString()))
            {
                throw new ServiceException(new ErrorMessage[]
                {
                    new ErrorMessage()
                        {
                            Code = string.Empty,
                            Message = $"Unable to delete customer"
                        }
                });
            }
            return true;
        }

        public bool ValidateCustomer(CustomerDto customer, string transcationType)
        {
            if (transcationType == TransactionTypes.Insert.ToString())
            {
                if (_tcContext.Customers.Any(x => x.CustomerCode.ToUpper().Trim() == customer.CustomerCode.ToUpper().Trim() && x.Deleted==false))
                {
                    throw new ServiceException(new ErrorMessage[]
                    {
                        new ErrorMessage()
                        {
                            Code = string.Empty,
                            Message = $"Existing Customer Code Found"
                        }
                    });
                }

                if (_tcContext.Customers.Any(x => x.Name.ToUpper().Trim() == customer.Name.ToUpper().Trim() && x.Deleted == false))
                {
                    throw new ServiceException(new ErrorMessage[]
                    {
                        new ErrorMessage()
                        {
                            Code = string.Empty,
                            Message = $"Existing Customer Name Found"
                        }
                    });
                }
            }
            if (transcationType == TransactionTypes.Update.ToString())
            {
                if (_tcContext.Customers.Any(c => c.TrackingId != customer.TrackingId && c.Name.ToUpper().Trim() == customer.Name.ToUpper().Trim()))
                {
                    throw new ServiceException(new ErrorMessage[]
                    {
                        new ErrorMessage()
                        {
                            Code = string.Empty,
                            Message = $"Existing Customer Name Found"
                        }
                    });
                }
            }
            return true;
        }

        private bool SaveCustomer(CustomerDto customerTransaction, string transcationType)
        {
            #region Assign values to utds          
            List<CustomerAuthorizationListUtdDto> lstAuthorization = new List<CustomerAuthorizationListUtdDto>();
            List<CustomerAuthorizationListDetailUdtDto> lstAuthorizationLevel = new List<CustomerAuthorizationListDetailUdtDto>();

            var custAuth = customerTransaction.CustomerAuthorizationListHeaders.ToList();
            int ix = 0;
            foreach (var customerAuthItem in custAuth)
            {
                var b = _mapper.Map<CustomerAuthorizationListUtdDto>(customerAuthItem);
                b.AutoId = ix + 1;
                lstAuthorization.Add(b);

                var d = _mapper.Map<List<CustomerAuthorizationListDetailUdtDto>>(customerAuthItem.CustomerAuthorizationListDetails.ToList());
                foreach (var customerAuthlevel in d)
                {
                    customerAuthlevel.AutoId = b.AutoId;
                    lstAuthorizationLevel.Add(customerAuthlevel);

                }
                ix = ix + 1;
            }
            #endregion

            #region Sql Parameter loading
            List<SqlParameter> parms = new List<SqlParameter>
            {
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[0].ToString(),
                    Value = transcationType==TransactionTypes.Insert.ToString()? 0: customerTransaction.TrackingId },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[1].ToString(), Value = customerTransaction.CustomerCode.AsDbValue() },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[2].ToString(), Value = customerTransaction.Name.AsDbValue() },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[3].ToString(), Value = customerTransaction.Address1.AsDbValue() },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[4].ToString(), Value = customerTransaction.Address2.AsDbValue() },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[5].ToString(), Value = customerTransaction.Address3.AsDbValue() },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[6].ToString(), Value = customerTransaction.Telephone1.AsDbValue()},
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[7].ToString(), Value = customerTransaction.Telephone2.AsDbValue() },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[8].ToString(), Value = customerTransaction.Fax.AsDbValue() },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[9].ToString(), Value = customerTransaction.ZipCode.AsDbValue() },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[10].ToString(), Value = customerTransaction.CountryId.AsDbValue() },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[11].ToString(), Value = customerTransaction.Email.AsDbValue() },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[12].ToString(), Value = customerTransaction.ContractNo.AsDbValue() },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[13].ToString(), Value = customerTransaction.ContractStartDate.AsDbValue() },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[14].ToString(), Value = customerTransaction.ContractEndDate.AsDbValue() },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[15].ToString(), Value = customerTransaction.DeliveryName.AsDbValue() },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[16].ToString(), Value = customerTransaction.DeliveryAddress1.AsDbValue() },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[17].ToString(), Value = customerTransaction.DeliveryAddress2.AsDbValue() },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[18].ToString(), Value = customerTransaction.DeliveryAddress3.AsDbValue() },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[19].ToString(), Value = customerTransaction.DeliveryTelephone1.AsDbValue() },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[20].ToString(), Value = customerTransaction.DeliveryTelephone2.AsDbValue() },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[21].ToString(), Value = customerTransaction.DeliveryFax.AsDbValue() },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[22].ToString(), Value = customerTransaction.PickUpName.AsDbValue() },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[23].ToString(), Value = customerTransaction.PickUpAddress1.AsDbValue() },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[24].ToString(), Value = customerTransaction.PickUpAddress2.AsDbValue() },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[25].ToString(), Value = customerTransaction.PickUpAddress3.AsDbValue() },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[26].ToString(), Value = customerTransaction.PickUpTelephone1.AsDbValue() },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[27].ToString(), Value = customerTransaction.PickUpTelephone2.AsDbValue() },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[28].ToString(), Value = customerTransaction.PickUpFax.AsDbValue() },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[29].ToString(), Value = customerTransaction.City.AsDbValue() },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[30].ToString(), Value = customerTransaction.ContactName.AsDbValue() },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[31].ToString(), Value = customerTransaction.ContactAddress1.AsDbValue() },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[32].ToString(), Value = customerTransaction.ContactAddress2.AsDbValue() },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[33].ToString(), Value = customerTransaction.ContactAddress3.AsDbValue()},
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[34].ToString(), Value = customerTransaction.ContactTelephone1.AsDbValue() },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[35].ToString(), Value = customerTransaction.ContactTelephone2.AsDbValue() },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[36].ToString(), Value = customerTransaction.ContactFax.AsDbValue() },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[37].ToString(), Value = customerTransaction.PoNo.AsDbValue() },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[38].ToString(), Value = customerTransaction.VatNo.AsDbValue() },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[39].ToString(), Value = customerTransaction.SvatNo.AsDbValue() },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[40].ToString(), Value = customerTransaction.BillingCycle.AsDbValue() },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[41].ToString(), Value = customerTransaction.Route.AsDbValue() },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[42].ToString(), Value = customerTransaction.IsSeparateInvoice.AsDbValue() },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[43].ToString(), Value = customerTransaction.ContactPersonInv.AsDbValue() },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[44].ToString(), Value = customerTransaction.SubInvoice.AsDbValue() },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[45].ToString(), Value = customerTransaction.ServiceProvided.AsDbValue()},
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[46].ToString(), Value = customerTransaction.AccountType.AsDbValue() },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[47].ToString(), Value = customerTransaction.MainCustomerCode.AsDbValue() },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[48].ToString(), Value = customerTransaction.Active.AsDbValue() },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[49].ToString(), Value = customerTransaction.User.AsDbValue() },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[50].ToString(), Value = transcationType.AsDbValue() } ,

                new SqlParameter
                {
                   ParameterName = CustomerStoredProcedure.StoredProcedureParameters[51].ToString(),
                   TypeName = CustomerStoredProcedure.StoredProcedureTypeNames[0].ToString(),
                   SqlDbType = SqlDbType.Structured,
                   Value =lstAuthorization.ToDataTable()
                },
                new SqlParameter
                {
                   ParameterName = CustomerStoredProcedure.StoredProcedureParameters[52].ToString(),
                   TypeName = CustomerStoredProcedure.StoredProcedureTypeNames[1].ToString(),
                   SqlDbType = SqlDbType.Structured,
                   Value =  lstAuthorizationLevel.ToDataTable()
                }
            };
            #endregion
            return _tcContext.Set<BoolReturn>().FromSqlRaw(CustomerStoredProcedure.Sql, parms.ToArray()).AsEnumerable().First().Value;
        }

        public async Task<PagedResponse<CustomerSearchDto>> SearchCustomer(string columnValue, int pageIndex, int pageSize)
        {
            List<SqlParameter> parms = new List<SqlParameter>
            {
               new SqlParameter { ParameterName = CustomerStoredProcedureSearch.StoredProcedureParameters[0].ToString(), Value = columnValue==null ? string.Empty :columnValue },
               new SqlParameter { ParameterName = CustomerStoredProcedureSearch.StoredProcedureParameters[1].ToString(), Value = pageIndex },
               new SqlParameter { ParameterName = CustomerStoredProcedureSearch.StoredProcedureParameters[2].ToString(), Value = pageSize },
            };

            var outParam = new SqlParameter { ParameterName = CustomerStoredProcedureSearch.StoredProcedureParameters[3].ToString(), SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };
            parms.Add(outParam);

            var customerList = await _tcContext.Set<CustomerSearch>().FromSqlRaw(CustomerStoredProcedureSearch.Sql, parms.ToArray()).ToListAsync();
           
            var totalRows = (int)outParam.Value;          
            var postResponse = _mapper.Map<List<CustomerSearchDto>>(customerList);
            var paginationResponse = new PagedResponse<CustomerSearchDto>(postResponse, pageIndex, pageSize, totalRows);               
          
            if (paginationResponse == null)
            {
                throw new ServiceException(new ErrorMessage[]
                {
                     new ErrorMessage()
                     {
                            Code = string.Empty,
                            Message = $"Unable to find customers"
                     }
                });
            }
            return paginationResponse;
        }

    }
}
