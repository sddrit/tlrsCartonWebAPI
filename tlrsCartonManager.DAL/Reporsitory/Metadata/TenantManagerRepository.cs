﻿using AutoMapper;
using tlrsCartonManager.DAL.Models;
using tlrsCartonManager.DAL.Dtos;
using tlrsCartonManager.DAL.Reporsitory.Metadata.Core;

namespace tlrsCartonManager.DAL.Reporsitory
{
    public class TenantManagerRepository :  BaseMetadataRepository<Tenant, TenantDto>
    {
        public TenantManagerRepository(tlrmCartonContext tccontext, IMapper mapper, BaseMetaRepositoryValidator validator) 
            : base(tccontext, mapper,validator)
        {
        }

    }
}
