using AutoMapper;
using tlrsCartonManager.DAL.Models;
using tlrsCartonManager.DAL.Dtos.MetaData;
using tlrsCartonManager.DAL.Reporsitory.Metadata.Core;

namespace tlrsCartonManager.DAL.Reporsitory
{
    public class WorkOrderTypeManagerRepository : BaseMetadataRepository<WorkOrderRequestType, WorkOrderTypeDto>
    {
        public WorkOrderTypeManagerRepository(tlrmCartonContext tccontext, IMapper mapper, BaseMetaRepositoryValidator validator)
            : base(tccontext, mapper, validator)
        {

        }

    }
}
