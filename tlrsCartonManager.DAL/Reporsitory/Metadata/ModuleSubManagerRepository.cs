using AutoMapper;
using tlrsCartonManager.DAL.Models;
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
