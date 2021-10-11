using AutoMapper;
using tlrsCartonManager.DAL.Models;
using tlrsCartonManager.DAL.Models.MetaData;
using tlrsCartonManager.DAL.Reporsitory.Metadata.Core;

namespace tlrsCartonManager.DAL.Reporsitory
{

    public class ReceiveTypeManagerRepository : BaseMetadataRepository<ReceiveType, ReceiveTypeDto>
    {
        public ReceiveTypeManagerRepository(tlrmCartonContext tccontext, IMapper mapper, BaseMetaRepositoryValidator validator)
            : base(tccontext, mapper, validator)
        {

        }

    }

}
