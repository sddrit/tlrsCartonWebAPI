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
using tlrsCartonManager.DAL.Dtos.Company;
using tlrsCartonManager.Core.Environment;
using tlrsCartonManager.DAL.Exceptions;

namespace tlrsCartonManager.DAL.Reporsitory
{
    public class CompanyManagerRepository : ICompanyManagerRepository
    {
        private readonly tlrmCartonContext _tcContext;
        private readonly IMapper _mapper;
        private readonly IEnvironment _environment;
        public CompanyManagerRepository(tlrmCartonContext tccontext, IMapper mapper, IEnvironment environment)
        {
            _tcContext = tccontext;
            _mapper = mapper;
            _environment = environment;
        }

        public CompanyDto GetCompany()
        {
            var company = _mapper.Map<CompanyDto>(_tcContext.Companies.FirstOrDefault());
            var taxEffectiveDates = _mapper.Map<List<TaxEffectiveDateDto>>(_tcContext.TaxEffectiveDates.
                Where(x => x.Deleted == false).ToList());
            if (company != null)
                company.TaxEffectiveDate = taxEffectiveDates == null ? new List<TaxEffectiveDateDto>() : taxEffectiveDates;
            return company;
        }

        public bool UpdateCompanyProfile(CompanyDto request)
        {
            List<SqlParameter> parms = new List<SqlParameter>
            {
                 new SqlParameter { ParameterName = CompanyProfileStoredProcedure.StoredProcedureParameters[0].ToString(), Value = request.CompanyId.AsDbValue() } ,
                 new SqlParameter { ParameterName = CompanyProfileStoredProcedure.StoredProcedureParameters[1].ToString(), Value = request.CompanyName.AsDbValue() } ,
                 new SqlParameter { ParameterName = CompanyProfileStoredProcedure.StoredProcedureParameters[2].ToString(), Value = request.Address1.AsDbValue() } ,
                 new SqlParameter { ParameterName = CompanyProfileStoredProcedure.StoredProcedureParameters[3].ToString(), Value = request.Address2.AsDbValue() } ,
                 new SqlParameter { ParameterName = CompanyProfileStoredProcedure.StoredProcedureParameters[4].ToString(), Value = request.Address3.AsDbValue() } ,
                 new SqlParameter { ParameterName = CompanyProfileStoredProcedure.StoredProcedureParameters[5].ToString(), Value = request.Country.AsDbValue() } ,
                 new SqlParameter { ParameterName = CompanyProfileStoredProcedure.StoredProcedureParameters[6].ToString(), Value = request.Tel.AsDbValue() } ,
                 new SqlParameter { ParameterName = CompanyProfileStoredProcedure.StoredProcedureParameters[7].ToString(), Value = request.Fax.AsDbValue() } ,
                 new SqlParameter { ParameterName = CompanyProfileStoredProcedure.StoredProcedureParameters[8].ToString(), Value = request.Email.AsDbValue() } ,
                 new SqlParameter { ParameterName = CompanyProfileStoredProcedure.StoredProcedureParameters[9].ToString(), Value = request.VatNo.AsDbValue() } ,
                 new SqlParameter { ParameterName = CompanyProfileStoredProcedure.StoredProcedureParameters[10].ToString(), Value = request.NbtNo.AsDbValue() } ,
                 new SqlParameter { ParameterName = CompanyProfileStoredProcedure.StoredProcedureParameters[11].ToString(), Value = request.SvatNo.AsDbValue() } ,
                 new SqlParameter { ParameterName = CompanyProfileStoredProcedure.StoredProcedureParameters[12].ToString(), Value =  _environment.GetCurrentEnvironment().UserId.AsDbValue() } ,
                new SqlParameter
                {
                   ParameterName = CompanyProfileStoredProcedure.StoredProcedureParameters[13].ToString(),
                   TypeName = CompanyProfileStoredProcedure.StoredProcedureTypeNames[0].ToString(),
                   SqlDbType = SqlDbType.Structured,
                   Value =request.TaxEffectiveDate.Where(x=>x.Id==0).ToList().ToDataTable()
                },

                 new SqlParameter { ParameterName = CompanyProfileStoredProcedure.StoredProcedureParameters[14].ToString(), Value =  request.TenantCode.AsDbValue() } ,
                 new SqlParameter { ParameterName = CompanyProfileStoredProcedure.StoredProcedureParameters[15].ToString(), Value =  request.AccountMangerName.AsDbValue() } ,
                 new SqlParameter { ParameterName = CompanyProfileStoredProcedure.StoredProcedureParameters[16].ToString(), Value =  request.AccountManagerEmail.AsDbValue() } ,
                 new SqlParameter { ParameterName = CompanyProfileStoredProcedure.StoredProcedureParameters[17].ToString(), Value =  request.BankAccountNo.AsDbValue() } ,
                 new SqlParameter { ParameterName = CompanyProfileStoredProcedure.StoredProcedureParameters[18].ToString(), Value =  request.BankName.AsDbValue() } ,
                 new SqlParameter { ParameterName = CompanyProfileStoredProcedure.StoredProcedureParameters[19].ToString(), Value =  request.BranchName.AsDbValue() } ,

            };
            var result = _tcContext.Set<StringReturn>().FromSqlRaw(CompanyProfileStoredProcedure.Sql, parms.ToArray()).AsEnumerable().First().Value;

            if (result != "OK")
            {
                throw new ServiceException(new ErrorMessage[]
                   {
                        new ErrorMessage()
                        {
                            Code = string.Empty,
                            Message = result
                        }
                   });


            }
            return true;
        }
    }
}
