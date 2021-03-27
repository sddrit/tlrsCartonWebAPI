﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tlrsCartonManager.DAL.Dtos;

namespace tlrsCartonManager.DAL.Reporsitory.IRepositiry
{
    public interface IUserLoggerManagerRepository
    {
        Task<IEnumerable<UserLoggerDto>> GetUserLoggerList();
    }
}
