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
    public class CustomerManagerRepository : ICustomerManagerRepository
    {
        private readonly tlrmCartonContext _tcContext;
        private readonly IMapper _mapper;

        public CustomerManagerRepository(tlrmCartonContext tccontext, IMapper mapper)
        {
            _tcContext = tccontext;
            _mapper = mapper;
        }
        public async  Task<IEnumerable<CustomerDisplayDto>> GetCustomerList()
        {
            var customer = await _tcContext.Customers. //ruv
               ToListAsync();
            return _mapper.Map<IEnumerable<CustomerDisplayDto>>(customer);

        }

        public async Task<CustomerDisplayDto> GetCustomerById(int customerId)
        {
            var subAccList= _mapper.Map < IEnumerable < CustomerSubAccountListDto >> (await _tcContext.Customers.Where(x => x.MainCustomerCode == customerId && x.AccountType!="M").ToListAsync());
            var customerList= _mapper.Map <CustomerDisplayDto>( await _tcContext.Customers.Include(x => x.CustomerAuthorizationLists).SingleOrDefaultAsync(x => x.TrackingId == customerId));
            customerList.CustomerSubAccountLists = (ICollection<CustomerSubAccountListDto>)subAccList;
            return customerList;
           
        }
        public async Task<PagedListSP<CustomerSearch>> SearchCustomer(string columnName, string columnValue, int pageIndex, int pageSize)
        {
            
            List<SqlParameter> parms = new List<SqlParameter>
            {
               new SqlParameter { ParameterName = CustomerStoredProcedureSearch.StoredProcedureParameters[0].ToString(), Value = columnName },
               new SqlParameter { ParameterName = CustomerStoredProcedureSearch.StoredProcedureParameters[1].ToString(), Value = columnValue==null ? string.Empty : columnValue },
               new SqlParameter { ParameterName = CustomerStoredProcedureSearch.StoredProcedureParameters[2].ToString(), Value = pageIndex },
               new SqlParameter { ParameterName = CustomerStoredProcedureSearch.StoredProcedureParameters[3].ToString(), Value = pageSize },
              
            };
            var outParam = new SqlParameter { ParameterName = CustomerStoredProcedureSearch.StoredProcedureParameters[4].ToString(),SqlDbType= SqlDbType.Int, Direction = ParameterDirection.Output };
            parms.Add(outParam);
            var customerList = await _tcContext.Set<CustomerSearch>().FromSqlRaw(CustomerStoredProcedureSearch.Sql, parms.ToArray()).ToListAsync();
            var totalRows = (int)outParam.Value;           
            return new PagedListSP<CustomerSearch>(customerList, pageIndex, pageSize, totalRows);
        }
        public bool AddCustomer(CustomerInsertDto customerInsert)
        {
            var customerTransaction = _mapper.Map<CustomerInsertDto, CustomerInsertUpdateDto> (customerInsert);
            return SaveCustomer(customerTransaction, TransactionTypes.Insert.ToString());
        }
        public bool UpdateCustomer(CustomerInsertUpdateDto customerUpdate)
        {
            return SaveCustomer(customerUpdate, TransactionTypes.Update.ToString());
        }
        public bool DeleteCustomer(CustomerDeleteDto customerDelete)
        {
            var cutomerTransaction = new CustomerInsertUpdateDto
            {
                TrackingId = customerDelete.TrackingId
            };
            
            return SaveCustomer(cutomerTransaction, TransactionTypes.Delete.ToString());
        }
        private bool SaveCustomer(CustomerInsertUpdateDto customerTransaction, string transcationType)
        {    

            List<SqlParameter> parms = new List<SqlParameter>
            {
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[0].ToString(), 
                    Value = transcationType==TransactionTypes.Insert.ToString()? 0: customerTransaction.TrackingId },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[1].ToString(), Value = customerTransaction.CustomerCode },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[2].ToString(), Value = customerTransaction.Name },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[3].ToString(), Value = customerTransaction.Address1 },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[4].ToString(), Value = customerTransaction.Address2 },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[5].ToString(), Value = customerTransaction.Address3 },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[6].ToString(), Value = customerTransaction.Telephone1 },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[7].ToString(), Value = customerTransaction.Telephone2 },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[8].ToString(), Value = customerTransaction.Fax },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[9].ToString(), Value = customerTransaction.ZipCode },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[10].ToString(), Value = customerTransaction.CountryId },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[11].ToString(), Value = customerTransaction.Email },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[12].ToString(), Value = customerTransaction.ContractNo },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[13].ToString(), Value = customerTransaction.ContractStartDate },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[14].ToString(), Value = customerTransaction.ContractEndDate },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[15].ToString(), Value = customerTransaction.DeliveryName },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[16].ToString(), Value = customerTransaction.DeliveryAddress1 },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[17].ToString(), Value = customerTransaction.DeliveryAddress2 },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[18].ToString(), Value = customerTransaction.DeliveryAddress3 },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[19].ToString(), Value = customerTransaction.DeliveryTelephone1 },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[20].ToString(), Value = customerTransaction.DeliveryTelephone2 },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[21].ToString(), Value = customerTransaction.DeliveryFax },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[22].ToString(), Value = customerTransaction.PickUpName },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[23].ToString(), Value = customerTransaction.PickUpAddress1 },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[24].ToString(), Value = customerTransaction.PickUpAddress2 },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[25].ToString(), Value = customerTransaction.PickUpAddress3 },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[26].ToString(), Value = customerTransaction.PickUpTelephone1 },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[27].ToString(), Value = customerTransaction.PickUpTelephone2 },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[28].ToString(), Value = customerTransaction.PickUpFax },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[29].ToString(), Value = customerTransaction.City },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[30].ToString(), Value = customerTransaction.ContactName },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[31].ToString(), Value = customerTransaction.ContactAddress1 },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[32].ToString(), Value = customerTransaction.ContactAddress2 },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[33].ToString(), Value = customerTransaction.ContactAddress3 },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[34].ToString(), Value = customerTransaction.ContactTelephone1 },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[35].ToString(), Value = customerTransaction.ContactTelephone2 },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[36].ToString(), Value = customerTransaction.ContactFax },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[37].ToString(), Value = customerTransaction.PoNo },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[38].ToString(), Value = customerTransaction.VatNo },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[39].ToString(), Value = customerTransaction.SvatNo },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[40].ToString(), Value = customerTransaction.BillingCycle },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[41].ToString(), Value = customerTransaction.Route },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[42].ToString(), Value = customerTransaction.IsSeparateInvoice },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[43].ToString(), Value = customerTransaction.ContactPersonInv },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[44].ToString(), Value = customerTransaction.SubInvoice },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[45].ToString(), Value = customerTransaction.ServiceProvided },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[46].ToString(), Value = customerTransaction.AccountType },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[47].ToString(), Value = customerTransaction.MainCustomerCode },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[48].ToString(), Value = customerTransaction.Status },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[49].ToString(), Value = customerTransaction.User },
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[50].ToString(), Value = transcationType },               
                new SqlParameter { ParameterName = CustomerStoredProcedure.StoredProcedureParameters[51].ToString(), TypeName = CustomerStoredProcedure.StoredProcedureTypeNames[0].ToString(),
                SqlDbType = SqlDbType.Structured, Value = customerTransaction.CustomerAuthorizationLists.ToList().ToDataTable()}               
            };
            return _tcContext.Set<BoolReturn>().FromSqlRaw(CustomerStoredProcedure.Sql, parms.ToArray()).AsEnumerable().First().Value;
        }

       
    }
    }