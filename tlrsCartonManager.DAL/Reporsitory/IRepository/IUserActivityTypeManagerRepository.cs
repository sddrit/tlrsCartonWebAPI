
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tlrsCartonManager.DAL.Dtos;

namespace tlrsCartonManager.DAL.Reporsitory.IRepository
{
    public interface IUserActivityTypeManagerRepository
    {
        Task<IEnumerable<UserActivityTypeDto>> GetUserActivityTypeList();
    }
}
