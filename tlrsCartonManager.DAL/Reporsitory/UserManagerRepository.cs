using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tlrsCartonManager.DAL.Dto;
using tlrsCartonManager.DAL.Models;
using tlrsCartonManager.DAL.Reporsitory.IRepositiry;

namespace tlrsCartonManager.DAL.Reporsitory
{
    public class UserManagerRepository : IUserManagerRepository
    {
        private readonly tlrmCartonContext _tcContext;
        private readonly IMapper _mapper;
        public UserManagerRepository(tlrmCartonContext tccontext, IMapper mapper)
        {
            _tcContext = tccontext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDto>> GetUsersList()
        {
            var users = await _tcContext.Users.ToListAsync();

            return _mapper.Map<IEnumerable<UserDto>>(users);


        }
    }
}
