using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tlrsCartonManager.DAL.Reporsitory.IRepository;

namespace tlrsCartonManager.DAL.Reporsitory
{
    public class GenericReportManagerRepository<T> : IGenericReportManagerRepository<T>
    {
        public T GetReportData(string menuName, string filterOptions)
        {
            throw new NotImplementedException();
        }
    }
}
