using tlrsCartonManager.DAL.Reporsitory.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using tlrsCartonManager.Api.Util.Authorization;

namespace tlrsCartonManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DashBoardController : Controller
    {
        private readonly IDashBoardManagerRepository _dashBoardRepository;

        public DashBoardController(IDashBoardManagerRepository dashBoardRepository)
        {
            _dashBoardRepository = dashBoardRepository;
        }

        [HttpGet("getWeeklyStatusbyRequetType")]
        [RmsAuthorization("Daily Dashboard", tlrsCartonManager.Core.Enums.ModulePermission.View)]
        public async Task<ActionResult> GetWeeklyWoStatus()
        {
            return Ok(await _dashBoardRepository.GetWeelklyWoStatusAsync());
            
        }

        [HttpGet("getWeeklyPendingRetrievalByWoType")]
        [RmsAuthorization("Daily Dashboard", tlrsCartonManager.Core.Enums.ModulePermission.View)]
        public async Task<ActionResult> GetWeeklyWoStatusByType()
        {
            return Ok(await _dashBoardRepository.GetWeelklyPendingRetrievalByTypeAsync());

        }

        [HttpGet("getDailyDashBoard")]
        [RmsAuthorization("Daily Dashboard", tlrsCartonManager.Core.Enums.ModulePermission.View)]
        public async Task<ActionResult> GetDashBoard()
        {
            return Ok(await _dashBoardRepository.GetDailyDashBoard());

        }

        [HttpGet("getWeeklyCartonInAndConfirm")]
        [RmsAuthorization("Daily Dashboard", tlrsCartonManager.Core.Enums.ModulePermission.View)]
        public async Task<ActionResult> GetWeeklyCartonInAndConfirm()
        {
            return Ok(await _dashBoardRepository.GetWeelklyCartonsInAndConfirm());

        }

        [HttpGet("getWeeklyScannedCartons")]
        [RmsAuthorization("Daily Dashboard", tlrsCartonManager.Core.Enums.ModulePermission.View)]
        public async Task<ActionResult> GetWeeklyScannedCartons()
        {
            return Ok(await _dashBoardRepository.GetWeelklyScannedCartons());

        }

        [HttpGet("gettWeeklyScannedCartonsByWH")]
        [RmsAuthorization("Daily Dashboard", tlrsCartonManager.Core.Enums.ModulePermission.View)]
        public async Task<ActionResult> GetWeeklyScannedCartonsByWH()
        {
            return Ok(await _dashBoardRepository.GetWeelklyWeeklyScannedCartonsByWH());

        }
    }
}
