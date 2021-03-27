
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Reporsitory.IRepository
{
    public interface ITokenServicesRepository
    {
        string CreateToken(string username);
    }
}
