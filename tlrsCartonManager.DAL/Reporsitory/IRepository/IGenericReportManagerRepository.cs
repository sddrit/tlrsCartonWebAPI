using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Reporsitory.IRepository
{
    public interface IGenericReportManagerRepository<T>
    {
        public T GetReportData(string menuName,string filterOptions);

    }
}
