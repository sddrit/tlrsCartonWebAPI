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
using tlrsCartonManager.DAL.Dtos.MetaData;

namespace tlrsCartonManager.DAL.Reporsitory
{
    public class DisposalTimeFrameManagerRepository : IDisposalTimeFrameManagerRepository
    {
        private readonly tlrmCartonContext _tcContext;
        private readonly IMapper _mapper;

        public DisposalTimeFrameManagerRepository(tlrmCartonContext tccontext, IMapper mapper)
        {
            _tcContext = tccontext;
            _mapper = mapper;
        }
        public async  Task<IEnumerable<DisposalTimeFrameDto>> GetDisposalTimeFrameList()
        {
            var billingCycle = await _tcContext.DisposalTimeFrames.ToListAsync();
            return _mapper.Map<IEnumerable<DisposalTimeFrameDto>>(billingCycle);

        }

       
    }
    }
