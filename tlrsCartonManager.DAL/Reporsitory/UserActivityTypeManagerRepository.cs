using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tlrsCartonManager.DAL.Dtos;
using tlrsCartonManager.DAL.Models;
using tlrsCartonManager.DAL.Reporsitory.IRepository;

namespace tlrsCartonManager.DAL.Reporsitory
{
    public class UserActivityTypeManagerRepository: IUserActivityTypeManagerRepository
    {
        private readonly tlrmCartonContext _tcContext;
        private readonly IMapper _mapper;

        public UserActivityTypeManagerRepository(tlrmCartonContext tcContext, IMapper mapper)
        {
            _tcContext = tcContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserActivityTypeDto>> GetUserActivityTypeList()
        {
            var userActivityType = await _tcContext.UserActivityTypes.ToListAsync();

            return _mapper.Map<IEnumerable<UserActivityTypeDto>>(userActivityType);


        }
    }
}
