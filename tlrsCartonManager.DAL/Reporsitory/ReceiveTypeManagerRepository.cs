using AutoMapper;
using tlrsCartonManager.DAL.Reporsitory;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using tlrsCartonManager.DAL.Reporsitory.IRepository;
using tlrsCartonManager.DAL.Models;
using tlrsCartonManager.DAL.Dtos;
using Microsoft.Data.SqlClient;
using tlrsCartonManager.DAL.Utility;
using System.Data;
using tlrsCartonManager.DAL.Helper;
using static tlrsCartonManager.DAL.Utility.Status;
using tlrsCartonManager.DAL.Extensions;
using Newtonsoft.Json;
using tlrsCartonManager.DAL.Models.MetaData;

namespace tlrsCartonManager.DAL.Reporsitory
{
    public class ReceiveTypeManagerRepository : IReceiveTypeManagerRepository
    {
        private readonly tlrmCartonContext _tcContext;
        private readonly IMapper _mapper;

        public ReceiveTypeManagerRepository(tlrmCartonContext tccontext, IMapper mapper)
        {
            _tcContext = tccontext;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ReceiveType>> GetReceiveTypeList()
        {
            var receiveType = await _tcContext.ReceiveTypes.ToListAsync();
            return _mapper.Map<IEnumerable<ReceiveType>>(receiveType);
        }

       
    }
}
