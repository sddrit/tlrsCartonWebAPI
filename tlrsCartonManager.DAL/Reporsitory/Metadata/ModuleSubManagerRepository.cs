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
using tlrsCartonManager.DAL.Reporsitory.Metadata.Core;
using tlrsCartonManager.DAL.Dtos.Module;

namespace tlrsCartonManager.DAL.Reporsitory
{
    public class ModuleSubManagerRepository :  BaseMetadataRepository<ModuleSub, ModuleSubMetaDataDto>
    {
        public ModuleSubManagerRepository(tlrmCartonContext tccontext, IMapper mapper, BaseMetaRepositoryValidator validator) 
            : base(tccontext, mapper,validator)
        {
        }

    }
}