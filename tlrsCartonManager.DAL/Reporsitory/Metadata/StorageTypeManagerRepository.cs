using AutoMapper;
using tlrsCartonManager.DAL.Models;
using tlrsCartonManager.DAL.Dtos;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Reporsitory
{
    public class StorageTypeManagerRepository : BaseMetadataRepository<StorageType, StorageTypeDto>
    {
        public StorageTypeManagerRepository(tlrmCartonContext tccontext, IMapper mapper) : base(tccontext, mapper)
        {
                        
        }
       
    }
}
