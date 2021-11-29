using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using tlrsCartonManager.DAL.Reporsitory.IRepository;
using tlrsCartonManager.DAL.Models;
using Microsoft.Data.SqlClient;
using tlrsCartonManager.DAL.Utility;
using System.Data;
using tlrsCartonManager.DAL.Extensions;
using tlrsCartonManager.DAL.Dtos.Import;
using tlrsCartonManager.Core.Environment;

namespace tlrsCartonManager.DAL.Reporsitory
{
    public class ImportDataManagerRepository : IImportDataManagerRepository
    {
        private readonly tlrmCartonContext _tcContext;
        private readonly IMapper _mapper;
        private readonly IEnvironment _environment;

        public ImportDataManagerRepository(tlrmCartonContext tccontext, IMapper mapper, IEnvironment environment)
        {
            _tcContext = tccontext;
            _mapper = mapper;
            _environment = environment;
        }

        public ImportResultDto GetAlternativeNoUpdateResult(List<ExcelParseAlternativeNoUpdateViewModel> alternativeNoList, int userId)
        {
            List<SqlParameter> parms = new List<SqlParameter>
            {
                 new SqlParameter { ParameterName = ImportDataAlternativeNoStoredprocedure.StoredProcedureParameters[0].ToString(), Value = _environment.GetCurrentEnvironment().UserId.AsDbValue() } ,

                new SqlParameter
                {
                   ParameterName = ImportDataAlternativeNoStoredprocedure.StoredProcedureParameters[1].ToString(),
                   TypeName = ImportDataAlternativeNoStoredprocedure.StoredProcedureTypeNames[0].ToString(),
                   SqlDbType = SqlDbType.Structured,
                   Value =alternativeNoList.ToList().ToDataTable()
                },
            };

            var outSuccessCount = new SqlParameter
            {
                ParameterName = ImportDataAlternativeNoStoredprocedure.StoredProcedureParameters[2].ToString(),
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };
            parms.Add(outSuccessCount);

            var result = _tcContext.Set<ImportErrorModelItemDto>().FromSqlRaw(ImportDataAlternativeNoStoredprocedure.Sql, parms.ToArray()).ToList();

            if (result == null || result != null && result.Count() == 0)
                return new ImportResultDto(alternativeNoList.Count(), 0, alternativeNoList.Count(), new List<ImportErrorModelItemDto>());
            else
                return new ImportResultDto((int)outSuccessCount.Value, result.Count(), alternativeNoList.Count(), result);

        }

        public ImportResultDto GetDestructionDateUpdateResult(List<ExcelParseDestructioDateUpdateViewModel> destructionDateList, int userId)
        {
            List<SqlParameter> parms = new List<SqlParameter>
            {
                 new SqlParameter { ParameterName = ImportDataDestructionDateStoredprocedure.StoredProcedureParameters[0].ToString(), Value =_environment.GetCurrentEnvironment().UserId.AsDbValue() } ,

                new SqlParameter
                {
                   ParameterName = ImportDataDestructionDateStoredprocedure.StoredProcedureParameters[1].ToString(),
                   TypeName = ImportDataDestructionDateStoredprocedure.StoredProcedureTypeNames[0].ToString(),
                   SqlDbType = SqlDbType.Structured,
                   Value =destructionDateList.ToList().ToDataTable()
                },
            };

            var outSuccessCount = new SqlParameter
            {
                ParameterName = ImportDataDestructionDateStoredprocedure.StoredProcedureParameters[2].ToString(),
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };
            parms.Add(outSuccessCount);

            var result = _tcContext.Set<ImportErrorModelItemDto>().FromSqlRaw(ImportDataDestructionDateStoredprocedure.Sql, parms.ToArray()).ToList();

            if (result == null || result != null && result.Count() == 0)
                return new ImportResultDto(destructionDateList.Count(), 0, destructionDateList.Count(), new List<ImportErrorModelItemDto>());
            else
                return new ImportResultDto((int)outSuccessCount.Value, result.Count(), destructionDateList.Count(), result);
        }

    }
}
