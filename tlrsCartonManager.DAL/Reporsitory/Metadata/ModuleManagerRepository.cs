using AutoMapper;
using tlrsCartonManager.DAL.Models;
using tlrsCartonManager.DAL.Reporsitory.Metadata.Core;
using tlrsCartonManager.DAL.Dtos.Module;

namespace tlrsCartonManager.DAL.Reporsitory
{
    public class ModuleManagerRepository :  BaseMetadataRepository<Module, ModuleMetaDataDto>
    {
        public ModuleManagerRepository(tlrmCartonContext tccontext, IMapper mapper, BaseMetaRepositoryValidator validator) 
            : base(tccontext, mapper,validator)
        {
        }

    }
}
