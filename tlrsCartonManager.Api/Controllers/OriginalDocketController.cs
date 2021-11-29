using tlrsCartonManager.DAL.Dtos;
using tlrsCartonManager.DAL.Reporsitory.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using tlrsCartonManager.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using tlrsCartonManager.Api.Error;
using System.Net;
using tlrsCartonManager.Api.Util.Authorization;

namespace tlrsCartonManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OriginalDocketController : Controller
    {
        private readonly IRequestManagerRepository _requestRepository;

        public OriginalDocketController(IRequestManagerRepository requestRepository)
        {
            _requestRepository = requestRepository;
        }

        [HttpGet]
        [RmsAuthorization("Original Docket Entry", tlrsCartonManager.Core.Enums.ModulePermission.View)]
        public async Task<ActionResult<OriginalDocketSearchDto>> SearchOriginalDocket(string searchtext, string searchColumn, string sortOrder, int pageIndex, int pageSize)
        {
            var requestList = await _requestRepository.SearchOriginalDockets(searchtext, searchColumn, sortOrder, pageIndex, pageSize);
            return Ok(requestList);
        }           

        [HttpPost]
        [RmsAuthorization("Original Docket Entry", tlrsCartonManager.Core.Enums.ModulePermission.Add)]
        public ActionResult AddOriginalDocket(RequestOriginalDocket request)
        {           

            if (_requestRepository.AddOriginalDocketNoAsync(request))
                return new JsonErrorResult(new { Message = "Docket No Updated" }, HttpStatusCode.OK);
            else
                return new JsonErrorResult(new { Message = "Docket No Updation Failed" }, HttpStatusCode.NotFound);         
          
        }       

    }
}
