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
using tlrsCartonManager.DAL.Dtos.Import;
using tlrsCartonManager.DAL.Exceptions;

namespace tlrsCartonManager.DAL.Reporsitory
{
    public class ImportDataManagerRepository : IImportDataManagerRepository
    {
        private readonly tlrmCartonContext _tcContext;
        private readonly IMapper _mapper;

        public ImportDataManagerRepository(tlrmCartonContext tccontext, IMapper mapper)
        {
            _tcContext = tccontext;
            _mapper = mapper;
        }

        public ImportResultDto GetAlternativeNoUpdateResult(List<ExcelParseAlternativeNoUpdateViewModel> alternativeNoList, int userId)
        {
            List<SqlParameter> parms = new List<SqlParameter>
            {
                 new SqlParameter { ParameterName = ImportDataAlternativeNoStoredprocedure.StoredProcedureParameters[0].ToString(), Value = userId.AsDbValue() } ,

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
                 new SqlParameter { ParameterName = ImportDataDestructionDateStoredprocedure.StoredProcedureParameters[0].ToString(), Value = userId.AsDbValue() } ,

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