using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tlrsCartonManager.DAL.Models.DashBoard;

namespace tlrsCartonManager.DAL.Reporsitory.IRepository
{
    public interface IDashBoardManagerRepository
    {
        Task<List<DashBoardWeeklyWOStatusDetail>> GetWeelklyWoStatusAsync();
    }
}
