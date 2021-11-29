using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using tlrsCartonManager.Services.ImportData;
using Microsoft.AspNetCore.Http;
using tlrsCartonManager.Core.Enums;
using tlrsCartonManager.DAL.Dtos.Import;
using tlrsCartonManager.Api.Util.Authorization;

namespace tlrsCartonManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ImportController : Controller
    {

        private readonly ImportDataService _importDataService;

        public ImportController(ImportDataService importDataService)
        {
            _importDataService = importDataService;
        }

        [HttpPost("importDataAlternativeNo")]
        [RmsAuthorization("Alternative No Import", tlrsCartonManager.Core.Enums.ModulePermission.Add)]
        public IActionResult ImportDataAlternativeNos(IFormFile file, int userId)
        {
            return Ok(_importDataService.GetImportDetails<ExcelParseAlternativeNoUpdateViewModel>(file, ImportType.AlternativeNoUpdate, userId));
        }

        [HttpPost("importDataDestructionDates")]
        [RmsAuthorization("Destruction Date Import", tlrsCartonManager.Core.Enums.ModulePermission.Add)]
        public IActionResult ImportDataDestructionDates(IFormFile file, int userId)
        {
            return Ok(_importDataService.GetImportDetails<ExcelParseDestructioDateUpdateViewModel>(file, ImportType.DestructionDateUpdate, userId));
        }

    }
}
