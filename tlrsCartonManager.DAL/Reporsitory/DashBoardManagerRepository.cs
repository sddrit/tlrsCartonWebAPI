using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tlrsCartonManager.DAL.Exceptions;
using tlrsCartonManager.DAL.Models;
using tlrsCartonManager.DAL.Models.DashBoard;
using tlrsCartonManager.DAL.Reporsitory.IRepository;

namespace tlrsCartonManager.DAL.Reporsitory
{
    public class DashBoardManagerRepository : IDashBoardManagerRepository
    {
        private readonly tlrmCartonContext _tcContext;
        private readonly IMapper _mapper;
   

        public DashBoardManagerRepository(tlrmCartonContext tccontext, IMapper mapper)
        {
            _tcContext = tccontext;
            _mapper = mapper;
           
        }
        public async Task<List<DashBoardWeeklyWOStatusDetail>> GetWeelklyWoStatusAsync()
        {
            var result =await  _tcContext.Set<DashBoardWeeklyWOStatusDetail>().FromSqlRaw("dashBoardWeeklyWoStatus").ToListAsync();
            if (!result.Any())
            {
                throw new ServiceException(new ErrorMessage[]
                  {
                    new ErrorMessage()
                    {
                        Code = string.Empty,
                        Message = $"Unable to find Weekly Wo data"
                    }
                  });
            }
            return result;
        }

        public async Task<List<DashBoardWeeklyWOStatusDetail>> GetWeelklyWoStatusByTypeAsync()
        {
            var result = await _tcContext.Set<DashBoardWeeklyWOStatusDetail>().FromSqlRaw("dashBoardWeeklyWoStatusbyWoType").ToListAsync();
            if (!result.Any())
            {
                throw new ServiceException(new ErrorMessage[]
                  {
                    new ErrorMessage()
                    {
                        Code = string.Empty,
                        Message = $"Unable to find Weekly Wo data by type"
                    }
                  });
            }
            return result;
        }

        public async Task<List<DashBoardWeeklyCartonsInAndConfirm>> GetWeelklyCartonsInAndConfirm()
        {
            var result = await _tcContext.Set<DashBoardWeeklyCartonsInAndConfirm>().FromSqlRaw("dashBoardWeeklyCartonInAndConfirmed").ToListAsync();
            if (!result.Any())
            {
                throw new ServiceException(new ErrorMessage[]
                  {
                    new ErrorMessage()
                    {
                        Code = string.Empty,
                        Message = $"Unable to find Weekly in and confirmed"
                    }
                  });
            }
            return result;
        }
    }
}
