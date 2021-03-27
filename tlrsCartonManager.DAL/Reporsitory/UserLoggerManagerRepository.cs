using AutoMapper;
using tlrsCartonManager.DAL.Reporsitory;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tlrsCartonManager.DAL.Reporsitory.IRepository;
using tlrsCartonManager.DAL.Models;
using tlrsCartonManager.DAL.Dtos;

namespace tlrsCartonManager.DAL.Reporsitory
{
    public class UserLoggerManagerRepository : IUserLoggerManagerRepository
    {
        private readonly tlrmCartonContext _tcContext;
        private readonly IMapper _mapper;

        public UserLoggerManagerRepository(tlrmCartonContext tcContext, IMapper mapper)
        {
            _tcContext = tcContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserLoggerDto>> GetUserLoggerList()
        {
            var userLogger = await _tcContext.UserLoggers.ToListAsync();
            return _mapper.Map<IEnumerable<UserLoggerDto>>(userLogger);

        }
    }
}
