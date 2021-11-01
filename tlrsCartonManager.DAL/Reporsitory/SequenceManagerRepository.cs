using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tlrsCartonManager.Core.Environment;
using tlrsCartonManager.DAL.Exceptions;
using tlrsCartonManager.DAL.Extensions;
using tlrsCartonManager.DAL.Models;
using tlrsCartonManager.DAL.Models.SequenceMonthEnd;
using tlrsCartonManager.DAL.Reporsitory.IRepository;
using tlrsCartonManager.DAL.Utility.StoredProcedures;
using static tlrsCartonManager.DAL.Utility.Status;

namespace tlrsCartonManager.DAL.Reporsitory
{
    public class SequenceManagerRepository : ISequenceManagerRepository
    {

        private readonly tlrmCartonContext _tcContext;
        private readonly IMapper _mapper;
        private readonly IEnvironment _environment;

        public SequenceManagerRepository(tlrmCartonContext tccontext, IMapper mapper,IEnvironment environment)
        {
            _tcContext = tccontext;
            _mapper = mapper;            
            _environment = environment;
        }
        public bool AddSequence(SequenceModel request)
        {
            SaveSequences(request, TransactionTypes.Insert.ToString());
            return true;
        }

        public List<SequenceModel> GetSequenceAsync()
        {
            var result =  _tcContext.Sequences.OrderByDescending(x=>x.SequenceType).ToList();
            if(result==null || !result.Any())
            {
                throw new ServiceException(new ErrorMessage[]
                {
                     new ErrorMessage()
                     {
                        Code = string.Empty,
                        Message = "No Sequences found"
                     }
                });

            }
           return _mapper.Map<List<SequenceModel>>(result);
        }

        public SequenceModel GetSequenceByCodeAsync(string code)
        {
            var result = _tcContext.Sequences.Where(x => x.SequenceType == code).FirstOrDefault();
            if (result == null )
            {
                throw new ServiceException(new ErrorMessage[]
                {
                     new ErrorMessage()
                     {
                        Code = string.Empty,
                        Message = "No Sequences found"
                     }
                });

            }
            return _mapper.Map<SequenceModel>(result);
        }

        public bool UpdateSequence(SequenceModel request)
        {
            SaveSequences(request, TransactionTypes.Update.ToString());
            return true;
        }

        private string SaveSequences(SequenceModel request, string transactionType)
        {

            List<SqlParameter> parms = new List<SqlParameter>
            {
                 new SqlParameter { ParameterName = SequenceStoredProcedure.StoredProcedureParameters[0].ToString(), Value = request.SequenceType.AsDbValue()} ,
                 new SqlParameter { ParameterName = SequenceStoredProcedure.StoredProcedureParameters[1].ToString(), Value = request.LastNo.AsDbValue() } ,
                 new SqlParameter { ParameterName = SequenceStoredProcedure.StoredProcedureParameters[2].ToString(), Value = request.Active.AsDbValue() } ,
                 new SqlParameter { ParameterName = SequenceStoredProcedure.StoredProcedureParameters[3].ToString(), Value = request.CurrentSuffix.AsDbValue() } ,
                 new SqlParameter { ParameterName = SequenceStoredProcedure.StoredProcedureParameters[4].ToString(), Value = request.RequestTypeCode.AsDbValue() } ,
                 new SqlParameter { ParameterName = SequenceStoredProcedure.StoredProcedureParameters[5].ToString(), Value = transactionType.AsDbValue() } ,
                 new SqlParameter { ParameterName = SequenceStoredProcedure.StoredProcedureParameters[6].ToString(), Value = _environment.GetCurrentEnvironment().UserId.AsDbValue() } 
             
            };
            var result= _tcContext.Set<StringReturn>().FromSqlRaw(SequenceStoredProcedure.Sql, parms.ToArray()).AsEnumerable().FirstOrDefault().Value;

            if( result.ToString().ToUpper()!="OK")
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

            return result;
        }

    }
}
