using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Data;
using tlrsCartonManager.DAL.Models;
using tlrsCartonManager.DAL.Exceptions;
using tlrsCartonManager.DAL.Reporsitory.IRepository;
using tlrsCartonManager.Core.Environment;
using tlrsCartonManager.DAL.Helper;
using tlrsCartonManager.DAL.Dtos.DailyCollectionMark;
using tlrsCartonManager.DAL.Utility;
using tlrsCartonManager.DAL.Extensions;
using tlrsCartonManager.DAL.Models.Verification;
using static tlrsCartonManager.DAL.Utility.Status;

namespace tlrsDailyCollectionManager.DAL.Reporsitory
{
    public class VerificationManagerRepository : IVerficationManagerRepository
    {
        private readonly tlrmCartonContext _tcContext;
        private readonly IMapper _mapper;
        private readonly ISearchManagerRepository _searchManager;
        private readonly IEnvironment _environment;

        public VerificationManagerRepository(tlrmCartonContext tccontext, IMapper mapper, ISearchManagerRepository searchManager, IEnvironment environment)
        {
            _tcContext = tccontext;
            _mapper = mapper;
            _searchManager = searchManager;
            _environment = environment;
        }

        public async Task<List<VerificationPickInvalidViewModel>> GetInvalidScans(string requestNo)
        {
            VerificationPickModel request = new VerificationPickModel() { RequestNo = requestNo };
            return GetSetPickVerification<VerificationPickInvalidViewModel>(request, "ViewInvalidScans");

        }

        public async Task<List<VerificationPickViewModel>> GetVerified(string requestNo)
        {
            VerificationPickModel request = new VerificationPickModel() { RequestNo = requestNo };
            return GetSetPickVerification<VerificationPickViewModel>(request, "ViewVerified");

        }

        public async Task<List<VerificationPickViewModel>> UpdateAndGetCartons(VerificationPickModel request)
        {
           
            return GetSetPickVerification<VerificationPickViewModel>(request, TransactionTypes.Insert.ToString()).ToList();

        }

        private List<T> GetSetPickVerification<T>(VerificationPickModel request, string type) where T : class
        {
            List<SqlParameter> parms = new List<SqlParameter>
            {
                new SqlParameter { ParameterName = VerificationStoredProcedure.StoredProcedureParameters[0].ToString(),
                    Value = request.RequestNo.AsDbValue() },
                new SqlParameter { ParameterName = VerificationStoredProcedure.StoredProcedureParameters[1].ToString(),
                    Value = request.CartonNo.AsDbValue()},
                new SqlParameter { ParameterName = VerificationStoredProcedure.StoredProcedureParameters[2].ToString(),
                    Value = type.AsDbValue()},

            };
            return _tcContext.Set<T>().FromSqlRaw(VerificationStoredProcedure.Sql, parms.ToArray()).ToList();
        }
    }
}
