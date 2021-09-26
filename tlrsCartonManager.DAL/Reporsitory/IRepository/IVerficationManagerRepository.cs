using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tlrsCartonManager.DAL.Dtos.DailyCollectionMark;
using tlrsCartonManager.DAL.Helper;
using tlrsCartonManager.DAL.Models;
using tlrsCartonManager.DAL.Models.Verification;

namespace tlrsCartonManager.DAL.Reporsitory.IRepository
{
    public interface IVerficationManagerRepository
    {
        Task<List<VerificationPickInvalidViewModel>> GetInvalidScans(string requestNo);
        Task<List<VerificationPickViewModel>> GetVerified(string requestNo);
        Task<List<VerificationPickViewModel>> UpdateAndGetCartons(VerificationPickModel request);
    }
}
