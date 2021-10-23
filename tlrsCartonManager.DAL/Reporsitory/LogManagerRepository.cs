using AutoMapper;
using tlrsCartonManager.DAL.Reporsitory.IRepository;
using tlrsCartonManager.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using tlrsCartonManager.DAL.Helper;
using tlrsCartonManager.DAL.Exceptions;
using Microsoft.EntityFrameworkCore;
using tlrsCartonManager.DAL.Extensions;

namespace tlrsCartonManager.DAL.Reporsitory
{
    public class LogManagerRepository : ILogManagerRepository
    {
        private readonly tlrmCartonContext _tcContext;
        private readonly IMapper _mapper;
      


        public LogManagerRepository(tlrmCartonContext tccontext, IMapper mapper  )
        {
            _tcContext = tccontext;
            _mapper = mapper;
           
        }
        public  PagedResponse<Log> GetDbErrorLogAsync(string searchColumn, int pageIndex, int pageSize)
        {
           
            var result = _tcContext.Logs.ToList();

            if (!string.IsNullOrEmpty(searchColumn))
            {
                result = _tcContext.Logs.Where(x =>x.FullMessage.Contains(searchColumn) 
               || (x.ShortMessage.Contains(searchColumn))
               || (x.Id.ToString().Contains(searchColumn))
               ||( x.CreatedOnUtc.Contains(searchColumn))
               

                ).ToList();

               
            }

            var paginationResponse = new PagedResponse<Log>(result, pageIndex, pageSize, result.Count);

            if (paginationResponse == null)
            {
                throw new ServiceException(new ErrorMessage[]
                {
                     new ErrorMessage()
                     {
                            Code = string.Empty,
                            Message = $"Unable to find erro logs"
                     }
                });
            }
            return paginationResponse;

        }
    }
}
