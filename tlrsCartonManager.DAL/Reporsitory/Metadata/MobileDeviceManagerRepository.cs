using AutoMapper;
using tlrsCartonManager.DAL.Models;
using tlrsCartonManager.DAL.Dtos;
using tlrsCartonManager.DAL.Models.MetaData;
using tlrsCartonManager.DAL.Reporsitory.Metadata.Core;

namespace tlrsCartonManager.DAL.Reporsitory
{
    public class MobileDeviceManagerRepository : BaseMetadataRepository<MobileDevice, MobileDeviceDto>
    {
        public MobileDeviceManagerRepository(tlrmCartonContext tccontext, IMapper mapper, BaseMetaRepositoryValidator validator)
            : base(tccontext, mapper, validator)
        {

        }

    }
}
