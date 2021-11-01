using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tlrsCartonManager.DAL.Models.SequenceMonthEnd;

namespace tlrsCartonManager.DAL.Reporsitory.IRepository
{
    public interface ISequenceManagerRepository
    {
        List<SequenceModel> GetSequenceAsync();
        bool AddSequence(SequenceModel request);
        bool UpdateSequence(SequenceModel request);
        SequenceModel GetSequenceByCodeAsync(string code);

    }
}
