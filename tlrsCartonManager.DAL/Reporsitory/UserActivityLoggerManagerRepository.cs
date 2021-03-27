using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tlrsCartonManager.DAL.Dtos;
using tlrsCartonManager.DAL.Models;
using tlrsCartonManager.DAL.Reporsitory.IRepositiry;

namespace tlrsCartonManager.DAL.Reporsitory
{
    public class UserActivityLoggerManagerRepository : IUserActivityLoggerManagerRepository
    {
        private readonly tlrmCartonContext _tcContext;
        private readonly IMapper _mapper;

        public UserActivityLoggerManagerRepository(tlrmCartonContext tcContext, IMapper mapper)
        {
            _tcContext = tcContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserActivityLoggerDto>> GetUserActivityLoggerList()
        {
            var userActivityLogger = await _tcContext.UserActivityLoggers.ToListAsync();
            return _mapper.Map<IEnumerable<UserActivityLoggerDto>>(userActivityLogger);

        }
    }
}
