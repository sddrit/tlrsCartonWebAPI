using tlrsCartonManager.DAL.Reporsitory.IRepository;
using Microsoft.AspNetCore.Mvc;
using tlrsCartonManager.DAL.Models;
using tlrsCartonManager.DAL.Models.Report;

namespace tlrsCartonManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class GenericReportController : Controller
    {
        private readonly IGenericReportManagerRepository _reportRepository;

        public GenericReportController(IGenericReportManagerRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        [HttpPost]
        public ActionResult GetInventoryByCustomer(GenericReportData model)
        {
          
            return Ok(_reportRepository.GetReportData(model));

        }

        [HttpGet]
        public ActionResult GetGenericReportColumns(string reportName)
        {

            return Ok(_reportRepository.GetReportColumns(reportName));

        }

        [HttpPost("GetReportDataforView")]
        public ActionResult GetReportDataforView(GenericReportData model)
        {
            return Ok(_reportRepository.GetReportDataforView(model));
        }
    }
}
