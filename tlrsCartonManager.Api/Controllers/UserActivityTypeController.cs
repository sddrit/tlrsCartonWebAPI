using tlrsCartonManager.DAL.Dtos;
using tlrsCartonManager.DAL.Reporsitory.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace tlrsCartonManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserActivityTypeController : Controller
    {
        private readonly IUserActivityTypeManagerRepository _userActivityTypeRepository;

        public UserActivityTypeController(IUserActivityTypeManagerRepository userActivityTypeRepository)
        {
            _userActivityTypeRepository = userActivityTypeRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserActivityTypeDto>>> GetUserActivityTypeList()
        {
            var userActivityTypeList = await _userActivityTypeRepository.GetUserActivityTypeList();
            return Ok(userActivityTypeList);
        }
    }
}
